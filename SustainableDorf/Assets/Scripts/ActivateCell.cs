using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Zellen werden sichbar sobald ein MapTiles gesetzt wird

public class ActivateCell : MonoBehaviour
{
    [SerializeField] Material placeable;
    public bool Active = false;

    public List<GameObject> neighbors = new List<GameObject>();
    int factory;
    int social;
    int nature;
    int sustainable;
    GameObject SceneManager;

    private void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log("hey");
        Active = true;
        //gameObject.GetComponent<Renderer>().material = placeable;
        gameObject.GetComponent<Renderer>().enabled = true;

        //liste aller benachbarten Zellen
        if (other.gameObject.CompareTag("nature") || other.gameObject.CompareTag("factory") || other.gameObject.CompareTag("social") || other.gameObject.CompareTag("sustainable"))
        {
            neighbors.Add(other.gameObject);
            
        }
    }

    private void OnMouseOver()
    {
        
        //Debug.Log(neighbors.Count);
        foreach (var tile in neighbors)
        {
            string str = tile.tag;
            switch (str)
            {
                case "factory":
                    factory ++;
                    
                    break;
                case "social":
                    social++;
                    
                    break;
                case "nature":
                    nature++;
                    
                    break;
                case "sustainable":
                    sustainable++;
                    
                    break;
                default:
                    Debug.Log("Fehler");
                    break;
            }
        }
        
        SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().NeighborEffect(factory, social, nature, sustainable);
        Debug.Log("factory" + factory);
        Debug.Log("social" + social);
        Debug.Log("nature" + nature);
        Debug.Log("sustainable" + sustainable);
        factory = 0;
        social = 0;
        nature = 0;
        sustainable = 0;
    }
}
