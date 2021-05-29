using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NeedsManager : MonoBehaviour
{
    public float prosperityValue = 10f;
    public float happinessValue = 10f;
    public float environmentValue = 10f;
    [SerializeField] float degenerationRate = 0.0005f;
    float degenerationThreshold;

    void Start()
    {
        degenerationThreshold = environmentValue / 2; // is reached when environement is at 50% or lower
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
            Debug.Log("the prosVal: " + prosperityValue + "the happVal: " + happinessValue + "the envVal: " + environmentValue);
        }
        else // faster decline of prosperity and happiness if environment-Rate is red
        {
            // let prosperity PowerbarValue blink red
            // let happiness PowerbarValue blink red
            prosperityValue = prosperityValue - degenerationRate * 2;
            happinessValue = happinessValue - degenerationRate * 2;
            environmentValue = environmentValue - degenerationRate; // stays the same
            Debug.Log("Threshold breached = the prosVal: " + prosperityValue + "the happVal: " + happinessValue + "the envVal: " + environmentValue);
        }
        
        // these 3 IF'S 
        if(prosperityValue <= prosperityValue * .5f)
        {
            //turn prosperityValue Powerbar red
            Debug.Log("the prosperityValue would now be red");
        }
        if (happinessValue <= happinessValue * .5f)
        {
            //turn happinessValue Powerbar red
            Debug.Log("the happinessValue would now be red");
        }
        if (environmentValue <= environmentValue * .5f)
        {
            //turn environmentValue Powerbar red
            Debug.Log("the environmentValue would now be red");
        }
    }
}
