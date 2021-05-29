using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjFollowMouse : MonoBehaviour
{
    private PlaceObjectsOnGrid placeObjectsOnGrid;
    public bool isOnGrid;

    // Start is called before the first frame update
    void Start()
    {
        placeObjectsOnGrid = FindObjectOfType<PlaceObjectsOnGrid>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isOnGrid)
        {
            transform.position = placeObjectsOnGrid.smoothMousePosition + new Vector3(x: 0, y: 0.5f, z: 0);
        }
    }
}
