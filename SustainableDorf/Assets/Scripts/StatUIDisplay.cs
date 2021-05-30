using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatUIDisplay : MonoBehaviour
{
    [SerializeField] Slider prosperityBar;
    [SerializeField] Slider happinessBar;
    [SerializeField] Slider environmentBar;

    [SerializeField] Slider NEGprosperityBar;
    [SerializeField] Slider NEGhappinessBar;
    [SerializeField] Slider NEGenvironmentBar;

    float pS;
    float hS;
    float eS;
    public void CastStatsToUI(GameObject gO)
    {
        pS = gO.GetComponent<Stats>().prosperityStat;
        hS = gO.GetComponent<Stats>().happinessStat;
        eS = gO.GetComponent<Stats>().environmentStat;

        if (hS > 0f) // show happiness
        {
            happinessBar.value = hS;
            NEGhappinessBar.value = 0f;
        }
        else
        {
            happinessBar.value = 0f;
            NEGhappinessBar.value = hS * -1;
        }
        if (pS > 0f) // show prosperity
        {
            prosperityBar.value = pS;
            NEGprosperityBar.value = 0f;
        }
        else
        {
            prosperityBar.value = 0f;
            NEGprosperityBar.value = hS * -1;
        }
        if (eS > 0f) // show environment
        {
            environmentBar.value = eS;
            NEGenvironmentBar.value = 0f;
        }
        else
        {
            environmentBar.value = 0f;
            NEGenvironmentBar.value = eS * -1;
        }
    }
}
