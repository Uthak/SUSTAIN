using UnityEngine;
using UnityEngine.UI;

public class NeedsManager : MonoBehaviour
{
    public float prosperityValue;
    public float happinessValue;
    public float environmentValue;

    public float prosperityDegenerationRate;
    public float happinessDegenerationRate;
    public float environmentDegenerationRate;

    float accelleratedDegenerationRate;
    float degenerationThreshold;
    float baseStatValue;

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
        GameObject SceneManager = GameObject.Find("SceneManager");

        // determines the degeneration Threshold as % of the maximum stat-value
        degenerationThreshold = SceneManager.GetComponent<GameManager>().degenerationThreshold / 100f * SceneManager.GetComponent<GameManager>().baseStatValue;
        
        // gets the accellerated degeneration-rate value from "GameManager"-class
        accelleratedDegenerationRate = SceneManager.GetComponent<GameManager>().accelleratedDegenerationRate;
        
        // determines basic degeneration rates
        prosperityDegenerationRate = SceneManager.GetComponent<GameManager>().globalDegenerationRate;
        happinessDegenerationRate = SceneManager.GetComponent<GameManager>().globalDegenerationRate;
        environmentDegenerationRate = SceneManager.GetComponent<GameManager>().globalDegenerationRate;

        // determines basic stat values
        baseStatValue = SceneManager.GetComponent<GameManager>().baseStatValue;
        prosperityValue = baseStatValue;
        happinessValue = baseStatValue;
        environmentValue = baseStatValue;

        // determines basic stat-bar colors (green)
        proImgBaseColor = proImg.color;
        hapImgBaseColor = hapImg.color;
        envImgBaseColor = envImg.color;
    }

    // this updates the game stats at a rate of 50/s
    void FixedUpdate()
    {
            // as long as stat values are above the degeneration-threshold the normal degeneration-rate applies
            if (environmentValue >= degenerationThreshold | prosperityValue >= degenerationThreshold | happinessValue >= degenerationThreshold)
            {
                // updates stat values 50/s
                prosperityValue -= prosperityDegenerationRate;
                happinessValue -= happinessDegenerationRate;
                environmentValue -= environmentDegenerationRate;
            }
            // once below the degeneration-threshold the accellerated degeneration-rate applies to all other stats
            else if(environmentValue < degenerationThreshold)
            {
                prosperityValue -= prosperityDegenerationRate * accelleratedDegenerationRate;
                happinessValue -= happinessDegenerationRate * accelleratedDegenerationRate;
            }else if(happinessValue < degenerationThreshold)
            {
                prosperityValue -= prosperityDegenerationRate * accelleratedDegenerationRate;
                environmentValue -= environmentDegenerationRate * accelleratedDegenerationRate;
            }else if (prosperityValue < degenerationThreshold)
            {
                happinessValue -= happinessDegenerationRate * accelleratedDegenerationRate;
                environmentValue -= environmentDegenerationRate * accelleratedDegenerationRate;
            }

        // displays updated stat values in UI
        // was previously in the if/else statement above. if this works - DELETE this comment! (!!!!!!)
        prosperityBar.value = prosperityValue;
        happinessBar.value = happinessValue;
        environmentBar.value = environmentValue;

        // ensures that the stat caps cannot be surpassed
        if (prosperityValue > baseStatValue)
        {
            prosperityValue = baseStatValue;
        }
        if (happinessValue > baseStatValue)
        {
            happinessValue = baseStatValue;
        }
        if (environmentValue > baseStatValue)
        {
            environmentValue = baseStatValue;
        }

        // call "LOOSER"-script to end game if any stat value drops to 0 (or below)
        if (prosperityValue <= 0f || happinessValue <= 0f || environmentValue <= 0f)
        {
            GetComponent<Looser>().YouLose();
        }

            //color changes:
            // 50.01%-100% green
            // 25.01%-50% orange
            // 0%-25% red

            //PROSPERITY
            if (prosperityValue > (baseStatValue / 2))
            {
                proImg.color = proImgBaseColor; // green
            }else if (prosperityValue <= (baseStatValue / 2) && prosperityValue > (baseStatValue / 4))
            {
                proImg.color = new Color32(255, 116, 0, 150); // orange
            }else if (prosperityValue <= (baseStatValue / 4))
            {
                proImg.color = new Color32(188, 0, 0, 200); // red
            }

            //HAPPINESS
            if (happinessValue > (baseStatValue / 2))
            {
                hapImg.color = hapImgBaseColor;
            }else if (happinessValue <= (baseStatValue / 2) && happinessValue > (baseStatValue / 4))
            {
                hapImg.color = new Color32(255, 116, 0, 150);
            }else if (happinessValue <= (baseStatValue / 4))
            {
                hapImg.color = new Color32(188, 0, 0, 200);
            }

            //ENVIRONMENT
            if (environmentValue > (baseStatValue / 2))
            {
                envImg.color = envImgBaseColor;
            }else if (environmentValue <= (baseStatValue / 2) && environmentValue > (baseStatValue / 4))
            {
                envImg.color = new Color32(255, 116, 0, 150);
            }else if (environmentValue <= (baseStatValue / 4))
            {
                envImg.color = new Color32(188, 0, 0, 200);
            }
    }
}
    

