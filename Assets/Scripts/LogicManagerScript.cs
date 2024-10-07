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
    //public Sprite Bird;
    public SpriteRenderer BirdRenderer;

    [Header("CharacterSelection")]
    public Image SelectedCharacterImage;
    public Button[] characterButtons;
    public Sprite[] characterSprites;

    [Header("Canvas")]
    public GameObject gameOveScreen;
    public GameObject startScreen;
    public GameObject stage;
    public GameObject settingsCanvas;
    public GameObject menuCanvas;


    private void Start() {
        // Assign a listener to each button to handle the selection
        for (int i = 0; i < characterButtons.Length; i++) {
            int index = i;  // Local variable to avoid closure issues
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }
    }

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

    public void GoToSettings() {
        settingsCanvas.SetActive(true);
        menuCanvas.SetActive(false);
    }

    public void GoToMenu() {
        settingsCanvas.SetActive(false);
        menuCanvas.SetActive(true);

    }


    public void OnCharacterSelected(int characterIndex) {
        SelectedCharacterImage.sprite = characterSprites[characterIndex];
        //Bird = characterSprites[characterIndex];
        BirdRenderer.sprite = characterSprites[characterIndex];


    }
}
