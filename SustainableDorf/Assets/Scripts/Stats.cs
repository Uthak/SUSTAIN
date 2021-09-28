using UnityEngine;

public class Stats : MonoBehaviour
{
    // checks position when spawning to allow placing the tile back if not wanted 
    // used in Jans "PutSelectedTileBack"-Script
    public Vector3 myPosition;

    // one-time stats generated randomly for every new tile
    public float prosperityStat;
    public float happinessStat;
    public float environmentStat;

    // these are used to determine the value of the stats
    // why public???
    float randomFloat; // technically this is now == to mainStat
    public float mainStat;
    public float randomStat1;
    public float randomStat2;

    // used to decide when to play "mooh"-sound
    public bool isCow = false;

    // important as it declares which tiles NOT to destroy
    public bool wasPlaced = false;

    // these are generated in the wake of the Neighbor-Effect
    // why public???
    public float prosperityBonus = 0f;
    public float happinessBonus = 0f;
    public float environmentBonus = 0f;

    // used in UpdateStats() to update overall degeneration rate with this factor
    private float degenerationFactor;

    float positiveInfluence = -.001f;
    float neutralInfluence = 0f;
    float negativeInfluence = .001f;
    GameObject sceneManager;

    // this will stop working (!!) if there is no instance of STATS in the scene from the start!
    private void Start()
    {
        sceneManager = GameObject.Find("SceneManager");

        positiveInfluence = sceneManager.GetComponent<GameManager>().positiveInfluence;
        neutralInfluence = sceneManager.GetComponent<GameManager>().neutralInfluence;
        negativeInfluence = sceneManager.GetComponent<GameManager>().negativeInfluence;
    }

    // randomize Stats of a tile AND its rotation on the platform
    public void RandomizeStats(GameObject tile, Vector3 position)
    {
        // checks position when spawning to allow placing the tile back if not wanted
        myPosition = position;

        sceneManager = GameObject.Find("SceneManager");

        positiveInfluence = sceneManager.GetComponent<GameManager>().positiveInfluence;
        neutralInfluence = sceneManager.GetComponent<GameManager>().neutralInfluence;
        negativeInfluence = sceneManager.GetComponent<GameManager>().negativeInfluence;

        // checks whether we use one-time or contineous modifiers to stats
        if (sceneManager.GetComponent<GameManager>().usingContinuousValues)
        {
            //randomFloat = Random.Range(SceneManager.GetComponent<GameManager>().minInfluencePerTile, SceneManager.GetComponent<GameManager>().maxInfluencePerTile);
            randomFloat = Random.Range(sceneManager.GetComponent<GameManager>().minStatsPerTile, sceneManager.GetComponent<GameManager>().maxStatsPerTile);

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
            environmentStat = randomFloat * -1 /*Random.Range(-1f, 0f)*/; // factorys always cost environment
        }
        else if (CompareTag("nature"))
        {
            prosperityStat = randomFloat * Random.Range(-1f, -0.5f); // nature always costs money
            happinessStat = randomFloat * Random.Range(0f, .5f); // nature always makes people happy
            environmentStat = mainStat;
        }
        else if (CompareTag("social"))
        {
            prosperityStat = randomFloat * Random.Range(-1f, -0.5f); // social always costs money
            happinessStat = mainStat;
            environmentStat = randomStat2;
        }
        else if (CompareTag("sustainable")) // does this make sense? essentially the same as environment-tile
        {
            prosperityStat = randomStat2;
            happinessStat = randomStat1;
            environmentStat = randomFloat * Random.Range(0f, .5f); // sustainability always aims to improve the environment
        }

    }

