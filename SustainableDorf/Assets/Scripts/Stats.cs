using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats : MonoBehaviour
{
    public float prosperityStat;
    public float happinessStat;
    public float environmentStat;

    float randomFloat;
    float mainStat;
    float randomStat1;
    float randomStat2;

    public bool wasPlaced = false;

    [SerializeField] float minStatsPerTile = 3f;
    [SerializeField] float maxStatsPerTile = 6f;

    public void RandomizeStats(GameObject tile)
    {
        /*randomFloat = Random.Range(minStatsPerTile, maxStatsPerTile);
            mainStat = randomFloat / 2 + Random.Range(0, (randomFloat / 2));
            randomStat1 = (randomFloat - mainStat) * Random.Range(0, 1);
            randomStat2 = 1 - randomStat2;*/
        randomFloat = Random.Range(minStatsPerTile, maxStatsPerTile);
        mainStat = randomFloat / 2 + Random.Range(0f, (randomFloat / 2f));
        randomStat1 = randomFloat * Random.Range(-.5f, .5f);
        randomStat2 = randomFloat * Random.Range(-.5f, .5f);

        if (tag == "factory")
        {
            prosperityStat = mainStat;
            happinessStat = randomStat1;
            environmentStat = randomStat2;
            Debug.Log("I am " + tile + "I am a factory: prosperity " + prosperityStat + ", happiness " + happinessStat + ", environment " + environmentStat);
        }
        else if (tag == "nature")
        {
            prosperityStat = randomFloat * Random.Range(0f, -1f); // nature always costs money
            happinessStat = randomFloat * Random.Range(0f, .5f); // nature always makes people happy
            environmentStat = mainStat;
            Debug.Log("I am " + tile + "I am nature: prosperity " + prosperityStat + ", happiness " + happinessStat + ", environment " + environmentStat);

        }
        else if (tag == "social")
        {
            prosperityStat = randomStat1;
            happinessStat = mainStat;
            environmentStat = randomStat2;
            Debug.Log("I am " + tile + "I am a social structure: prosperity " + prosperityStat + ", happiness " + happinessStat + ", environment " + environmentStat);

        }
        else if (tag == "sustainable")
        {
            prosperityStat = randomStat2;
            happinessStat = randomStat1;
            environmentStat = randomFloat; // maximum environment +
            Debug.Log("I am " + tile + "I am a sustainability structure: prosperity " + prosperityStat + ", happiness " + happinessStat + ", environment " + environmentStat);

        }
    }
    public void UpdateStats() // Jan, call this here once tile is dropped
    {
        wasPlaced = true;
        GameObject SceneManager = GameObject.Find("SceneManager");
        
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

        SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();

        SceneManager.GetComponent<TileGenerator>().NextSet();        
        return;
    }
}
