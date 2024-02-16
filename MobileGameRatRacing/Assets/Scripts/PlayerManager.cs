using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Firebase;
using Firebase.Auth;
using Firebase.Database;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject startingText;
    public static bool isGameStarted;
    public static int numberOfCheese;
    public TextMeshProUGUI cheeseText;
    
    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
        numberOfCheese = 0;

    }


    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        cheeseText.text = numberOfCheese.ToString();

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
            
        }

        if (Events.isShowinScore == true)
        {
            gameOverPanel.SetActive(false);
        }

    }
}
