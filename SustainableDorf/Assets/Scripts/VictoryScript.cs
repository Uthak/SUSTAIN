using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    GameObject SceneManager;
    NeedsManager NeedsManager;
    GameManager GameManager;
    GameObject Button;
    float enviromentLimit;
    float happinessLimit;
    float prosperityLimit;

    bool gameEnded = false;

    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        GameManager = SceneManager.GetComponent<GameManager>();
        NeedsManager = SceneManager.GetComponent<NeedsManager>();
        
        //enviromentLimit = NeedsManager.environmentValue * 0.8f;
        //happinessLimit = NeedsManager.happinessValue * 0.8f;
        //prosperityLimit = NeedsManager.prosperityValue * 0.8f;

        //Button für Screenshot suchen
        Button = GameObject.Find("Button");
        Button.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedsManager.tileCounter == 49)
        {
            if (NeedsManager.environmentDegenerationRate < 0f && NeedsManager.happinessDegenerationRate < 0f && NeedsManager.prosperityDegenerationRate < 0f)
            {
                Debug.Log("You Won");

                // this will fill up all bars really quick:
                NeedsManager.prosperityDegenerationRate *= 10;
                NeedsManager.happinessDegenerationRate *= 10;
                NeedsManager.environmentDegenerationRate *= 10;

                // execute winner screen HERE

                Button.SetActive(true);
            }else // = if any degeneration rate is still going downwards
            {
                Debug.Log("You Lost");

                // this will fill up all bars really quick:
                NeedsManager.prosperityDegenerationRate *= 10;
                NeedsManager.happinessDegenerationRate *= 10;
                NeedsManager.environmentDegenerationRate *= 10;

                // execute loser screen HERE

                Button.SetActive(true);
            }
        }
    }
}
