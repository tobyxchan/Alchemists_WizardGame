using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    private AudioSource audioSource;
    public GameObject player;
    private Vector3 startingPosition;

    //score system
    public int score = 0;
    public TextMeshProUGUI scoreText; //ui ref to display score
    public TextMeshProUGUI deathMessageText; //ref to death text
    public TextMeshProUGUI startInfoText; //level start info text

    private PlayerHealth playerHealth;
    public GameObject mainUI;

    public bool hasPlayerWon = false;
    private bool hasRecievedInput = false;

    private void Awake()
    {
        // Initialize the game manager and make it persistent between scenes
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to scene loaded event
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        hasPlayerWon = false;

        // Ensure MainUI is persistent across scenes
        if (mainUI == null)
        {
            mainUI = GameObject.Find("MainUI");
            if (mainUI != null)
            {
                DontDestroyOnLoad(mainUI);  // Keep the MainUI between scenes
            }
        }

        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<SpriteRenderer>().enabled = true;
            playerHealth = player.GetComponent<PlayerHealth>();

            //store start pos
            startingPosition = player.transform.position;

            // Hide death text
            if (deathMessageText != null)
            {
                deathMessageText.gameObject.SetActive(false);
            }

            score = 0;
            UpdateScoreUI();
        }

        StartInfo();
    }

    private void Update()
    {
        

        if(!hasRecievedInput && (Input.anyKeyDown || Input.GetMouseButtonDown(0)))
        {
            hasRecievedInput = true;
            HideInfoText();
        }
    }
    
    // Handle score system
    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        if (scoreText != null)
        {
            scoreText.text = score.ToString(); // Display score
        }
    }


    //info text at start of level
    private void StartInfo()
    {
        if(startInfoText !=null)
        {
            
            startInfoText.gameObject.SetActive(true);//show info text
            hasRecievedInput = false; //reset input tracking
        }

    }

    private void HideInfoText()
    {
       
        if (startInfoText !=null)
        {
            startInfoText.gameObject.SetActive(false); //hide info
        }

    }

    // Handle Game Over
    public void GameOver()
    {
        if (player != null)
        {
            // Disable sprite on death
            player.GetComponent<SpriteRenderer>().enabled = false;

            // Show death message
            if (deathMessageText != null)
            {
                deathMessageText.gameObject.SetActive(true);
                deathMessageText.text = "You Died"; // Death text
            }

            // Restart level after a short delay
            StartCoroutine(RestartSceneAfterDelay(2f));
        }
    }

//delay respawn to allow a moment after death to display text and set health to 0
    public IEnumerator RestartSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        // Reset health
        playerHealth = player.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.ResetHealth(); // Reset health and update UI
        }
    }
}
