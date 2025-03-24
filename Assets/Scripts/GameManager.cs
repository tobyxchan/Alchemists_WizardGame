using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    // Static instance of the game manager which can be accessed from other scripts

    public static GameManager instance;
    private AudioSource audioSource;
    public GameObject player;
    private Vector3 startingPosition; // Player initial spot
    
    public static bool hasPlayerWon = false; // Check for win state


    private void Awake()
    {
        // We initialize the game manager, making sure only one instance exists and manager is same between scenes
        // Check if an instance doesnt exist
        if (instance == null)
        {
            // If not, set this to instance
             instance = this;

            // Make sure that game manager persists between scenes
             DontDestroyOnLoad(gameObject);

             // Sub to scene loaded event
             SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Debug.Log("game manager instance exists, destroying duplicate");
            // If any exists then destroy them
            Destroy(gameObject);
            return;
        }
    }


    // Method for what happens when the scene loads
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {

        hasPlayerWon = false; // Reset win

        player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            player.GetComponent<SpriteRenderer>().enabled = true;

            startingPosition = player.transform.position;// Save initial position
        }
    }


   public void GameOver()
    {
            if(player !=null)
        {
            // Disable sprite on death
            player.GetComponent<SpriteRenderer>().enabled = false;
            Debug.Log("Successful disable of player");
        }
    }
}
