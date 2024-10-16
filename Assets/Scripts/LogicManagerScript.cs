using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LogicManagerScript : MonoBehaviour
{

    public int playerScore;
    public Text scoreText;
    public TextMeshProUGUI FinelScore;
    public GameObject scoreParent;

    [Header("CharacterSelection")]
    public Image SelectedCharacterImage;
    public Button[] characterButtons;
    public Sprite[] characterSprites;
    public SpriteRenderer BirdRenderer;

    [Header("Canvas")]
    public GameObject gameOveScreen;
    public GameObject startScreen;
    public GameObject stage;
    public GameObject settingsCanvas;
    public GameObject menuCanvas;
    public GameObject howToPlayCanvas;

    [Header("Spawn&Move")]
    public PipeMoveScript pipeMoveScript; // Reference to pipe prefab script
    private List<GameObject> pipes = new List<GameObject>(); // List to track all pipes
    public float globalPipeSpeed = 5f; // Store the global pipe move speed
    public PipeSpawnScript pipeSpawnScript; // Reference to PipeSpawnScript
    public float globalSpawnRate = 3f; // Initial spawn rate
    
    [Header("Timer")]
    public int startTimeInSeconds = 10; // Countdown starts from 10 seconds
    public float currentTime;
    public TextMeshProUGUI countdownText; // Reference to the UI Text element
    public bool isGameStarted = false; // Controls when the game (and timer) starts


    [Header("Platform")]
    public GameObject platform; // Reference to the platform under the bird

    private void Start() {

        platform.SetActive(true); // Ensure platform is active at the start

        // Set the current time to the starting time
        currentTime = startTimeInSeconds;

        // Assign a listener to each button to handle the selection
        for (int i = 0; i < characterButtons.Length; i++) {
            int index = i;  // Local variable to avoid closure issues
            characterButtons[i].onClick.AddListener(() => OnCharacterSelected(index));
        }
    }

    private void Update() {
        // Run the countdown timer only after the game has started
        if (isGameStarted) {
            currentTime -= Time.deltaTime;

            // Ensure time never goes below zero
            if (currentTime <= 0) {
                currentTime = 0;
                howToPlayCanvas.SetActive(false);
                isGameStarted = false; // Stop timer after it hits zero
                Destroy(platform); // Destroy the platform when the game starts
            }

            // Update the text with the time remaining
            countdownText.text = "Time: " + Mathf.Ceil(currentTime).ToString();
        }
    }

    [ContextMenu("StartGame")]
    public void StartGame()
    {
        Debug.Log("Start");
        startScreen.SetActive(false);
        stage.SetActive(true);
        gameOveScreen.SetActive(false);
        isGameStarted = true; // Start the game and timer
    }



    [ContextMenu("increaseScore")]
    public void AddScore(int scoreToAdd)
    {
        playerScore = playerScore + scoreToAdd;
        scoreText.text = playerScore.ToString();

        if(playerScore == 0)
            return;
        
        if (playerScore % 5 == 0) {
            IncreasePipeSpeedForAll();
            DecreaseSpawnRate();
        }
    }

    public void GameOver()
    {
        gameOveScreen.SetActive(true);
        scoreText.text = "" ;
        FinelScore.text = "Score " + playerScore.ToString();
        scoreParent.SetActive(false);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        //StartGame();
    }



    //public void MainMenu() {
    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //}

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


    // Method to increase pipe speed for all existing pipes
    private void IncreasePipeSpeedForAll() {
        globalPipeSpeed += 2f; // Increase the global pipe speed

        // Update the speed for all currently active pipes
        foreach (GameObject pipe in pipes) {
            PipeMoveScript pipeMove = pipe.GetComponent<PipeMoveScript>();
            if (pipeMove != null) {
                pipeMove.SetSpeed(globalPipeSpeed);  // Update speed of existing pipes
            }
        }
    }


    private void DecreaseSpawnRate() {
        globalSpawnRate = Mathf.Max(1f, globalSpawnRate - 0.2f); // Decrease spawn rate, but keep it at a minimum of 1 second
        pipeSpawnScript.SetSpawnRate(globalSpawnRate); // Update spawn rate in the PipeSpawnScript
    }




    // Method to add new pipes to the list
    public void RegisterPipe(GameObject pipe) {
        pipes.Add(pipe);

        // Make sure new pipes inherit the current global speed
        PipeMoveScript pipeMove = pipe.GetComponent<PipeMoveScript>();
        if (pipeMove != null) {
            pipeMove.SetSpeed(globalPipeSpeed);  // Set speed for new pipes
        }
    }

    // Method to remove destroyed pipes from the list
    public void UnregisterPipe(GameObject pipe) {
        pipes.Remove(pipe);
    }

}
