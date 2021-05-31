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
    {
        Object = gameObject;
        GameObject SceneManager = GameObject.Find("SceneManager");
        SceneManager.GetComponent<PlaceObjectsOnGrid>().OnMouse(Object);
    }

    private void OnMouseOver()
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
    private void OnMouseExit()
    {
        //Debug.Log("OnMouseExit");
        //statsDisplay.transform.GetChild(0).gameObject.SetActive(false);
        //statsDisplay = null;

        StatsDisplay.GetComponent<StatUIDisplay>().ResetStatBars();
    }
}
