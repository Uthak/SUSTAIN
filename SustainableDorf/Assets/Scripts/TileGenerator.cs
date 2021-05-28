using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    //list/array of maptile-classe
    //list/array of maptiles inside said classes
    //list/array of characteristics [1-3],

    public void GenerateTile(/*characteristic1, characteristic2, characteristic3*/)
    {
        //get random tile (mesh) from rondom class (nature, factory, sustainability, social)
        //give this tile random values of the three characteristics 
        // if class A, majority of characteritics would be in Need1; etc 
        // --> it could happen that you get 3 factory tiles, and have to chose based on their stats that most suit your need
        Debug.Log("a tile was created");
    }
}
