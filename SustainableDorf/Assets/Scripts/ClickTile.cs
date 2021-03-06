using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour
{
    private GameObject Object;
    public bool setFix = false;
    private GameObject StatsDisplay;
    public GameObject Over;

    //public bool hoverInfoEnabled = true;
    GameObject SceneManager;

    private void Awake()
    {
        SceneManager = GameObject.Find("SceneManager");
    }
    public void OnMouseDown()
        //?bergibt angeklicktes Object an OnMouse Funktion aus PlaceObjectsOnGrid
    {
        Object = gameObject;
        //SceneManager = GameObject.Find("SceneManager");
        SceneManager.GetComponent<GameManager>().hoverInfoEnabled = false;
        Debug.Log("hover Info OFF");
        SceneManager.GetComponent<PlaceObjectsOnGrid>().OnMouse(Object);

        // Felix added this 9.10.21 to cast stats to UI while carrying:
        //hoverInfoEnabled = false;
    }

    // Felix added this 10.10.21 to cast stats to UI while carrying:
    //hoverInfoEnabled = false;
    /*private void Update()
    {
        if (SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject != null)
        {
            hoverInfoEnabled = false;
        }
    }*/

    private void OnMouseOver()
    // Liest Stats von MapTile beim Hovern aus 
    {
        //GameObject SceneManager = GameObject.Find("SceneManager");
        //if (SceneManager.GetComponent<GameManager>().usingContinuousValues) // can be deleted if we chose continous-effects for good
        //{
            if (SceneManager.GetComponent<GameManager>().hoverInfoEnabled == true)
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
                    //Debug.Log(StatsDisplay);
                    //statsDisplay.transform.GetChild(0).gameObject.SetActive(true);

                    float a = Over.GetComponent<Stats>().prosperityStat * SceneManager.GetComponent<GameManager>().statDisplayMultiplicator; //makes miniscule values visible
                    float b = Over.GetComponent<Stats>().environmentStat * SceneManager.GetComponent<GameManager>().statDisplayMultiplicator; //makes miniscule values visible
                    float c = Over.GetComponent<Stats>().happinessStat * SceneManager.GetComponent<GameManager>().statDisplayMultiplicator; //makes miniscule values visible

                    //if (hoverInfoEnabled) // only show stats while hovering if NOTHING is carried
                    //{
                        StatsDisplay.GetComponentInParent<StatUIDisplay>().CastStatsToUI(Over, tag, a, b, c);
                    //}

                    // added by Felix to see numerical stats in dev mode while hovering
                    if (SceneManager.GetComponent<GameManager>().developerMode)
                    {
                        SceneManager.GetComponent<GameManager>().ShowDevStats(a, b, c);
                    }
                }
            }
            
        //}
        /*if (SceneManager.GetComponent<GameManager>().usingOneTimeValues) // can be deleted if we chose continous-effects for good
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
                //Debug.Log(StatsDisplay);
                //statsDisplay.transform.GetChild(0).gameObject.SetActive(true);

                float a = Over.GetComponent<Stats>().prosperityStat;
                float b = Over.GetComponent<Stats>().environmentStat;
                float c = Over.GetComponent<Stats>().happinessStat;

                StatsDisplay.GetComponentInParent<StatUIDisplay>().CastStatsToUI(Over, tag, a, b, c);

                // added by Felix to see numerical stats in dev mode while hovering
                if (SceneManager.GetComponent<GameManager>().developerMode)
                {
                    SceneManager.GetComponent<GameManager>().ShowDevStats(a, b, c);
                }
            }
       }*/
    }

    private void OnMouseExit()
        //setzt die Angezeigten Stats auf null zur?ck
    {
        //Debug.Log("OnMouseExit");
        //statsDisplay.transform.GetChild(0).gameObject.SetActive(false);
        //statsDisplay = null;
        if (SceneManager.GetComponent<GameManager>().hoverInfoEnabled == true)
        {
            StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
        }
        //StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars(); //reinstate this if aboce doesnt work

        // added by Felix to see numerical stats in dev mode while hovering
        /*GameObject SceneManager = GameObject.Find("SceneManager");
        if (SceneManager.GetComponent<GameManager>().developerMode)
        {
            SceneManager.GetComponent<GameManager>().ResetDevStats();
        }*/
    }


    // ist das hier abfall?:
    /*private void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("nature") || gameObject.CompareTag("factory") || gameObject.CompareTag("social") || gameObject.CompareTag("sustainable") || gameObject.CompareTag("city"))
        {
            //Debug.Log("nachbar");
        }
    }*/
}
