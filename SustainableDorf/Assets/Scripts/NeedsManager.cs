using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NeedsManager : MonoBehaviour
{
    public float prosperityValue = 10f;
    public float happinessValue = 10f;
    public float environmentValue = 10f;
    [SerializeField] float degenerationRate = 0.001f;
    float degenerationThreshold;

    [SerializeField] Slider prosperityBar;
    [SerializeField] Slider happinessBar;
    [SerializeField] Slider environmentBar;
    [SerializeField] Image proImg;
    [SerializeField] Image hapImg;
    [SerializeField] Image envImg;

    Color proImgBaseColor;
    Color hapImgBaseColor;
    Color envImgBaseColor;

    void Start()
    {
        degenerationThreshold = environmentValue / 2; // is reached when environement is at 50% or lower
        //prosperityBar.maxValue = prosperityValue;
        //happinessBar.maxValue = happinessValue;
        //environmentBar.maxValue = environmentValue;
        proImgBaseColor = proImg.color;
        hapImgBaseColor = hapImg.color;
        envImgBaseColor = envImg.color;
        Debug.Log("the degenerationThreshold is now: " + degenerationThreshold);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(environmentValue >= degenerationThreshold)
        {
            prosperityValue = prosperityValue - degenerationRate;
            happinessValue = happinessValue - degenerationRate;
            environmentValue = environmentValue - degenerationRate;
            
            prosperityBar.value = prosperityValue;
            happinessBar.value = happinessValue;
            environmentBar.value = environmentValue;
            //Debug.Log("the prosVal: " + prosperityValue + "the happVal: " + happinessValue + "the envVal: " + environmentValue);
        }
        else // faster decline of prosperity and happiness if environment-Rate is red
        {
            // let prosperity PowerbarValue blink red
            // let happiness PowerbarValue blink red
            prosperityValue = prosperityValue - degenerationRate * 2;
            happinessValue = happinessValue - degenerationRate * 2;
            environmentValue = environmentValue - degenerationRate; // stays the same

            prosperityBar.value = prosperityValue;
            happinessBar.value = happinessValue;
            environmentBar.value = environmentValue;
            //Debug.Log("Threshold breached = the prosVal: " + prosperityValue + "the happVal: " + happinessValue + "the envVal: " + environmentValue);
        }
        
        //color changes:
        //PROSPERITY
        if(prosperityValue > degenerationThreshold)
        {
            proImg.color = proImgBaseColor;
        }else if(prosperityValue <= degenerationThreshold && prosperityValue > degenerationThreshold / 2)
        {
            proImg.color = new Color32(255, 116, 0, 150);
        }else if (prosperityValue <= degenerationThreshold / 2)
        {
            proImg.color = new Color32(188, 0, 0, 200);
        }
        //HAPPINESS
        if (happinessValue > degenerationThreshold)
        {
            hapImg.color = hapImgBaseColor;
        }
        else if (happinessValue <= degenerationThreshold && happinessValue > degenerationThreshold / 2)
        {
            hapImg.color = new Color32(255, 116, 0, 150);
        }
        else if (happinessValue <= degenerationThreshold / 2)
        {
            hapImg.color = new Color32(188, 0, 0, 200);
        }
        //ENVIRONMENT
        if (environmentValue > degenerationThreshold)
        {
            envImg.color = envImgBaseColor;
        }
        else if (environmentValue <= degenerationThreshold && environmentValue > degenerationThreshold / 2)
        {
            envImg.color = new Color32(255, 116, 0, 150);
        }
        else if (environmentValue <= degenerationThreshold / 2)
        {
            envImg.color = new Color32(188, 0, 0, 200);
        }
    }
}
