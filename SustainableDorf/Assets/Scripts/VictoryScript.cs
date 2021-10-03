using UnityEngine;

public class VictoryScript : MonoBehaviour
{
    // winning and loosing the game are handled here
    // in general you win by having placed all 48 tiles AND having a positive growth rate of ALL stats
    // loosing early to to dropping a stat to zero is called by "NeedsManager"-script

    GameObject SceneManager;
    NeedsManager NeedsManager;
    GameManager GameManager;
    GameObject ScreenshotButton;
    [SerializeField] GameObject stats_UI;
    [SerializeField] GameObject game_UI;

    // input Podest Models to deactivate them
    [SerializeField] GameObject podest1;
    [SerializeField] GameObject podest2;
    [SerializeField] GameObject podest3;

    // this carries the input field for names (highscore)
    [SerializeField] GameObject enterName_UI;


    public GameObject youWonLogo; // public in case i want to turn off with a button
    [SerializeField] AudioSource youWonSound;

    public GameObject youLostLogo; // public in case i want to turn off with a button
    [SerializeField] AudioSource youLostSound;

    //[SerializeField] GameObject statUI; // why?
    //[SerializeField] GameObject gameUI; // why
    //[SerializeField] AudioSource windSound; // this may be cool for after loosing: just a dry wind blowing
    [SerializeField] AudioSource ambienteSound; // to turn off music


    public GameObject endGame_UI; // public so the "OpenHighscore_UI()" (from BUTTONS) can call it to close it

    public bool gameHasEnded = false; // public so the "NeedsManager" can read this information 

    void Start()
    {
        SceneManager = GameObject.Find("SceneManager");
        GameManager = SceneManager.GetComponent<GameManager>();
        NeedsManager = SceneManager.GetComponent<NeedsManager>();

        // search screenshot UI
        //ScreenshotButton = GameObject.Find("Screenshot_Button"); // name has to be exactly the same as in the actual game
        //ScreenshotButton.SetActive(false); // turns off the button in case its forgotten. Could be redundant
    }


    void Update()
    {
        // this whole IF statement is only for Quick Win while testing the game: --> winning by placing 1 tile
        if (GameManager.developerMode == true && GameManager.quickWin == true && NeedsManager.tileCounter == 1)
        {
            // quickly change all degeneration rates to negative (thus winning-ready)
            NeedsManager.prosperityDegenerationRate -= .005f;
            NeedsManager.happinessDegenerationRate -= .005f;
            NeedsManager.environmentDegenerationRate -= .005f;

            if (NeedsManager.environmentDegenerationRate < 0f && NeedsManager.happinessDegenerationRate < 0f && NeedsManager.prosperityDegenerationRate < 0f)
            {
                Debug.Log("You Won");
                Winner();
            }
        }

        // checks for real winning condition
        if (NeedsManager.tileCounter == 48)
        {
            if (NeedsManager.environmentDegenerationRate < 0f && NeedsManager.happinessDegenerationRate < 0f && NeedsManager.prosperityDegenerationRate < 0f)
            {
                if (gameHasEnded == false)
                {
                    Debug.Log("You Won"); // if working - delete me
                    Winner();
                }
            }else // = if any degeneration rate is still going downwards
            {
                if (gameHasEnded == false)
                {
                    Debug.Log("Sorry, You Lost"); // if working - delete me
                    Loser();
                }
            }
        }
    }