    // called when placing tile to transmit Stats
    // called in Jans "PlaceObjectsOnGrid"-Script
    public void UpdateStats()
    {

        // this bool tells the "TileGenerator"-script NOT to destroy a placed tile
        wasPlaced = true;


        // this Counter works in connection with a timer to increase game difficulty over time
        sceneManager.GetComponent<NeedsManager>().tileCounter += 1;

        degenerationFactor = sceneManager.GetComponent<GameManager>().degenerationFactor;

        // add the one-time stats of a tile with its neighbor-effects and add them once to the NEEDSMANAGER
        // this is the basis to derive the contineous influence (10% of the one-time-stats)
        /*Debug.Log("prosperityStat = " + prosperityStat + " + prosperityBonus = " + prosperityBonus); // test
        prosperityStat += prosperityBonus;
        Debug.Log("new prosperityStat == " + prosperityStat); // test

        Debug.Log(" happinessStat = " + happinessStat + " + happinessBonus = " + happinessBonus); // test
        happinessStat += happinessBonus;
        Debug.Log("new happinessStat == " + happinessStat); // test

        Debug.Log("environmentStat = " + environmentStat + " + environmentBonus = " + environmentBonus); // test
        environmentStat += environmentBonus;
        Debug.Log("new environmentStat == " + environmentStat); // test*/


        sceneManager.GetComponent<NeedsManager>().prosperityValue += prosperityStat;
        if (sceneManager.GetComponent<NeedsManager>().prosperityValue > 10f)
        {
            sceneManager.GetComponent<NeedsManager>().prosperityValue = 10f;
        }
        sceneManager.GetComponent<NeedsManager>().environmentValue += environmentStat;
        if (sceneManager.GetComponent<NeedsManager>().environmentValue > 10f)
        {
            sceneManager.GetComponent<NeedsManager>().environmentValue = 10f;
        }
        sceneManager.GetComponent<NeedsManager>().happinessValue += happinessStat;
        if (sceneManager.GetComponent<NeedsManager>().happinessValue > 10f)
        {
            sceneManager.GetComponent<NeedsManager>().happinessValue = 10f;
        }

        // the "* degenerationFactor" takes the static xStats and multiplies them with the degenerationFactor from the Game Manager, effectively creating a contineous influnece that is 1% of the static value
        // a degenerationFactor of .0001 = .01 % of static x 50 per secon. .0001 of (ex.) 3 would be 0.015 per second and 1 full unit every 66.6 seconds
        sceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate -= prosperityStat * degenerationFactor;
        sceneManager.GetComponent<NeedsManager>().environmentDegenerationRate -= environmentStat * degenerationFactor;
        sceneManager.GetComponent<NeedsManager>().happinessDegenerationRate -= happinessStat * degenerationFactor;

        // calls function to destory leftover tiles in "TileGenerator"-script & loads new ones
        // this used to be part of BOTH above IF statements. if this works - delete THIS comment (!!!)
        sceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
        sceneManager.GetComponent<TileGenerator>().NextSet();
        return;
    }

    // beginning of neighbor-effects:

