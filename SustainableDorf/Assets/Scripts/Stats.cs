using UnityEngine;

public class Stats : MonoBehaviour
{
    // checks position when spawning to allow placing the tile back if not wanted 
    // used in Jans "PutSelectedTileBack"-Script
    public Vector3 myPosition;

    public float prosperityStat;
    public float happinessStat;
    public float environmentStat;

    float randomFloat; // technically this is now == to mainStat
    public float mainStat;
    public float randomStat1;
    public float randomStat2;

    public bool isCow = false;
    public bool wasPlaced = false; // do we need this

    public void RandomizeStats(GameObject tile, Vector3 position) // added ", Vector3 position" --> delete this comment if this works (!!!)
    {
        // checks position when spawning to allow placing the tile back if not wanted
        myPosition = position;

        GameObject SceneManager = GameObject.Find("SceneManager");

        // checks whether we use one-time or contineous modifiers to stats
        // this is important, as the generated values differ vastly
        if (SceneManager.GetComponent<GameManager>().usingContinuousValues)
        { 
            randomFloat = Random.Range(SceneManager.GetComponent<GameManager>().minInfluencePerTile, SceneManager.GetComponent<GameManager>().maxInfluencePerTile);

            //mainStat = randomFloat / 2 + Random.Range(0f, (randomFloat / 2f)); // why this complicated math?
            mainStat = randomFloat;
            randomStat1 = randomFloat * Random.Range(-.5f, .5f); // second multiplyer to allow negative outcome
            randomStat2 = randomFloat * Random.Range(-.5f, .5f); // second multiplyer to allow negative outcome

            // this prevents all stats from being positive or the same
            if (randomStat1 >= 0f && randomStat2 >= 0f | randomStat1 == randomStat2)
            {
                int randomStat = Random.Range(1, 2);
                if (randomStat == 1)
                {
                    randomStat1 *= -1;
                }else
                {
                    randomStat2 *= -1;
                }
            }
        }

        // stats are generated depending on tile-tag
        if (CompareTag("factory"))
        {
            prosperityStat = mainStat;
            happinessStat = randomStat1;
            environmentStat = randomStat2;
        }else if (CompareTag("nature"))
        {
            prosperityStat = randomFloat * Random.Range(-1f, 0f); // nature always costs money
            happinessStat = randomFloat * Random.Range(0f, .5f); // nature always makes people happy
            environmentStat = mainStat;
        }else if (CompareTag("social"))
        {
            prosperityStat = randomStat1;
            happinessStat = mainStat;
            environmentStat = randomStat2;
        }else if (CompareTag("sustainable")) // does this make sense? essentially the same as environment-tile
        {
            prosperityStat = randomStat2;
            happinessStat = randomStat1;
            environmentStat = mainStat;
        }
    
        // checks whether we use one-time or contineous modifiers to stats
        // this is important, as the generated values differ vastly
        if (SceneManager.GetComponent<GameManager>().usingOneTimeValues) // can be deleted if we chose continous-effects for good
        {
            randomFloat = Random.Range(SceneManager.GetComponent<GameManager>().minStatsPerTile, SceneManager.GetComponent<GameManager>().maxStatsPerTile);
            mainStat = randomFloat / 2 + Random.Range(0f, (randomFloat / 2f)); // not sure if this makes any sense???
            randomStat1 = randomFloat * Random.Range(-.5f, .5f);
            randomStat2 = randomFloat * Random.Range(-.5f, .5f);

            // this prevents all stats from being positive or the same
            if (randomStat1 >= 0f && randomStat2 >= 0f | randomStat1 == randomStat2)
            {
                int randomStat = Random.Range(1, 2);
                if (randomStat == 1)
                {
                    randomStat1 = randomStat1 * -1;
                }else
                {
                    randomStat2 = randomStat2 * -1;
                }
            }
        
            // stats are generated depending on tile-tag
            if (CompareTag("factory"))
            {
                prosperityStat = mainStat;
                happinessStat = randomStat1;
                environmentStat = randomStat2;
            }else if (CompareTag("nature"))
            {
                prosperityStat = randomFloat * Random.Range(-1f, 0f); // nature always costs money
                happinessStat = randomFloat * Random.Range(0f, .5f); // nature always makes people happy
                environmentStat = mainStat;
            }else if (CompareTag("social"))
            {
                prosperityStat = randomStat1;
                happinessStat = mainStat;
                environmentStat = randomStat2;
            }else if (CompareTag("sustainable")) // does this make sense? essentially the same as environment-tile
            {
                prosperityStat = randomStat2;
                happinessStat = randomStat1;
                environmentStat = mainStat;
            }
        }
    }

    // called when placing tile to transmit Stats
    // called in Jans "PlaceObjectsOnGrid"-Script
    public void UpdateStats()
    {
        // this bool tells the "TileGenerator"-script NOT to destroy a placed tile
        wasPlaced = true;

        GameObject SceneManager = GameObject.Find("SceneManager");

        // updates stats using continuous degeneration rates (new system)
        // key difference is updating "xxxDegenerationRate", rather than xxxValue"
        if (SceneManager.GetComponent<GameManager>().usingContinuousValues)
        {
            SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate += prosperityStat;
            if (SceneManager.GetComponent<NeedsManager>().prosperityValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().prosperityValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate += environmentStat;
            if (SceneManager.GetComponent<NeedsManager>().environmentValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().environmentValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate += happinessStat;
            if (SceneManager.GetComponent<NeedsManager>().happinessValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().happinessValue = 10f;
            }
        }

        // updates stats using one-time additions & subtractions (old system)
        // key difference is updating "xxxValue", rather than xxxDegenerationRate"
        if (SceneManager.GetComponent<GameManager>().usingOneTimeValues)
        {
            SceneManager.GetComponent<NeedsManager>().prosperityValue += prosperityStat;
            if (SceneManager.GetComponent<NeedsManager>().prosperityValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().prosperityValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().environmentValue += environmentStat;
            if (SceneManager.GetComponent<NeedsManager>().environmentValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().environmentValue = 10f;
            }
            SceneManager.GetComponent<NeedsManager>().happinessValue += happinessStat;
            if (SceneManager.GetComponent<NeedsManager>().happinessValue > 10f)
            {
                SceneManager.GetComponent<NeedsManager>().happinessValue = 10f;
            }

        }

        // calls function to destory leftover tiles in "TileGenerator"-script & loads new ones
        // this used to be part of BOTH above IF statements. if this works - delete THIS comment (!!!)
        SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
        SceneManager.GetComponent<TileGenerator>().NextSet();
        return;
    }

    // beginning of neighbor-effects:

    /*public void InterdependenceEffect(neighbor.tag)
    {
        if (neighbor.tag == "nature")
        {}

    }*/
}