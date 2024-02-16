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
    
    public GameObject gameOverPanel;
    public GameObject startingText;
    public GameObject swipeUpText;
    public GameObject swipeText;
    public static bool gameOver;
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

            if (swipeText != null)
                swipeText.SetActive(true);



        }

        if (SwipeManager.swipeLeft || SwipeManager.swipeRight)
        {
            Destroy(swipeText);
          
            if(swipeUpText != null)
                swipeUpText.SetActive(true);
           
        }

        if (SwipeManager.swipeUp)
        {
            Destroy(swipeUpText);
        }

        if (Events.isShowinScore == true)
        {
            gameOverPanel.SetActive(false);
        }

    }
}
