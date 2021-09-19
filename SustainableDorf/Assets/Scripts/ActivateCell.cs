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

    //speicher Original Farbe
    Color OriginalColor;
    public Material Material1;
    public Material Material2;
    Renderer[] children;

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
        if (SceneManager.GetComponent<PlaceObjectsOnGrid>().onMousePrefab != null)
        {
            
            //blau beim hovern
            gameObject.GetComponent<Renderer>().material.color = Color.blue;
        

            //zählt die jeweiligen Tags der Nachbarn
            //Debug.Log(neighbors.Count);
            foreach (var tile in neighbors)
            {
                children = tile.GetComponentsInChildren<Renderer>();
                //tile.GetComponentsInChildren<>(true);
                string str = tile.tag;
                switch (str)
                {
                    case "factory":
                        factory ++;
                        children[1].GetComponent<Renderer>().material.color = Color.gray;//new Color(1f, 0.85f, 0.35f);//Boden von Nachbarn wird gefärbt

                        break;
                    case "social":
                        social++;
                        children[1].GetComponent<Renderer>().material.color = Color.magenta;

                        break;
                    case "nature":
                        nature++;
                        children[1].GetComponent<Renderer>().material.color = Color.green;

                        break;
                    case "sustainable":
                        sustainable++;
                        children[1].GetComponent<Renderer>().material.color = Color.blue;

                        break;
                    default:
                        Debug.Log("Fehler");
                        break;
                }
            }
        
            SceneManager.GetComponent<PlaceObjectsOnGrid>().curObject.GetComponent<Stats>().NeighborEffect(factory, social, nature, sustainable);
            //Debug.Log("factory" + factory);
            //Debug.Log("social" + social);
            //Debug.Log("nature" + nature);
            //Debug.Log("sustainable" + sustainable);
            factory = 0;
            social = 0;
            nature = 0;
            sustainable = 0;
        }
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<MeshRenderer>().material = Material1;

        foreach (var tile in neighbors)
        {
            children = tile.GetComponentsInChildren<Renderer>();
            //tile.GetComponentsInChildren<>(true);
            string str = tile.tag;
            switch (str)
            {
                case "factory":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                case "social":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                case "nature":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                case "sustainable":
                    
                    children[1].GetComponent<Renderer>().material = Material2;

                    break;
                default:
                    Debug.Log("Fehler");
                    break;
            }
        }
    }
}
