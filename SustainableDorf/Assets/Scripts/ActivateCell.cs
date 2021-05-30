using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCell : MonoBehaviour
{
    [SerializeField] Material placeable;
    public bool Active = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("hey");
        Active = true;
        //gameObject.GetComponent<Renderer>().material = placeable;
        gameObject.GetComponent<Renderer>().enabled = true;
    }
}
