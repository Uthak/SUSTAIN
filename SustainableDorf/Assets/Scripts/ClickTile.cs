using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTile : MonoBehaviour
{
    private GameObject Object;
    public bool setFix = false;


    public void OnMouseDown()
    {
        Object = gameObject;
        GameObject SceneManager = GameObject.Find("SceneManager");
        SceneManager.GetComponent<PlaceObjectsOnGrid>().OnMouse(Object);
    }
}
