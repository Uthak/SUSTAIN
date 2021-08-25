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
        //Debug.Log("ggggggggggggggggggggg");
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log(SceneManager);
            GameObject curMaptile = SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject;
            Transform myTrans = curMaptile.GetComponent<Stats>().myPosition;
            SceneManager.GetComponent<PlaceObjectsOnGrid>().isOnGrid = true;
            //GameObject myGo = SceneManager.GetComponent < PlaceObjectsOnGrid>().curObject;
            Debug.Log(myTrans.position);
            myGo.transform.position = myTrans.position;
            //myGo.transform = trans.position;
            //myGo.transform = trans.rotation;
            //myGo.transform = trans.scale;





            SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab = null;
            SceneManager.GetComponent<PlaceObjectsOnGrid>().isOnGrid = true;
        }
    }
}
