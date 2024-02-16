using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public GameObject scorePanel;
    public GameObject gameOverPanel;
    public static bool isShowinScore;

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
        
        isShowinScore = false;
    }

    public void QuitGame() 
    { 
        Application.Quit();
        isShowinScore = false;
    }

    public void SeeScores()
    {
        isShowinScore = true;
        scorePanel.SetActive(true);
        
       
    }
}
