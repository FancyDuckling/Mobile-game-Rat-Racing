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


    private void Start()
    {
        InitializeGame();
    }

    private void Update()
    {
        HandleGameOver();
        UpdateCheeseText();
        HandleGameStart();
        HandleSwipeActions();
        HideGameOverPanel();
    }

    private void InitializeGame()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
        numberOfCheese = 0;
    }

    private void HandleGameOver()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }
    }

    private void UpdateCheeseText()
    {
        cheeseText.text = numberOfCheese.ToString();
    }

    private void HandleGameStart()
    {
        if (SwipeManager.tap && !isGameStarted)
        {
            isGameStarted = true;
            Destroy(startingText);
            if (swipeText != null)
                swipeText.SetActive(true);
        }
    }

    private void HandleSwipeActions()
    {
        if (SwipeManager.swipeLeft || SwipeManager.swipeRight)
        {
            Destroy(swipeText);
            if (swipeUpText != null)
                swipeUpText.SetActive(true);
        }

        if (SwipeManager.swipeUp)     
            Destroy(swipeUpText);
        
    }

    private void HideGameOverPanel()
    {
        if (Events.isShowinScore)
            gameOverPanel.SetActive(false);
        
    }

}
