using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<HighscoreEntry> highscoreEntriesList;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("EntryContainer");
        //Debug.Log(entryContainer);
        entryTemplate = entryContainer.Find("EntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        /*
        highscoreEntriesList = new List<HighscoreEntry>() //Liste wird erstellt
        {
            new HighscoreEntry{ score = 2834, name = "AAA"},
            new HighscoreEntry{ score = 3244, name = "AAA"},
            new HighscoreEntry{ score = 3834, name = "ABA"},
            new HighscoreEntry{ score = 4834, name = "AAA"},
            new HighscoreEntry{ score = 2834, name = "AAA"},
            new HighscoreEntry{ score = 2834, name = "AAA"},
            new HighscoreEntry{ score = 2834, name = "AAA"},
        };*/

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        //sortiert die Liste nach dem Score
        for (int i = 0; i < highscoreEntriesList.Count; i++)
        {
            for (int j = i + 1; j < highscoreEntriesList.Count; j++)
            {
                if (highscoreEntriesList[j].score > highscoreEntriesList[i].score)  //schaut ob das nachfolgende Listen Element einen größeren Score hat und taucht, wenn nötig deren Position
                {
                    HighscoreEntry tmp = highscoreEntriesList[i];
                    highscoreEntriesList[i] = highscoreEntriesList[j];
                    highscoreEntriesList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscoreEntriesList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        /*
        Highscores highscores = new Highscores { highscoreEntriesList = highscoreEntriesList };
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
        Debug.Log(PlayerPrefs.GetString("highscoreTable"));
        */
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 50f;//Abstand zwichen Zeilen

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;
        switch (rank)
        {
            default:
                rankString = rank + "TH"; break;

            case 1: rankString = "1ST"; break;
            case 2: rankString = "2ND"; break;
            case 3: rankString = "3RD"; break;
        }

        entryTransform.Find("posText").GetComponent<Text>().text = rankString;  //position wird belegt

        int score = highscoreEntry.score;
        entryTransform.Find("scoreText").GetComponent<Text>().text = score.ToString();  //Score wird belegt

        string name = highscoreEntry.name;
        entryTransform.Find("nameText").GetComponent<Text>().text = name;   //Name wird belegt

        transformList.Add(entryTransform);
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntriesList;
    }

    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }
}
