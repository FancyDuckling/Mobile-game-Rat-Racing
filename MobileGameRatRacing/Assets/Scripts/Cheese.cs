using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class Cheese : MonoBehaviour
{
    private FirebaseAuth auth;
    private DatabaseReference databaseRef;

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        databaseRef = FirebaseDatabase.DefaultInstance.RootReference;
    }

   
    void Update()
    {
        transform.Rotate(0 , 0 , 40 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            FindAnyObjectByType<AudioManager>().PlaySound("PickUpCheese");
            PlayerManager.numberOfCheese += 1;
            SaveScoreToDatabase(auth.CurrentUser.DisplayName, PlayerManager.numberOfCheese);
            Destroy(gameObject);
        }
    }

    private void SaveScoreToDatabase(string username, int score)
    {
        if (auth.CurrentUser != null)
        {
            // Create a new entry in the database with the user's score
            string userId = auth.CurrentUser.UserId;
            Dictionary<string, object> scoreData = new Dictionary<string, object>();
            scoreData["username"] = username;
            scoreData["score"] = score;
            databaseRef.Child("scores").Child(userId).SetValueAsync(scoreData);
        }
    }
}
