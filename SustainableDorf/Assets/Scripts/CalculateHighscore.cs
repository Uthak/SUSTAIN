using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CalculateHighscore : MonoBehaviour
{
    GameObject SceneManager;
    GameObject YearsEntry;
    GameObject DaysEntry;
    GameObject ProsEntry;
    GameObject EnvEntry;
    GameObject HapEntry;
    public int score;

    float years;
    float days;
    public float detailPros;
    public float detailEnv;
    public float detailHap;



    float startTime;
    // Start is called before the first frame update
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
        days = spentTime * 9;
        years = days / 365;
        Debug.Log(years);

        SceneManager = GameObject.Find("SceneManager");
        float pros = SceneManager.GetComponent<NeedsManager>().prosperityDegenerationRate;
        float env = SceneManager.GetComponent<NeedsManager>().environmentDegenerationRate;
        float hap = SceneManager.GetComponent<NeedsManager>().happinessDegenerationRate;

        // werte zwichenspeichern für Detail Seite
        detailPros = pros;
        detailEnv = env;
        detailHap = hap;

        // Wenn einer der DegenerationRates positiv sein sollte, wird dieser auf null gesetzt um die Berechnung nicht zu störren 
        if (pros > 0)
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
        }

        //Berechnung Highscore
        pros = pros * -1 * 1000000;
        env = env * -1 * 1000000;
        hap = hap * -1 * 1000000;
        float playtimeBonus = years * -1;
        score = (int)pros + (int)env + (int)hap + (int)playtimeBonus;
        Debug.Log(score);


    }

    public void DetailWindow()
    {
        int currentYear = (int)years + 2000;
        int currentDay = (int)days - ((currentYear - 2000) * 365);

        YearsEntry = GameObject.Find("Years Entry");
        YearsEntry.GetComponent<TextMeshProUGUI>().text = currentYear.ToString();

        DaysEntry = GameObject.Find("Days Entry");
        DaysEntry.GetComponent<TextMeshProUGUI>().text = currentDay.ToString();

        ProsEntry = GameObject.Find("Pros Entry");
        ProsEntry.GetComponent<TextMeshProUGUI>().text = detailPros.ToString();

        EnvEntry = GameObject.Find("Env Entry");
        EnvEntry.GetComponent<TextMeshProUGUI>().text = detailEnv.ToString();

        HapEntry = GameObject.Find("Hap Entry");
        HapEntry.GetComponent<TextMeshProUGUI>().text = detailHap.ToString();

    }

}
