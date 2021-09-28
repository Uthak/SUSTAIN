using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    [SerializeField] AudioSource standardClick;
    [SerializeField] AudioSource exitClick;
    [SerializeField] AudioSource doorClose;
    public AudioSource kickingSound;
    public AudioSource mooh;


    public AudioSource soundOfPaper;

    [SerializeField] GameObject pauseScreen_UI;
    [SerializeField] GameObject areYouSure_UI;
    //[SerializeField] GameObject areYouSure_BackToMain_UI;
    //[SerializeField] GameObject areYouSure_Quit_UI;


    [SerializeField] GameObject pop_up_screen_1;
    [SerializeField] GameObject pop_up_screen_2;

    [SerializeField] GameObject jan;
    [SerializeField] GameObject sophie;
    [SerializeField] GameObject paula;
    [SerializeField] GameObject felix;

    GameObject lastClickedNameButton;

    [SerializeField] GameObject credits_PopUp;
    public TextMeshProUGUI developerName_TextField;
    public TextMeshProUGUI developerSkills_TextField;
    bool aCreditsButtonHasBeenPressed = false;

    bool gameIsPaused = false;
    // used to check if the player can pause the scene he/she is in
    bool canBePaused = false;

    // respawn-blink-timer; used by KickOnClick-script
    public float blinkIntervall = .3f;

    // this Start function is only to turn off your pop_ups in case you forgot
    private void Start()
    {
        if (pop_up_screen_1 != null)
        {
            pop_up_screen_1.SetActive(false);
        }
        if (pop_up_screen_2 != null)
        {
            pop_up_screen_2.SetActive(false);
        }

        // checks if player is currently in the actual game in order to unlock "pause"-ability
        if (SceneManager.GetActiveScene().name == "Level_1")
        {
            canBePaused = true;
        }

    }
    // Pause and open a screen while playing, enabling leaving the game midgame
    void Update()
    {
        if(gameIsPaused == false && canBePaused)
        {
            if (Input.GetKeyDown(KeyCode.Escape) | Input.GetKeyDown(KeyCode.Space))
            {
                standardClick.Play();
                PauseGame();
            }
        }
        // this part doesnt work: likely because time stops moving and thus no updates happen = cant see key being pressed?!
        /*else
        {
            if (Input.GetKey(KeyCode.Escape | KeyCode.Space))
            {
                //standardClick.Play();
                ResumeGame();
            }
        }*/
    }
    void PauseGame()
    {
        // this doesnt need sound, as this gets called WITH sound in the Update
        gameIsPaused = true;
        Time.timeScale = 0;
        pauseScreen_UI.SetActive(true);
    }
    public void ResumeGame()
    {
        standardClick.Play();
        gameIsPaused = false;
        if (pauseScreen_UI != null)
        {
            pauseScreen_UI.SetActive(false);
        }
        Time.timeScale = 1;
    }

    public void StartTutorial()
    {
        standardClick.Play();
        SceneManager.LoadScene("Tutorial_1");
    }

    public void StartGame()
    {
        standardClick.Play();
        SceneManager.LoadScene("Level_1");
        Debug.Log("Start Button has been pressed");
        ResumeGame();
    }

    public void Restart()
    {
        standardClick.Play();
        ResumeGame();
        // this restarts current Level
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Debug.Log("this scene was just restarted");
    }

    // Opens Credit Screen
    public void Credits()
    {
        standardClick.Play();
        SceneManager.LoadScene("Credits_UI");
    }

    // Goes back to main menu
    public void BackToMain()
    {
        standardClick.Play();
        ResumeGame();
        SceneManager.LoadScene("Start_UI");
        /*if (SceneManager.GetActiveScene().name != "Start_UI")
        {
            SceneManager.LoadScene("Start_UI");
        }
        else
        {
            creditScreen.SetActive(false);
            mainMenu.SetActive(true);
        }*/
        //Debug.Log("trying to go back to Main");
    }

    // Exits the game when asked to confirm
    public void YesIAmSure()
    {
        standardClick.Play();
        Debug.Log("The game would now end in built game!"); // this is to check operation while in Editor
        Application.Quit(); // only works when built
    }
    public void NoIAmNot()
    {
        standardClick.Play();
        areYouSure_UI.SetActive(false);
    }

    // launches UI asking to confirm if you REALLY wanna leave the game
    public void QuitGame()
    {
        //exitClick.Play(); // should play door closing & clicking sound
        //doorClose.Play();
        standardClick.Play();
        areYouSure_UI.SetActive(true);
    }

    public void Options_UI()
    {
        standardClick.Play();
        SceneManager.LoadScene("Options_UI");
        Debug.Log("Options-Button was pressed");
    }
    public void Highscore_UI()
    {
        standardClick.Play();
        SceneManager.LoadScene("Highscore_UI");
        Debug.Log("Highscore-Button was pressed");

    }


    // ----------------------------------------------

    // these are completly optional UI_Screens you can assign via [SerializeFields]
    public void Open_PoP_Up_Screen_1()
    {
        if (pop_up_screen_1 != null)
        {
            standardClick.Play();
            Time.timeScale = 0;
            pop_up_screen_1.SetActive(true);
        }else
        {
            Debug.Log("You want to open pop_up_screen_1");
            Debug.Log("Make sure that you put the desired pop-up in the corresponding slot of the \"Buttons\"-Script in the \"SceneManager\"-GameObject in the Editor");
            Debug.Log("Be aware that there is pop_up_screen_1 & pop_up_screen_2! --> this is targeting Nr.: 1");
        }
    }
    public void Close_PoP_Up_Screen_1()
    {
        if (pop_up_screen_1 != null)
        {
            standardClick.Play();
            Time.timeScale = 1;
            pop_up_screen_1.SetActive(false);
        }
    }
    public void Open_PoP_Up_Screen_2()
    {
        if (pop_up_screen_2 != null)
        {
            standardClick.Play();
            Time.timeScale = 0;
            pop_up_screen_2.SetActive(true);
        }else
        {
            Debug.Log("You want to open pop_up_screen_2");
            Debug.Log("Make sure that you put the desired pop-up in the corresponding slot of the \"Buttons\"-Script in the \"SceneManager\"-GameObject in the Editor");
            Debug.Log("Be aware that there is pop_up_screen_1 & pop_up_screen_2! --> this is targeting Nr.: 2");
        }
    }
    public void Close_PoP_Up_Screen_2()
    {
        if (pop_up_screen_2 != null)
        {
            standardClick.Play();
            Time.timeScale = 1;
            pop_up_screen_2.SetActive(false);
        }
    }

    public void StopWobble()
    {
        lastClickedNameButton.GetComponent<KickOnClick>().wobbleEnabled = false;
    }

    // these are the credits buttons only. they are kind of pointless all together
    // untill I add descriptions of contributions
    public void JanKuh()
    {
        jan.GetComponent<KickOnClick>().wobbleEnabled = true;
        lastClickedNameButton = jan;
        Invoke("StopWobble", 3f);

        developerName_TextField.text = "Jan";
        developerSkills_TextField.text = "Game Design and Code";
        /*if (aCreditsButtonHasBeenPressed == false)
        {
            aCreditsButtonHasBeenPressed = true;
            developerName_TextField.text = "Jan";
            developerSkills_TextField.text = "Game Design and Code";
        }else
        {
            aCreditsButtonHasBeenPressed = false;
            developerName_TextField.text = "";
            developerSkills_TextField.text = "";
        }*/
    }
    public void SophieKuh()
    {
        sophie.GetComponent<KickOnClick>().wobbleEnabled = true;
        lastClickedNameButton = sophie;
        Invoke("StopWobble", 3f);

        developerName_TextField.text = "Sophie";
        developerSkills_TextField.text = "Art, 3D and Music";
        /*if (aCreditsButtonHasBeenPressed == false)
        {
            aCreditsButtonHasBeenPressed = true;
            developerName_TextField.text = "Sophie";
            developerSkills_TextField.text = "Art, 3D and Music";
        }else
        {
            aCreditsButtonHasBeenPressed = false;
            developerName_TextField.text = "";
            developerSkills_TextField.text = "";
        }*/
    }
    public void PaulaKuh()
    {
        paula.GetComponent<KickOnClick>().wobbleEnabled = true;
        lastClickedNameButton = paula;
        Invoke("StopWobble", 3f);
        
        developerName_TextField.text = "Paula";
        developerSkills_TextField.text = "Art, 3D and VFX";
        /*if (aCreditsButtonHasBeenPressed == false)
        {
            aCreditsButtonHasBeenPressed = true;
            developerName_TextField.text = "Paula";
            developerSkills_TextField.text = "Art, 3D and VFX";
        }else
        {
            aCreditsButtonHasBeenPressed = false;
            developerName_TextField.text = "";
            developerSkills_TextField.text = "";
        }*/
    }
    public void FelixKuh()
    {
        //aCreditsButtonHasBeenPressed = false;
        //aCreditsButtonHasBeenPressed = true;
        felix.GetComponent<KickOnClick>().wobbleEnabled = true;
        lastClickedNameButton = felix;
        Invoke("StopWobble", 3f);

        developerName_TextField.text = "Felix";
        developerSkills_TextField.text = "Game Design, UI's and Code";
        /*if (aCreditsButtonHasBeenPressed == false)
        {
            aCreditsButtonHasBeenPressed = true;
            developerName_TextField.text = "Felix";
            developerSkills_TextField.text = "Game Design, UI's and Code";
        }else
        {
            aCreditsButtonHasBeenPressed = false;
            developerName_TextField.text = "";
            developerSkills_TextField.text = "";
        }   */
    }
}