    public void NeighborEffect(int factoryNeighbors, int socialNeighbors, int natureNeighbors, int sustainableNeighbors)
    {
        if (CompareTag("factory"))
        {
            // welchen Einfluss hat eine Factory als Nachbar auf die sustainable-Stats der Gesellschaft in Abhängigkeit seiner Location? 
            prosperityBonus = ((float)factoryNeighbors * positiveInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * neutralInfluence) + ((float)sustainableNeighbors * negativeInfluence);
            happinessBonus = ((float)factoryNeighbors * neutralInfluence) + ((float)socialNeighbors * negativeInfluence) + ((float)natureNeighbors * negativeInfluence) + ((float)sustainableNeighbors * neutralInfluence);
            environmentBonus = ((float)factoryNeighbors * negativeInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * negativeInfluence) + ((float)sustainableNeighbors * positiveInfluence);
            
            /*prosperityBonus = .001f * (float)factoryNeighbors; // factory + factory = mehr prosperity
            happinessBonus = 0f; // -.001f * (float)socialNeighbors; // factory + social = weniger happyness
            environmentBonus = -.001f *(float)natureNeighbors; // factory + nature = weniger environment*/
        }
        else if (CompareTag("nature"))
        {
            prosperityBonus = ((float)factoryNeighbors * neutralInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * negativeInfluence) + ((float)sustainableNeighbors * negativeInfluence);
            happinessBonus = ((float)factoryNeighbors * negativeInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * positiveInfluence) + ((float)sustainableNeighbors * positiveInfluence);
            environmentBonus = ((float)factoryNeighbors * negativeInfluence) + ((float)socialNeighbors * negativeInfluence) + ((float)natureNeighbors * negativeInfluence) + ((float)sustainableNeighbors * positiveInfluence);

            /*prosperityBonus = -.0015f * (float)factoryNeighbors; // nature + factory = - prosperity
            happinessBonus = .001f * (float)socialNeighbors; // nature + social = + happyness
            environmentBonus = .001f * (float)natureNeighbors; // nature + nature = ++ environment*/
        }
        else if (CompareTag("social"))
        {
            prosperityBonus = ((float)factoryNeighbors * positiveInfluence) + ((float)socialNeighbors * negativeInfluence) + ((float)natureNeighbors * neutralInfluence) + ((float)sustainableNeighbors * positiveInfluence);
            happinessBonus = ((float)factoryNeighbors * negativeInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * positiveInfluence) + ((float)sustainableNeighbors * negativeInfluence);
            environmentBonus = ((float)factoryNeighbors * neutralInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * negativeInfluence) + ((float)sustainableNeighbors * neutralInfluence);

            //prosperityBonus = .001f * (float)factoryNeighbors; // factory + factory = mehr prosperity
            //happinessBonus = -.001f * (float)socialNeighbors; // factory + social = weniger happyness
            //environmentBonus = -.0015f * (float)natureNeighbors; // factory + nature = weniger environment
        }
        else if (CompareTag("sustainable")) // does this make sense? essentially the same as environment-tile
        {
            prosperityBonus = ((float)factoryNeighbors * negativeInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * negativeInfluence) + ((float)sustainableNeighbors * positiveInfluence);
            happinessBonus = ((float)factoryNeighbors * neutralInfluence) + ((float)socialNeighbors * negativeInfluence) + ((float)natureNeighbors * positiveInfluence) + ((float)sustainableNeighbors * positiveInfluence);
            environmentBonus = ((float)factoryNeighbors * positiveInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * positiveInfluence) + ((float)sustainableNeighbors * positiveInfluence);

            //prosperityBonus = .001f * (float)factoryNeighbors; // factory + factory = mehr prosperity
            //happinessBonus = -.001f * (float)socialNeighbors; // factory + social = weniger happyness
            //environmentBonus = -.0015f * (float)natureNeighbors; // factory + nature = weniger environment
        }

        /*if (sustainableNeighbors > 0f)
        {
            if (prosperityBonus >= 0f)
            {
                prosperityBonus *= (1f - ((float)sustainableNeighbors * .2f));
            }
            else if (prosperityBonus < 0f)
            {
                prosperityBonus *= (1f + ((float)sustainableNeighbors * .2f));
            }
            if (happinessBonus >= 0f)
            {
                happinessBonus *= (1f - ((float)sustainableNeighbors * .2f));
            }
            else if (happinessBonus < 0f)
            {
                happinessBonus *= (1f + ((float)sustainableNeighbors * .2f));
            }
            if (environmentBonus >= 0f)
            {
                environmentBonus *= (1f - ((float)sustainableNeighbors * .2f));
            }
            else if (environmentBonus < 0f)
            {
                environmentBonus *= (1f + ((float)sustainableNeighbors * .2f));
            }
        }*/

        /*if (SceneManager.GetComponent<GameManager>().usingContinuousValues)
        {
            SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate += prosperityBonus;
            SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate += environmentBonus;
            SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate += happinessBonus;
        }*/
    }

    // depending on if a stat is pos. or neg. the bonus will be multiplied with the static on a 10% basis.
    // ex: base value 1.7 * (1 + bonus .2) = new value 2.04, as a 20% bonus was applied
    // ex: base value -1.9 * (1 + (-1 * bonus .2)) = new value -1.52, as a 20% bonus was applied
    public void AddNeighborBonus()
    {
        Debug.Log("prosperityStat = " + prosperityStat + " + prosperityBonus = " + prosperityBonus); // test
        if (prosperityStat >= 0)
        {
            prosperityStat = prosperityStat * (1 + prosperityBonus);
        }else
        {
            prosperityStat = prosperityStat * (1 + (-1 * prosperityBonus));
        }
        Debug.Log("new prosperityStat == " + prosperityStat); // test

        Debug.Log(" happinessStat = " + happinessStat + " + happinessBonus = " + happinessBonus); // test
        if (happinessStat >= 0)
        {
            happinessStat *= (1 + happinessBonus);
        }else
        {
            happinessStat *= (1 + (-1 * happinessBonus));
        }
        Debug.Log("new happinessStat == " + happinessStat); // test

        Debug.Log("environmentStat = " + environmentStat + " + environmentBonus = " + environmentBonus); // test
        if (environmentStat >= 0)
        {
            environmentStat *= (1 + environmentBonus);
        }else
        {
            environmentStat *= (1 + (-1 * environmentBonus));
        }
        Debug.Log("new environmentStat == " + environmentStat); // test
    }
}


