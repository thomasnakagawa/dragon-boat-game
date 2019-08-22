using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class Leaderboard : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private Button submitButton;
    [SerializeField] private TMPro.TMP_InputField inputField;
    [SerializeField] private TMPro.TMP_Text scoreText;
    [SerializeField] private TMPro.TMP_Text listText;
    [SerializeField] private CanvasManger canMan;

    private float raceTime;
    // Start is called before the first frame update
    void Start()
    {
        canvas.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnSubmitButtonClicked()
    {
        inputField.interactable = false;
        submitButton.interactable = false;
        StartCoroutine(PostScore(raceTime));
    }

    public void OnFieldChanged()
    {
        string str = inputField.text;
        submitButton.interactable = str.Length > 0 && str.Contains("|") == false;
    }

    public void open(float raceTime)
    {
        this.raceTime = raceTime;
        canvas.enabled = true;
        scoreText.text = "Your time: " + raceTime.ToString("0.000") + "s";

        StartCoroutine(FetchLeaderboard());
    }

    public void close()
    {
        canvas.enabled = false;
        canMan.atEndOfGame();
    }

    public bool isOpen()
    {
        return canvas.enabled;
    }

    IEnumerator FetchLeaderboard()
    {
        string url = "https://www.dreamlo.com/lb/" + SecretStrings.DREAMLO_PUBLIC_KEY + "/pipe/100";
        UnityWebRequest www = UnityWebRequest.Get(url);
        listText.text = "Loading...";
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            listText.text = "Error getting the leaderboard (maybe not connected to the internet)";
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            string response = www.downloadHandler.text;
            displayFetchedLeaderboard(response);
        }
    }

    private IEnumerator PostScore(float time)
    {
        string url = "https://www.dreamlo.com/lb/" + SecretStrings.DREAMLO_PRIVATE_KEY + "/add-pipe/" + inputField.text + "/" + timeToScore(time);
        UnityWebRequest www = UnityWebRequest.Get(url);
        listText.text = "Loading...";
        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            listText.text = "Error getting the leaderboard (maybe not connected to the internet)";
            Debug.Log(www.error);
        }
        else
        {
            // Show results as text
            string response = www.downloadHandler.text;
            displayFetchedLeaderboard(response);
        }
    }

    private void displayFetchedLeaderboard(string response)
    {
        string[] rows = response.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
        string message = "";
        int rowsToShow = rows.Length;
        if (rowsToShow > 100)
        {
            rowsToShow = 100;
        }
        if (rowsToShow < 10)
        {
            rowsToShow = 10;
        }
        for (int i = 0; i < rowsToShow; i++)
        {
            message += ((i + 1).ToString() + ": ");
            if (i < rows.Length)
            {
                string row = rows[i];
                string[] segments = row.Split('|');
                string name = segments[0];
                int score = int.Parse(segments[1]);
                float time = scoreToTime(score);
                string date = segments[3];
                message += (name + " | " + time);
            }
            message += "\n";

        }
        listText.text = message;
    }

    private int magicNumber = 10000000;
    private int timeToScore(float time)
    {
        return magicNumber - (int)(time * 1000);
    }
    private float scoreToTime(int score)
    {
        return (magicNumber - score) / 1000f;
    }
}
