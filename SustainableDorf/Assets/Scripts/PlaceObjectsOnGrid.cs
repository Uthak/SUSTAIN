using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsOnGrid : MonoBehaviour
{
    public Transform City;
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;
    public Vector3 smoothMousePosition;
    [SerializeField] private int height;
    [SerializeField] int width;

    private Vector3 mousePosition;
    private Node[,] nodes;
    private Plane plane;

    //My Var.
    [SerializeField] AudioSource PlopSound;
    [SerializeField] ObjFollowMouse ObjFollowMouse;
    [SerializeField] Stats Stats;
    //[SerializeField] Material placeable;
    //bool setFix = false;
    public bool isOnGrid;
    //bool activeFix = false;
    GameObject curObject;



    void Start()
    {
        CreateGrid();
        plane = new Plane(inNormal: Vector3.up, inPoint: transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        GetMousePositionOnGrid();
        if (!isOnGrid)
        {
            if (curObject != null)
            {
                curObject.transform.position = smoothMousePosition + new Vector3(x: 0, y: 0.5f, z: 0);
                curObject.GetComponent<BoxCollider>().enabled = false;
            }
        }

        foreach (var node in nodes)
        {
            if (node.activeFix == false)
            {
                if (node.obj.GetComponent<ActivateCell>().Active == true)
                {
                    node.isPlaceable = true;
                    node.activeFix = true;
                }
            }
        }
    }

    void GetMousePositionOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        
        if (plane.Raycast(ray,out var enter))
        {
            //Debug.Log("AAAA");
            mousePosition = ray.GetPoint(enter);
            smoothMousePosition = mousePosition;
            mousePosition.y = 0;
            mousePosition = Vector3Int.RoundToInt(mousePosition);
            foreach (var node in nodes)
            {
                
                    if (node.cellPosition == mousePosition && node.isPlaceable)
                    {
                        //Debug.Log("BBBB");
                        if (Input.GetMouseButtonUp(0))
                        {
                            //Debug.Log("CCCC");
                            if (onMousePrefab != null)
                            {
                                // wird gesetzt
                                curObject.GetComponent<BoxCollider>().enabled = true;
                                curObject.GetComponent<Stats>().UpdateStats();
                                curObject.GetComponent<ClickTile>().setFix = true;
                                Debug.Log("DDDD");
                                node.isPlaceable = false;
                                isOnGrid = true;
                                curObject.transform.position = node.cellPosition + new Vector3(x: 0, y: 0.1f, z: 0);
                                onMousePrefab = null;

                                
                                PlopSound.Play();
                            }
                        }
                    }
                
            }
        }
    }

    public void OnMouse(GameObject clickObject)
    {
        if (onMousePrefab == null && clickObject.GetComponent<ClickTile>().setFix == false)
        {
            curObject = clickObject;
            isOnGrid = false;
            onMousePrefab = gameObject.transform;
            //onMousePrefab = Instantiate(cube, mousePosition, Quaternion.identity);
        }
    }



    private void CreateGrid()
    {
        nodes = new Node[width, height];
        var name = 0;
        for (int i = 0; i < width; i++)
        {
            for (int j = 0; j < height; j++)
            {
                Vector3 worldPosition = new Vector3(x: i - width/2, y: 0, z: j - height/2);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell" + name;
                nodes[i, j] = new Node(isPlaceable: false, worldPosition, obj, activeFix: false);
                name++;
            }
        }

        //setze city in die mitte
        int half = width * height / 2;
        GameObject middle = GameObject.Find("Cell" + half);
        Debug.Log(middle);
        Instantiate(City, new Vector3(0,0,0) /*new Vector3(x: width/2, y: 0, z: height/2)*/, Quaternion.identity);
        nodes[width / 2, height / 2].isPlaceable = false;
        nodes[width / 2, height / 2].activeFix = true;
        City.GetComponent<ClickTile>().setFix = true;
    }
}



public class Node
{
    public bool isPlaceable;
    public Vector3 cellPosition;
    public Transform obj;
    public bool activeFix;

    public Node(bool isPlaceable, Vector3 cellPosition, Transform obj, bool activeFix)
    {
        this.isPlaceable = isPlaceable;
        this.cellPosition = cellPosition;
        this.obj = obj;
        this.activeFix = activeFix;
    }
}