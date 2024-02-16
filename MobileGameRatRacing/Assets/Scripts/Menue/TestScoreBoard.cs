using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Auth;
using Firebase.Database;
using UnityEngine.SceneManagement;


public class TestScoreBoard : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private FirebaseAuth auth;
    private DatabaseReference databaseRef;
    private string userId;

    void Start()
    {
        FirebaseDatabase.DefaultInstance.SetPersistenceEnabled(false); //remove cache
        auth = FirebaseAuth.DefaultInstance;
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;

        // Check if the user is authenticated before trying to load scores
        if (auth.CurrentUser != null)
        {
            userId = auth.CurrentUser.UserId;
            LoadScoreFromDatabase(userId);
            Debug.Log("loaded score");
        }
        else
        {
            Debug.LogWarning("User is not authenticated.");
        }
    }

    void Update()
    {
        ReloadScore();
    }

    private void LoadScoreFromDatabase(string userId)
    {
        databaseRef.Child("scores").Child(userId).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("Failed to load score data: " + task.Exception);
                return;
            }

            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                if (snapshot.Exists)
                {
                    string username = snapshot.Child("username").Value.ToString();
                    int score = int.Parse(snapshot.Child("score").Value.ToString());
                    Debug.Log("snapshot of score and username");
                    // Display the score in the UI
                    DisplayScore(username, score);
                }
                else
                {
                    Debug.LogWarning("Score data does not exist.");
                }
            }
        });
    }

    public void ReloadScore()
    {
        LoadScoreFromDatabase(userId);
    }

    private void DisplayScore(string username, int score)
    {
        // Assuming you have a TextMeshProUGUI component attached to this GameObject
        scoreText.text = "Username: " + username + "\nScore: " + score;

        Debug.Log("Scoreboard updated");
    }

   
}