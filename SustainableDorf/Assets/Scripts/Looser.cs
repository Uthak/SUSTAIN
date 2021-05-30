using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Looser : MonoBehaviour
{
    [SerializeField] GameObject looserUI;
    public void YouLose()
    {
        looserUI.SetActive(true);
    }
}
