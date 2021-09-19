using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    GameObject SceneManager;
    NeedsManager NeedsManager;
    GameManager GameManager;
    GameObject ScreenshotButton;
    float enviromentLimit;
    float happinessLimit;
    float prosperityLimit;

    //[SerializeField] endGameScreen_UI;
    //Highscore = highscoreScript;

    bool gameEnded = false;

    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        GameManager = SceneManager.GetComponent<GameManager>();
        NeedsManager = SceneManager.GetComponent<NeedsManager>();
        
        //highscoreScript = SceneManager.GetComponent<Highscore>();
        //endGameScreen_UI.SetActive(false);


        //enviromentLimit = NeedsManager.environmentValue * 0.8f;
        //happinessLimit = NeedsManager.happinessValue * 0.8f;
        //prosperityLimit = NeedsManager.prosperityValue * 0.8f;

        //Button für Screenshot suchen
        ScreenshotButton = GameObject.Find("Button");
        ScreenshotButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (NeedsManager.tileCounter == 48)
        {
            if (NeedsManager.environmentDegenerationRate < 0f && NeedsManager.happinessDegenerationRate < 0f && NeedsManager.prosperityDegenerationRate < 0f)
            {
                Debug.Log("You Won");

                // this will fill up all bars really quick:
                NeedsManager.prosperityDegenerationRate *= 10;
                NeedsManager.happinessDegenerationRate *= 10;
                NeedsManager.environmentDegenerationRate *= 10;

                //highscoreScript.CalculateHighscore();
                //endGameScreen_UI.SetActive(true);

                // execute winner screen HERE

                ScreenshotButton.SetActive(true);
            }else // = if any degeneration rate is still going downwards
            {
                Debug.Log("You Lost");

                // this will fill up all bars really quick:
                NeedsManager.prosperityDegenerationRate *= 10;
                NeedsManager.happinessDegenerationRate *= 10;
                NeedsManager.environmentDegenerationRate *= 10;

                //highscoreScript.CalculateHighscore();
                //endGameScreen_UI.SetActive(true);

                // execute loser screen HERE

                ScreenshotButton.SetActive(true);
            }
        }
    }
}
