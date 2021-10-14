using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateHighscore : MonoBehaviour
{
    GameObject SceneManager;
    GameObject YearsEntry;
    //GameObject DaysEntry; // combined this with years
    GameObject ProsEntry;
    GameObject EnvEntry;
    GameObject HapEntry;

    public int score;

    float years;
    float days;
    public float detailPros;
    public float detailEnv;
    public float detailHap;

    // Felix added this:
    GameObject timeEntry;
    GameObject neighborCountDisplay;
    GameObject cowCountDisplay;
    GameObject scoreEntry;
    float neighborCount;
    int cowCount;


    float startTime;
    void Awake()
    {
        startTime = Time.timeSinceLevelLoad;
        Debug.Log(startTime);
    }

    public void Calculate()
    {
        // Zeit berechnen die man gespielt hat
        float currentTime = Time.timeSinceLevelLoad;
        float spentTime = currentTime - startTime;
        days = spentTime * 6.0833f; // this is 9/sec, right? == 1.4795 years per minute // using .12167/frame or 6.0833 here would make 1 Minute = 1 year
        years = days / 365;
        Debug.Log("years " + years); //added "years "

        SceneManager = GameObject.Find("SceneManager");
        float pros = SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate;
        float env = SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate;
        float hap = SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate;

        // Felix added this:
        neighborCount = SceneManager.GetComponent<NeedsManager>().efficientlyPlaced;
        cowCount = SceneManager.GetComponent<NeedsManager>().cowCounter; // technically redundant



        // werte zwichenspeichern für Detail Seite
        detailPros = pros;
        detailEnv = env;
        detailHap = hap;

        // Wenn einer der DegenerationRates positiv sein sollte, wird dieser auf null gesetzt um die Berechnung nicht zu störren 
        /*if (pros > 0)
        {
            pros = 0;
        }
        if (env > 0)
        {
            env = 0;
        }
        if (hap > 0)
        {
            hap = 0;
        }*/

        //Berechnung Highscore
        pros = pros * -1 * 1000000;
        env = env * -1 * 1000000;
        hap = hap * -1 * 1000000;
        float playtimeBonus = years * -1;
        score = (int)pros + (int)env + (int)hap + (int)playtimeBonus;
        Debug.Log("score " + score); // added this to label log

        // Wenn man nicht alle Felder voll hat und dadurch verloren
        if (SceneManager.GetComponent < NeedsManager >().tileCounter < 48)//(pros == 0 & env == 0 & hap == 0)//wenn alle DegenarationRates positiv sind, ist score 0
        {
            score = (int)spentTime;
        }
    }

    public void DetailWindow()
    {
        int currentYear = (int)years + 2021; //Userfeedback start in 2021 oder Jahre die man gebraucht hat anzeigen
        int currentDay = (int)days - ((currentYear - 2021) * 365);

        YearsEntry = GameObject.Find("Years Entry");
        YearsEntry.GetComponent<TextMeshProUGUI>().text = currentYear.ToString();

        timeEntry = GameObject.Find("Time Entry");
        timeEntry.GetComponent<TextMeshProUGUI>().text = currentYear + " years, " + currentDay + " days";
        neighborCountDisplay = GameObject.Find("neighborCount");
        neighborCountDisplay.GetComponent<TextMeshProUGUI>().text = neighborCount.ToString("0.00"); 
        cowCountDisplay = GameObject.Find("cowCount");
        cowCountDisplay.GetComponent<TextMeshProUGUI>().text = cowCount.ToString(); 
        scoreEntry = GameObject.Find("ScoreCount");
        scoreEntry.GetComponent<TextMeshProUGUI>().text = score.ToString();
        //DaysEntry = GameObject.Find("Days Entry");
        //DaysEntry.GetComponent<TextMeshProUGUI>().text = currentDay.ToString();

        ProsEntry = GameObject.Find("Pros Entry");
        ProsEntry.GetComponent<TextMeshProUGUI>().text = detailPros.ToString("0.00");

        EnvEntry = GameObject.Find("Env Entry");
        EnvEntry.GetComponent<TextMeshProUGUI>().text = detailEnv.ToString("0.00");

        HapEntry = GameObject.Find("Hap Entry");
        HapEntry.GetComponent<TextMeshProUGUI>().text = detailHap.ToString("0.00");

    }

}
