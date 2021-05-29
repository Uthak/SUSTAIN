using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjectsOnGrid : MonoBehaviour
{
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
    [SerializeField] ObjFollowMouse ObjFollowMouse;
    [SerializeField] Stats Stats;
    //bool setFix = false;
    public bool isOnGrid;
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
                        Debug.Log("CCCC");
                        if (onMousePrefab != null)
                        {
                            // hier update Stats aufrufen
                            curObject.GetComponent<Stats>().UpdateStats();
                            curObject.GetComponent<ClickTile>().setFix = true;
                            Debug.Log("DDDD");
                            node.isPlaceable = false;
                            isOnGrid = true;
                            curObject.transform.position = node.cellPosition + new Vector3(x: 0, y: 0.1f, z: 0);
                            onMousePrefab = null;
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
                Vector3 worldPosition = new Vector3(x: i, y: 0, z: j);
                Transform obj = Instantiate(gridCellPrefab, worldPosition, Quaternion.identity);
                obj.name = "Cell" + name;
                nodes[i, j] = new Node(isPlaceable: true, worldPosition, obj);
                name++;
            }
        }
    }
}



public class Node
{
    public bool isPlaceable;
    public Vector3 cellPosition;
    public Transform obj;

    public Node(bool isPlaceable, Vector3 cellPosition, Transform obj)
    {
        this.isPlaceable = isPlaceable;
        this.cellPosition = cellPosition;
        this.obj = obj;
    }
}