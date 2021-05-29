using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileGenerator : MonoBehaviour
{
    public GameObject[] arrayOfAllTilePrefabs;

    public Transform spawnPos1;
    public Transform spawnPos2;
    public Transform spawnPos3;
    int randomInt;

    public GameObject tile1;
    public GameObject tile2;
    public GameObject tile3;
    [SerializeField] float respawnDelay = 2f;

    private void Start()
    {

        Invoke("GenerateTile1", respawnDelay);
        Invoke("GenerateTile2", respawnDelay);
        Invoke("GenerateTile3", respawnDelay);
    }

    private void Update() // for testing 
    {
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            tile1.GetComponent<Stats>().UpdateStats();
            Debug.Log("Mouse was pressed");
        }
    }
    public void NextSet()
    {
        Invoke("GenerateTile1", respawnDelay);
        Invoke("GenerateTile2", respawnDelay);
        Invoke("GenerateTile3", respawnDelay);
    }

    public void GenerateTile1()
    {
        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile1 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos1.position, spawnPos1.rotation);
        tile1.GetComponent<Stats>().RandomizeStats(tile1);

        Debug.Log("a tile was created");
    }
    public void GenerateTile2()
    {
        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile2 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos2.position, spawnPos2.rotation);
        tile2.GetComponent<Stats>().RandomizeStats(tile2);

        Debug.Log("a tile was created");
    }
    public void GenerateTile3()
    {
        randomInt = Random.Range(0, arrayOfAllTilePrefabs.Length);
        tile3 = Instantiate(arrayOfAllTilePrefabs[randomInt], spawnPos3.position, spawnPos3.rotation);
        tile3.GetComponent<Stats>().RandomizeStats(tile3);

        Debug.Log("a tile was created");
    }
    public void DestroyRemainingTiles()
    {
        if(tile1.GetComponent<Stats>().wasPlaced == false)
        {
            //Destroy(tile1);
            tile1 = null;
        }
        if (tile2.GetComponent<Stats>().wasPlaced == false)
        {
            Destroy(tile2);
        }
        if (tile3.GetComponent<Stats>().wasPlaced == false)
        {
            Destroy(tile3);
        }
        return;
    }
}
