using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Events : MonoBehaviour
{
    public GameObject scorePanel;
    public GameObject gameOverPanel;
    public static bool isShowinScore;


    private IEnumerator DelayedReset(bool value, float delay)
    {
        yield return new WaitForSeconds(delay);
        isShowinScore = value;
    }

    public void ReplayGame()
    {
        SceneManager.LoadScene("Level");
        //StartCoroutine(DelayedReset(false, 0.05f));
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