// safeties below... delete when not needed!


/*[SerializeField] GameObject mainMenu;
    [SerializeField] GameObject creditScreen;

    //[SerializeField] GameObject sophie;
    //[SerializeField] GameObject paula;
    //[SerializeField] GameObject jan;
    //[SerializeField] GameObject felix;

    //[SerializeField] GameObject[] arrayOfFood;

    [SerializeField] AudioSource standardClick;
    [SerializeField] AudioSource exitClick;
    [SerializeField] AudioSource doorClose;

    //[SerializeField] TMP_Text sophieText;
    [SerializeField] GameObject sophieButton;
    Vector3 sophieButtonDefaultScale;
    [SerializeField] GameObject susanneButton;
    Vector3 susanneButtonDefaultScale;
    [SerializeField] GameObject matthiasButton;
    Vector3 matthiasButtonDefaultScale;
    [SerializeField] GameObject janButton;
    Vector3 janButtonDefaultScale;
    [SerializeField] GameObject felixButton;
    Vector3 felixButtonDefaultScale;

    [SerializeField] float buttonFeedbackTimer = .3f;

    Vector3 foodSpawnPostion;
    Quaternion foodSpawnRotation;


    private void Start()
    {
        sophieButtonDefaultScale = sophieButton.transform.localScale;
        susanneButtonDefaultScale = susanneButton.transform.localScale;
        matthiasButtonDefaultScale = matthiasButton.transform.localScale;
        janButtonDefaultScale = janButton.transform.localScale;
        felixButtonDefaultScale = felixButton.transform.localScale;

    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            //gameHasEnded = true; // should tell Matthias Finish UI to end game (loss)
            PauseGame();
            SceneManager.LoadScene("Credits");
        }
    }
    void PauseGame()
    {
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
    }

    // starts Susi's level
    public void ChooseSusisLevel()
    {
        standardClick.Play();
        SceneManager.LoadScene("Basic map2");
    }

    // starts Felix's level
    public void ChooseFelixLevel()
    {
        standardClick.Play();
        SceneManager.LoadScene("FelixTrack2");
    }

    // Restart current Level
    public void Restart()
    {
        standardClick.Play();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Opens Credit Screen
    public void CreditScreen()
    {
        standardClick.Play();
        SceneManager.LoadScene("Credits_UI");
    }

    // Goes back to main menu
    public void BackToMain()
    {
        standardClick.Play();
        SceneManager.LoadScene("Start_UI");
        /*if (SceneManager.GetActiveScene().name != "Start_UI")
        {
            SceneManager.LoadScene("Start_UI");
        }
        else
        {
            creditScreen.SetActive(false);
            mainMenu.SetActive(true);
        }
Debug.Log("trying to go back to Main");
    }

    // Exits the game
    public void QuitGame()
{
    exitClick.Play(); // should play door closing & clicking sound
    doorClose.Play();
    Debug.Log("The game would now end in built game!");
    Application.Quit(); // only works when built
}

public void RestartGame()
{
    // SceneManager.LoadScene(SceneManager.GetActiveScene().name); // loads current scene.
}

public void Sophie()
{
    sophieButton.transform.localScale = sophieButton.transform.localScale * 1.1f;
    sophieButton.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
    Invoke("ColorAndScaleReset", buttonFeedbackTimer);

    standardClick.Play();

    AudioSource[] helloList = sophie.GetComponent<SayHello>().arrayOfHello;
    AudioSource randomHello = helloList[Random.Range(0, helloList.Length)];
    randomHello.Play();

    GameObject randomFood = arrayOfFood[Random.Range(0, arrayOfFood.Length)];
    foodSpawnPostion = sophie.transform.Find("FoodSpawnPoint").GetComponent<Transform>().position;
    foodSpawnRotation = sophie.transform.Find("FoodSpawnPoint").GetComponent<Transform>().rotation;
    Instantiate(randomFood, foodSpawnPostion, foodSpawnRotation);
}
public void Susanne()
{
    susanneButton.transform.localScale = susanneButton.transform.localScale * 1.1f;
    susanneButton.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
    Invoke("ColorAndScaleReset", buttonFeedbackTimer);

    standardClick.Play();

    AudioSource[] helloList = susanne.GetComponent<SayHello>().arrayOfHello;
    AudioSource randomHello = helloList[Random.Range(0, helloList.Length)];
    randomHello.Play();

    GameObject randomFood = arrayOfFood[Random.Range(0, arrayOfFood.Length)];
    foodSpawnPostion = susanne.transform.Find("FoodSpawnPoint").GetComponent<Transform>().position;
    foodSpawnRotation = susanne.transform.Find("FoodSpawnPoint").GetComponent<Transform>().rotation;
    Instantiate(randomFood, foodSpawnPostion, foodSpawnRotation);
}
public void Matthias()
{
    matthiasButton.transform.localScale = matthiasButton.transform.localScale * 1.1f;
    matthiasButton.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
    Invoke("ColorAndScaleReset", buttonFeedbackTimer);

    standardClick.Play();

    AudioSource[] helloList = matthias.GetComponent<SayHello>().arrayOfHello;
    AudioSource randomHello = helloList[Random.Range(0, helloList.Length)];
    randomHello.Play();

    GameObject randomFood = arrayOfFood[Random.Range(0, arrayOfFood.Length)];
    foodSpawnPostion = matthias.transform.Find("FoodSpawnPoint").GetComponent<Transform>().position;
    foodSpawnRotation = matthias.transform.Find("FoodSpawnPoint").GetComponent<Transform>().rotation;
    Instantiate(randomFood, foodSpawnPostion, foodSpawnRotation);
}
public void Jan()
{
    janButton.transform.localScale = janButton.transform.localScale * 1.1f;
    janButton.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
    Invoke("ColorAndScaleReset", buttonFeedbackTimer);

    standardClick.Play();

    AudioSource[] helloList = jan.GetComponent<SayHello>().arrayOfHello;
    AudioSource randomHello = helloList[Random.Range(0, helloList.Length)];
    randomHello.Play();

    GameObject randomFood = arrayOfFood[Random.Range(0, arrayOfFood.Length)];
    foodSpawnPostion = jan.transform.Find("FoodSpawnPoint").GetComponent<Transform>().position;
    foodSpawnRotation = jan.transform.Find("FoodSpawnPoint").GetComponent<Transform>().rotation;
    Instantiate(randomFood, foodSpawnPostion, foodSpawnRotation);
}
public void Felix()
{
    felixButton.transform.localScale = felixButton.transform.localScale * 1.1f;
    felixButton.GetComponentInChildren<TMP_Text>().color = new Color32(255, 255, 255, 255);
    Invoke("ColorAndScaleReset", buttonFeedbackTimer);

    standardClick.Play();

    AudioSource[] helloList = felix.GetComponent<SayHello>().arrayOfHello;
    AudioSource randomHello = helloList[Random.Range(0, helloList.Length)];
    randomHello.Play();

    GameObject randomFood = arrayOfFood[Random.Range(0, arrayOfFood.Length)];
    foodSpawnPostion = felix.transform.Find("FoodSpawnPoint").GetComponent<Transform>().position;
    foodSpawnRotation = felix.transform.Find("FoodSpawnPoint").GetComponent<Transform>().rotation;
    Instantiate(randomFood, foodSpawnPostion, foodSpawnRotation);
}

void ColorAndScaleReset()
{
    sophieButton.transform.localScale = sophieButtonDefaultScale;
    sophieButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

    susanneButton.transform.localScale = susanneButtonDefaultScale;
    susanneButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

    matthiasButton.transform.localScale = matthiasButtonDefaultScale;
    matthiasButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

    janButton.transform.localScale = janButtonDefaultScale;
    janButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);

    felixButton.transform.localScale = felixButtonDefaultScale;
    felixButton.GetComponentInChildren<TMP_Text>().color = new Color32(127, 255, 121, 255);
}
}*/