/*
public void RandomizeStats(GameObject tile, Vector3 position)
    {
        // checks position when spawning to allow placing the tile back if not wanted
        myPosition = position;

        GameObject SceneManager = GameObject.Find("SceneManager");

        // checks whether we use one-time or contineous modifiers to stats
        if (SceneManager.GetComponent<GameManager>().usingContinuousValues)
        {
            //randomFloat = Random.Range(SceneManager.GetComponent<GameManager>().minInfluencePerTile, SceneManager.GetComponent<GameManager>().maxInfluencePerTile);
            randomFloat = Random.Range(SceneManager.GetComponent<GameManager>().minStatsPerTile, SceneManager.GetComponent<GameManager>().maxStatsPerTile);

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
            environmentStat = randomFloat * -1 /*Random.Range(-1f, 0f); // factorys always cost environment
        }
        else if (CompareTag("nature"))
{
    prosperityStat = randomFloat * Random.Range(-1f, -0.5f); // nature always costs money
    happinessStat = randomFloat * Random.Range(0f, .5f); // nature always makes people happy
    environmentStat = mainStat;
}
else if (CompareTag("social"))
{
    prosperityStat = randomFloat * Random.Range(-1f, -0.5f); // social always costs money
    happinessStat = mainStat;
    environmentStat = randomStat2;
}
else if (CompareTag("sustainable")) // does this make sense? essentially the same as environment-tile
{
    prosperityStat = randomStat2;
    happinessStat = randomStat1;
    environmentStat = randomFloat * Random.Range(0f, .5f); // sustainability always aims to improve the environment
}

    }

    // called when placing tile to transmit Stats
    // called in Jans "PlaceObjectsOnGrid"-Script
    public void UpdateStats()
{

    // this bool tells the "TileGenerator"-script NOT to destroy a placed tile
    wasPlaced = true;

    GameObject SceneManager = GameObject.Find("SceneManager");

    SceneManager.GetComponent<NeedsManager>().tileCounter += 1;

    degenerationFactor = SceneManager.GetComponent<GameManager>().degenerationFactor;
    // updates stats using one-time additions & subtractions (old system)
    // key difference is updating "xxxValue", rather than xxxDegenerationRate"

    //if (SceneManager.GetComponent<GameManager>().usingOneTimeValues)
    //{
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

    //}

    // updates stats using continuous degeneration rates (new system)
    // key difference is updating "xxxDegenerationRate", rather than xxxValue"

    if (SceneManager.GetComponent<GameManager>().usingContinuousValues)
    {
        // the "* degenerationFactor" takes the static xStats and multiplies them with the degenerationFactor from the Game Manager, effectively creating a contineous influnece that is 1% of the static value
        SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate -= prosperityStat * degenerationFactor;
        SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate -= environmentStat * degenerationFactor;
        SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate -= happinessStat * degenerationFactor;
    }

    // calls function to destory leftover tiles in "TileGenerator"-script & loads new ones
    // this used to be part of BOTH above IF statements. if this works - delete THIS comment (!!!)
    SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
    SceneManager.GetComponent<TileGenerator>().NextSet();
    return;
}

// beginning of neighbor-effects:

public void NeighborEffect(int factoryNeighbors, int socialNeighbors, int natureNeighbors, int sustainableNeighbors)
{
    //float prosperityBonus = 0f;
    //float happinessBonus = 0f;
    //float environmentBonus = 0f;
    //GameObject SceneManager = GameObject.Find("SceneManager");

    float greatInfluence = -.002f;
    float positiveInfluence = -.001f;
    float neutralInfluence = 0f;
    float negativeInfluence = .001f;
    float terribleInfluence = .002f;

    if (CompareTag("factory"))
    {
        // welchen Einfluss hat eine Factory als Nachbar auf die sustainable-Stats der Gesellschaft in Abhängigkeit seiner Location? 
        /*prosperityBonus = ((float)factoryNeighbors * greatInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * neutralInfluence);
        happinessBonus = ((float)factoryNeighbors * neutralInfluence) + ((float)socialNeighbors * negativeInfluence) + ((float)natureNeighbors * terribleInfluence);
        environmentBonus = ((float)factoryNeighbors * terribleInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * terribleInfluence);
        
        prosperityBonus = .001f * (float)factoryNeighbors; // factory + factory = mehr prosperity
        happinessBonus = -.001f * (float)socialNeighbors; // factory + social = weniger happyness
        environmentBonus = -.0015f * (float)natureNeighbors; // factory + nature = weniger environment
    }
    else if (CompareTag("nature"))
    {
        prosperityBonus = ((float)factoryNeighbors * terribleInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * neutralInfluence);
        happinessBonus = ((float)factoryNeighbors * negativeInfluence) + ((float)socialNeighbors * greatInfluence) + ((float)natureNeighbors * greatInfluence);
        environmentBonus = ((float)factoryNeighbors * terribleInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * greatInfluence);

        //prosperityBonus = -.0015f * (float)factoryNeighbors; // nature + factory = - prosperity
        //happinessBonus = .001f * (float)socialNeighbors; // nature + social = + happyness
        //environmentBonus = .0015f * (float)natureNeighbors; // nature + nature = ++ environment
    }
    else if (CompareTag("social"))
    {
        prosperityBonus = ((float)factoryNeighbors * greatInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * neutralInfluence);
        happinessBonus = ((float)factoryNeighbors * terribleInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * greatInfluence);
        environmentBonus = ((float)factoryNeighbors * terribleInfluence) + ((float)socialNeighbors * neutralInfluence) + ((float)natureNeighbors * positiveInfluence);

        //prosperityBonus = .001f * (float)factoryNeighbors; // factory + factory = mehr prosperity
        //happinessBonus = -.001f * (float)socialNeighbors; // factory + social = weniger happyness
        //environmentBonus = -.0015f * (float)natureNeighbors; // factory + nature = weniger environment
    }
    else if (CompareTag("sustainable")) // does this make sense? essentially the same as environment-tile
    {
        prosperityBonus = ((float)factoryNeighbors * positiveInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * positiveInfluence);
        happinessBonus = ((float)factoryNeighbors * positiveInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * positiveInfluence);
        environmentBonus = ((float)factoryNeighbors * positiveInfluence) + ((float)socialNeighbors * positiveInfluence) + ((float)natureNeighbors * positiveInfluence);

        //prosperityBonus = .001f * (float)factoryNeighbors; // factory + factory = mehr prosperity
        //happinessBonus = -.001f * (float)socialNeighbors; // factory + social = weniger happyness
        //environmentBonus = -.0015f * (float)natureNeighbors; // factory + nature = weniger environment
    }

    /*if (sustainableNeighbors > 0f)
    {
        if (prosperityBonus >= 0f)
        {
            prosperityBonus *= (1f - ((float)sustainableNeighbors * .2f));
        }
        else if (prosperityBonus < 0f)
        {
            prosperityBonus *= (1f + ((float)sustainableNeighbors * .2f));
        }
        if (happinessBonus >= 0f)
        {
            happinessBonus *= (1f - ((float)sustainableNeighbors * .2f));
        }
        else if (happinessBonus < 0f)
        {
            happinessBonus *= (1f + ((float)sustainableNeighbors * .2f));
        }
        if (environmentBonus >= 0f)
        {
            environmentBonus *= (1f - ((float)sustainableNeighbors * .2f));
        }
        else if (environmentBonus < 0f)
        {
            environmentBonus *= (1f + ((float)sustainableNeighbors * .2f));
        }
    }*/

    /*if (SceneManager.GetComponent<GameManager>().usingContinuousValues)
    {
        SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate += prosperityBonus;
        SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate += environmentBonus;
        SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate += happinessBonus;
    }
}
public void AddNeighborBonus()
{
    GameObject SceneManager = GameObject.Find("SceneManager");

    SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate += prosperityBonus;
    SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate += environmentBonus;
    SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate += happinessBonus;
}
}
}*/