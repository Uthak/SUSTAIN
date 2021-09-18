using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatUIDisplay : MonoBehaviour
{
    [SerializeField] Slider prosperityBar;
    [SerializeField] Slider happinessBar;
    [SerializeField] Slider environmentBar;

    [SerializeField] Slider NEGprosperityBar;
    [SerializeField] Slider NEGhappinessBar;
    [SerializeField] Slider NEGenvironmentBar;

    [SerializeField] TextMeshProUGUI nameField;

    float pS;
    float hS;
    float eS;
    public void CastStatsToUI(GameObject gO, string t, float a, float b, float c)
    {
        /*pS = gO.GetComponent<Stats>().prosperityStat;
        hS = gO.GetComponent<Stats>().happinessStat;
        eS = gO.GetComponent<Stats>().environmentStat;*/
        pS = a; //gO.GetComponent<Stats>().prosperityStat;
        hS = c; //gO.GetComponent<Stats>().happinessStat;
        eS = b; //gO.GetComponent<Stats>().environmentStat;

        if(t == "nature")
        {
            nameField.text = "Nature";
        }
        if (t == "factory")
        {
            nameField.text = "Factory";
        }
        if (t == "social")
        {
            nameField.text = "Social";
        }
        if (t == "sustainable")
        {
            nameField.text = "Sustainable";
        }
        if (t == "city")
        {
            nameField.text = "Pagotopia"; // "\n" to add another line 
        }

        if (hS > 0f) // show happiness
        {
            happinessBar.value = hS;
            NEGhappinessBar.value = 0f;
        }else
        {
            happinessBar.value = 0f;
            NEGhappinessBar.value = hS * -1;
        }
        
        if (pS > 0f) // show prosperity
        {
            prosperityBar.value = pS;
            NEGprosperityBar.value = 0f;
        }else
        {
            prosperityBar.value = 0f;
            NEGprosperityBar.value = pS * -1;
        }
        
        if (eS > 0f) // show environment
        {
            environmentBar.value = eS;
            NEGenvironmentBar.value = 0f;
        }else
        {
            environmentBar.value = 0f;
            NEGenvironmentBar.value = eS * -1;
        }
    }

    public void ResetStatBars()
    {
        prosperityBar.value = 0f;
        happinessBar.value = 0f;
        environmentBar.value = 0f;

        NEGprosperityBar.value = 0f;
        NEGhappinessBar.value = 0f;
        NEGenvironmentBar.value = 0f;

        nameField.text = null;
    }
}
