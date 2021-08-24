using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour
{
    private GameObject Object;
    public bool setFix = false;
    private GameObject StatsDisplay;
    public GameObject Over;

    


    public void OnMouseDown()
        //übergibt angeklicktes Object an OnMouse Funktion aus PlaceObjectsOnGrid
    {
        Object = gameObject;
        GameObject SceneManager = GameObject.Find("SceneManager");
        SceneManager.GetComponent<PlaceObjectsOnGrid>().OnMouse(Object);
    }

    private void OnMouseOver()
    // Liest Stats von MapTile beim Hovern aus 
    {
        GameObject SceneManager = GameObject.Find("SceneManager");
        if (SceneManager.GetComponent<NeedsManager>().usingContinuousValues) // can be deleted if we chose continous-effects for good
        {
            if (gameObject.CompareTag("nature") || gameObject.CompareTag("factory") || gameObject.CompareTag("social") || gameObject.CompareTag("sustainable") || gameObject.CompareTag("city"))
            {
                Over = gameObject;
                tag = Over.tag;
                //Debug.Log("mS " + Over.GetComponent<Stats>().mainStat + "hS " + Over.GetComponent<Stats>().randomStat1 + "eS " + Over.GetComponent<Stats>().randomStat2);
                //Debug.Log("mS " + Over.GetComponent<Stats>().prosperityStat + "hS " + Over.GetComponent<Stats>().environmentStat + "eS " + Over.GetComponent<Stats>().happinessStat);
                //Debug.Log(Over);
                //Debug.Log("OnMouseOver");
                StatsDisplay = GameObject.Find("Stats_UI");
                Debug.Log(StatsDisplay);
                //statsDisplay.transform.GetChild(0).gameObject.SetActive(true);

                float a = Over.GetComponent<Stats>().prosperityStat * SceneManager.GetComponent<NeedsManager>().statDisplayMultiplicator; //makes miniscule values visible
                float b = Over.GetComponent<Stats>().environmentStat * SceneManager.GetComponent<NeedsManager>().statDisplayMultiplicator; //makes miniscule values visible
                float c = Over.GetComponent<Stats>().happinessStat * SceneManager.GetComponent<NeedsManager>().statDisplayMultiplicator; //makes miniscule values visible

                StatsDisplay.GetComponentInParent<StatUIDisplay>().CastStatsToUI(Over, tag, a, b, c);
            }
        }
        if (SceneManager.GetComponent<NeedsManager>().usingOneTimeValues) // can be deleted if we chose continous-effects for good
        {
            if (gameObject.CompareTag("nature") || gameObject.CompareTag("factory") || gameObject.CompareTag("social") || gameObject.CompareTag("sustainable") || gameObject.CompareTag("city"))
            {
                Over = gameObject;
                tag = Over.tag;
                //Debug.Log("mS " + Over.GetComponent<Stats>().mainStat + "hS " + Over.GetComponent<Stats>().randomStat1 + "eS " + Over.GetComponent<Stats>().randomStat2);
                //Debug.Log("mS " + Over.GetComponent<Stats>().prosperityStat + "hS " + Over.GetComponent<Stats>().environmentStat + "eS " + Over.GetComponent<Stats>().happinessStat);
                //Debug.Log(Over);
                //Debug.Log("OnMouseOver");
                StatsDisplay = GameObject.Find("Stats_UI");
                Debug.Log(StatsDisplay);
                //statsDisplay.transform.GetChild(0).gameObject.SetActive(true);

                float a = Over.GetComponent<Stats>().prosperityStat;
                float b = Over.GetComponent<Stats>().environmentStat;
                float c = Over.GetComponent<Stats>().happinessStat;

                StatsDisplay.GetComponentInParent<StatUIDisplay>().CastStatsToUI(Over, tag, a, b, c);
            }
        }
    }

    private void OnMouseExit()
        //setzt die Angezeigten Stats auf null zurück
    {
        //Debug.Log("OnMouseExit");
        //statsDisplay.transform.GetChild(0).gameObject.SetActive(false);
        //statsDisplay = null;

        StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
    }


    private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("nature") || gameObject.CompareTag("factory") || gameObject.CompareTag("social") || gameObject.CompareTag("sustainable") || gameObject.CompareTag("city"))
        {
            Debug.Log("nachbar");
        }
    }
}
