using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;
    public GameObject startingText;
    public static bool isGameStarted;
    void Start()
    {
        Time.timeScale = 1;
        gameOver = false;
        isGameStarted = false;
    }


    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        if (SwipeManager.tap)
        {
            isGameStarted = true;
            Destroy(startingText);
        }
    }
}
