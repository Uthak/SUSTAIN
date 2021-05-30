using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour
{
    private GameObject Object;
    public bool setFix = false;
    private GameObject statsDisplay;


    public void OnMouseDown()
    {
        Object = gameObject;
        GameObject SceneManager = GameObject.Find("SceneManager");
        SceneManager.GetComponent<PlaceObjectsOnGrid>().OnMouse(Object);
    }

    private void OnMouseOver()
    {
        GameObject Over = gameObject;
        Debug.Log(Over);
        //Debug.Log("OnMouseOver");
        statsDisplay = GameObject.Find("Stats_UI");
        statsDisplay.transform.GetChild(0).gameObject.SetActive(true);
        statsDisplay.GetComponent<StatUIDisplay>().CastStatsToUI();
    }
    private void OnMouseExit()
    {
        //Debug.Log("OnMouseExit");
        statsDisplay.transform.GetChild(0).gameObject.SetActive(false);
        statsDisplay = null;
    }
}
