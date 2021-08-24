using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NeedsManager : MonoBehaviour
{
    [SerializeField] bool showDegenerationRates = true;
    [SerializeField] TextMeshProUGUI prosperityDegenerationRateField;
    [SerializeField] TextMeshProUGUI happinessDegenerationRateField;
    [SerializeField] TextMeshProUGUI environmentDegenerationRateField;

    public bool usingContinuousValues = true; // wird nur in "Stats"-Klasse benutzt, aber hier abgefragt, weil die Stats variable nur auf den Tiles anliegt
    public bool usingOneTimeValues = false; // wird nur in "Stats"-Klasse benutzt, aber hier abgefragt, weil die Stats variable nur auf den Tiles anliegt
    public float statDisplayMultiplicator = 200f; // wird nur in "Stats"-Klasse benutzt, aber hier abgefragt, weil die Stats variable nur auf den Tiles anliegt

    public float prosperityValue = 10f;
    public float happinessValue = 10f;
    public float environmentValue = 10f;

    public float prosperityDegenerationRate = 0.1f;
    public float happinessDegenerationRate = 0.1f;
    public float environmentDegenerationRate = 0.1f;

    public float accelleratedDegenerationRate = 1.5f;
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
        degenerationThreshold = environmentValue / 2;

        proImgBaseColor = proImg.color;
        hapImgBaseColor = hapImg.color;
        envImgBaseColor = envImg.color;
        //Debug.Log("the degenerationThreshold is now: " + degenerationThreshold);
    }

    void FixedUpdate()
    {
            if (environmentValue >= degenerationThreshold | prosperityValue >= degenerationThreshold | happinessValue >= degenerationThreshold)
            {
                prosperityValue = prosperityValue - prosperityDegenerationRate;
                happinessValue = happinessValue - happinessDegenerationRate;
                environmentValue = environmentValue - environmentDegenerationRate;

                prosperityBar.value = prosperityValue;
                happinessBar.value = happinessValue;
                environmentBar.value = environmentValue;
                //Debug.Log("the prosVal: " + prosperityValue + "the happVal: " + happinessValue + "the envVal: " + environmentValue);
            }
            else // faster decline of other stats when any other stat is red
            {
                prosperityValue = prosperityValue - prosperityDegenerationRate * accelleratedDegenerationRate;
                happinessValue = happinessValue - happinessDegenerationRate * accelleratedDegenerationRate;
                environmentValue = environmentValue - environmentDegenerationRate * accelleratedDegenerationRate;

                prosperityBar.value = prosperityValue;
                happinessBar.value = happinessValue;
                environmentBar.value = environmentValue;
                //Debug.Log("Threshold breached = the prosVal: " + prosperityValue + "the happVal: " + happinessValue + "the envVal: " + environmentValue);
            }

            // call LOOSER-function
            if (prosperityValue <= 0f || happinessValue <= 0f || environmentValue <= 0f)
            {
                GetComponent<Looser>().YouLose();
            }

            //color changes:
            //PROSPERITY
            if (prosperityValue > degenerationThreshold)
            {
                proImg.color = proImgBaseColor; // green
            }
            else if (prosperityValue <= degenerationThreshold && prosperityValue > degenerationThreshold / 2)
            {
                proImg.color = new Color32(255, 116, 0, 150); // orange
            }
            else if (prosperityValue <= degenerationThreshold / 2)
            {
                proImg.color = new Color32(188, 0, 0, 200); // red
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

            if (showDegenerationRates == true)
            {
                GameObject SceneManager = GameObject.Find("SceneManager");

                prosperityDegenerationRateField.text = "prospRate " + SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate.ToString();
                happinessDegenerationRateField.text = "hapRate " + SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate.ToString();
                environmentDegenerationRateField.text = "envRate " + SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate.ToString();
            }
    }

}
    

