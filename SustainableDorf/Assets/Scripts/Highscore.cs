using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Highscore : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;

    private void Awake()
    {
        entryContainer = transform.Find("EntryContainer");
        //Debug.Log(entryContainer);
        entryTemplate = entryContainer.Find("EntryTemplate");

        entryTemplate.gameObject.SetActive(false);

        //AddHighscoreEntry(1000000, "CMK");

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        //sortiert die Liste nach dem Score
        for (int i = 0; i < highscores.highscoreEntriesList.Count; i++)
        {
            for (int j = i + 1; j < highscores.highscoreEntriesList.Count; j++)
            {
                if (highscores.highscoreEntriesList[j].score > highscores.highscoreEntriesList[i].score)  //schaut ob das nachfolgende Listen Element einen größeren Score hat und taucht, wenn nötig deren Position
                {
                    HighscoreEntry tmp = highscores.highscoreEntriesList[i];
                    highscores.highscoreEntriesList[i] = highscores.highscoreEntriesList[j];
                    highscores.highscoreEntriesList[j] = tmp;
                }
            }
        }

        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntriesList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }

        
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

        //setze jeden Zweite Zeile mit Background
        entryTransform.Find("background").gameObject.SetActive(rank % 2 == 1);

        if (rank == 1)
        {
            //Highlight ersten Platz
            entryTransform.Find("posText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("scoreText").GetComponent<Text>().color = Color.green;
            entryTransform.Find("nameText").GetComponent<Text>().color = Color.green;
        }

        transformList.Add(entryTransform);
    }

    private void AddHighscoreEntry(int score, string name)
    {
        HighscoreEntry highscoreEntry = new HighscoreEntry { score = score, name = name };

        string jsonString = PlayerPrefs.GetString("highscoreTable");
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        highscores.highscoreEntriesList.Add(highscoreEntry);

        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
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
