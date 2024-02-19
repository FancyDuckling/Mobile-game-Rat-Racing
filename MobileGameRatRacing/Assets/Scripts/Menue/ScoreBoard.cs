using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System;
using TMPro;


public class ScoreBoard : MonoBehaviour
{
    public TMP_Text scoreboardText;
    public TMP_Text beatenPlayer;


    private int loggedInPlayerScore = 0;

    void Start()
    {
        LoadScoresFromDatabase();
    }

    void Update()
    {
        
        LoadScoresFromDatabase();
    }

    void LoadScoresFromDatabase()
    {
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
        reference.Child("scores").OrderByChild("score").ValueChanged += HandleValueChanged;
    }

    private void HandleValueChanged(object sender, ValueChangedEventArgs args)
    {
        if (args.DatabaseError != null)
        {
            Debug.LogError(args.DatabaseError.Message);
            return;
        }

        if (args.Snapshot != null && args.Snapshot.ChildrenCount > 0)
        {
            List<KeyValuePair<string, object>> scoreList = new List<KeyValuePair<string, object>>();

            foreach (DataSnapshot childSnapshot in args.Snapshot.Children)
            {
                string username = childSnapshot.Child("username").Value.ToString();
                int score = Convert.ToInt32(childSnapshot.Child("score").Value);

                if (username == AuthManager.auth.CurrentUser.DisplayName)
                {
                    loggedInPlayerScore = score;
                }

                scoreList.Add(new KeyValuePair<string, object>(username, score));
            }

            // Sort the score list by score in descending order
            scoreList.Sort((x, y) => ((int)y.Value).CompareTo((int)x.Value));

            // Update UI with scoreboard
            UpdateScoreboardUI(scoreList);
        }
    }

    void UpdateScoreboardUI(List<KeyValuePair<string, object>> scoreList)
    {
        string scoreboard = "";

        foreach (var scoreEntry in scoreList)
        {
            scoreboard += scoreEntry.Key + ": " + scoreEntry.Value + "\n";
        }

        // Debug log the scoreboard before updating UI
        //Debug.Log("Scoreboard: " + scoreboard);
        // Update UI with sorted scoreboard
        scoreboardText.text = scoreboard;

        // Check if the logged-in player has beaten other players' scores
        foreach (var scoreEntry in scoreList)
        {
            if (scoreEntry.Key != AuthManager.auth.CurrentUser.DisplayName && (int)scoreEntry.Value < loggedInPlayerScore)
            {
                beatenPlayer.text = AuthManager.auth.CurrentUser.DisplayName + " beat " + scoreEntry.Key + "'s score!";
                Debug.Log(AuthManager.auth.CurrentUser.DisplayName + " beat " + scoreEntry.Key + "'s score!");
               
                break; 
            }
        }
    }
}
