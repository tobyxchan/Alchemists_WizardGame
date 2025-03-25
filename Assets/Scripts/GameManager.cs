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
    private PlayerHealth playerHealth;
    public GameObject mainUI;

    public bool hasPlayerWon = false;

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
    }

    private void Update()
    {
        if(player !=null && player.transform.position.y <-11f)

        {
            GameOver();
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
