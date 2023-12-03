using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogicManagerScript : MonoBehaviour
{

    public int playerScore;
    public Text scoreText;
    public Text FinelScore;

    public GameObject gameOveScreen;
    public GameObject startScreen;
    public GameObject stage;


    [ContextMenu("StartGame")]
    public void StartGame()
    {
        Debug.Log("Star");
        startScreen.SetActive(false);
        stage.SetActive(true);
    }

    [ContextMenu("increaseScore")]
    public void AddScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();
    }

    public void GameOver()
    {
        gameOveScreen.SetActive(true);
        scoreText.text = "" ;
        FinelScore.text = "Score " + playerScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }


}
