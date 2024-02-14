using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using System;
using TMPro;


public class ScoreBoard : MonoBehaviour
{
    public TMP_Text scoreboardText; // Reference to the text field to display scores

    void Start()
    {
        LoadScoresFromDatabase();
    }

    void LoadScoresFromDatabase()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("scores").OrderByChild("score").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to load scores from database: " + task.Exception);
                return;
            }

            DataSnapshot snapshot = task.Result;
            List<KeyValuePair<string, object>> scoreList = new List<KeyValuePair<string, object>>();

            foreach (DataSnapshot childSnapshot in snapshot.Children)
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int score = Convert.ToInt32(childSnapshot.Child("score").Value);

                scoreList.Add(new KeyValuePair<string, object>(username, score));
            }

            // Sort the score list by score in descending order
            scoreList.Sort((x, y) => ((int)y.Value).CompareTo((int)x.Value));


            // Update UI with scoreboard
            UpdateScoreboardUI(scoreList);
        });
    }

    void UpdateScoreboardUI(List<KeyValuePair<string, object>> scoreList)
    {
        string scoreboard = "";

        foreach (var scoreEntry in scoreList)
        {
            scoreboard += scoreEntry.Key + ": " + scoreEntry.Value + "\n";
        }

        // Debug log the scoreboard before updating UI
        Debug.Log("Scoreboard: " + scoreboard);
        // Update UI with sorted scoreboard
        scoreboardText.text = scoreboard;
        
    }
}
