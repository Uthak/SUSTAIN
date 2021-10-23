using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PutSelectedTileBack : MonoBehaviour
{
    GameObject SceneManager;
    GameObject myGo;
    //Transform myTrans;

    private void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        
    }

    private void OnMouseOver()
    {
        //Debug.Log("mouse courser über Podest");
        if (Input.GetMouseButtonDown(0))
        {
            SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab = null;
            SceneManager.GetComponent<PlaceObjectsOnGrid>().isOnGrid = true;

            //Debug.Log(SceneManager);
            GameObject curMaptile = SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject;
            curMaptile.GetComponent<BoxCollider>().enabled = true;
            Vector3 myTrans = curMaptile.GetComponent<Stats>().myPosition;
            
            
            //Debug.Log(myTrans);
            curMaptile.transform.position = myTrans;
            //myGo.transform.position = gameObject.transform.position;






        }
    }
}