    public void Winner()
    {
        // Winner-function can be called only once
        gameHasEnded = true;

        //temporary solution - this will fill up all bars instantly:
        NeedsManager.prosperityDegenerationRate *= 10;
        NeedsManager.happinessDegenerationRate *= 10;
        NeedsManager.environmentDegenerationRate *= 10;
        // better would be filling them up in like 3 seconds
        // maxVal - currVal = openVal / 50 = X (how much Val per tic needed to fill openVal in 1 sec)
        // X / 3 = Y (how much Val per tic needed to fill openVal in 3 sec)
        // newDegRate = Y --> this should fill all bars, no matter how high they are, in 3 seconds each

        // 1. "You Won"/"you lose" Logo
        youWonLogo.SetActive(true);
        // play victory sound/melody
        ambienteSound.Stop(); //--> not turning off ambient music, because i guess its cool!?
        youWonSound.Play();
        // invoke: open proceed UI: (with dramatic delay
        Invoke("EndGame_UI", 3f); // probably length of the victory sound. Adjust manually
    }
    public void Loser()
    {
        // Loser-function can be called only once
        gameHasEnded = true;

        //temporary solution - this will fill up all bars instantly:
        NeedsManager.prosperityDegenerationRate *= 10;
        NeedsManager.happinessDegenerationRate *= 10;
        NeedsManager.environmentDegenerationRate *= 10;
        // better would be filling them up in like 3 seconds
        // maxVal - currVal = openVal / 50 = X (how much Val per tic needed to fill openVal in 1 sec)
        // X / 3 = Y (how much Val per tic needed to fill openVal in 3 sec)
        // newDegRate = Y --> this should fill all bars, no matter how high they are, in 3 seconds each
        
        // 1. "You Won"/"you lose" Logo
        youLostLogo.SetActive(true);
        // play victory sound/melody
        ambienteSound.Stop();
        youLostSound.Play();
        // invoke: open proceed UI: (with dramatic delay
        Invoke("EndGame_UI", 3f); // probably length of the victory sound. Adjust manually
    }

    // overlay called once Logo & Sound were shown
    // this GO sits in EndGameScreen > Highscore_UI > Highscore_Background 
    [SerializeField] GameObject Highscore_carrying_GO;
    void EndGame_UI()
    {
        stats_UI.SetActive(false);
        game_UI.SetActive(false);
        SceneManager.GetComponent<TileGenerator>().DestroyRemainingTiles();
        podest1.SetActive(false);
        podest2.SetActive(false);
        podest3.SetActive(false);
        endGame_UI.SetActive(true);

        // this checks if a highscore was achieved/there was still open entries. If so, it opens the name input-field
        SceneManager.GetComponent<CalculateHighscore>().Calculate();
        Highscore_carrying_GO.GetComponent<Highscore>().AllowHighscore();
        if (Highscore_carrying_GO.GetComponent<Highscore>().allowedHS)
        {
            enterName_UI.SetActive(true);
        }
    }
}





//--------------------------------
// safeties:

/*using System.Collections;
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

    [SerializeField] GameObject endGameScreen_UI;
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
        ScreenshotButton = GameObject.Find("Screenshot_Button"); // name has to be exactly the same as in the actual game
        ScreenshotButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.developerMode == true && GameManager.quickWin == true && NeedsManager.tileCounter == 1)
        {
            NeedsManager.prosperityDegenerationRate -= .005f;
            NeedsManager.happinessDegenerationRate -= .005f;
            NeedsManager.environmentDegenerationRate -= .005f;

                if (NeedsManager.environmentDegenerationRate < 0f && NeedsManager.happinessDegenerationRate < 0f && NeedsManager.prosperityDegenerationRate < 0f)
                {
                    Debug.Log("You Won");

                    //highscoreScript.CalculateHighscore();
                    endGameScreen_UI.SetActive(true);

                    // execute winner screen HERE

                    ScreenshotButton.SetActive(true);
                if ((NeedsManager.prosperityValue && NeedsManager.happinessValue && NeedsManager.environmentValue) >= GameManager.baseStatValue)// && NeedsManager.happinessValue && NeedsManager.environmentValue))
                {
                    Time.timeScale = 0;
                }
                }
            
        }

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
        endGameScreen_UI.SetActive(true);

        // execute winner screen HERE

        ScreenshotButton.SetActive(true);
    }
    else // = if any degeneration rate is still going downwards
    {
        Debug.Log("You Lost");

        // this will fill up all bars really quick:
        NeedsManager.prosperityDegenerationRate *= 10;
        NeedsManager.happinessDegenerationRate *= 10;
        NeedsManager.environmentDegenerationRate *= 10;

        //highscoreScript.CalculateHighscore();
        endGameScreen_UI.SetActive(true);

        // execute loser screen HERE

        ScreenshotButton.SetActive(true);
    }
}
    }
}*/

