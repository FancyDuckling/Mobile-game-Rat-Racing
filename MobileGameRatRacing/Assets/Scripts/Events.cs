using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public GameObject scorePanel;
    public GameObject gameOverPanel;
    public TestScoreBoard scoreBoard;

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void QuitGame() 
    { 
        Application.Quit();
    }

    public void SeeScores()
    {
        //scoreBoard.ReloadScore(); // Reload the score data
        gameOverPanel.SetActive(false);
        scorePanel.SetActive(true);
       
    }
}
