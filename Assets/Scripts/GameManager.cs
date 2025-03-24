using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{

    //static instance of the game manager which can be accessed from other scripts

    public static GameManager instance;
    private AudioSource audioSource; 
    public List<AudioClip> soundEffects; //SFX list/manager

    public GameObject player;
    private Vector3 startingPosition; //player initial spot
    
    public static bool hasPlayerWon = false; //check for win state

private void Awake()
    {

        //we initialize the game manager
        //making sure only one instance exists and manager is same between scenes

        //check if an instance doesnt exist
        if (instance == null)
        {
            //if not, set this to instance
             instance = this;

            //make sure that game manager persists between scenes
             DontDestroyOnLoad(gameObject);

             //sub to scene loaded event
             SceneManager.sceneLoaded += OnSceneLoaded;
             
        }
        else
        {
            Debug.Log("game manager instance exists, destroying duplicate");
            //if any exists then destroy them
            Destroy(gameObject);
            return;
        }
       

    }

    //method for what happens when the scene loads
    private void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode)
    {

        hasPlayerWon = false;//reset win

        player = GameObject.FindWithTag("Player");
        if (player !=null)
        {
            player.GetComponent<SpriteRenderer>().enabled = true;

            startingPosition = player.transform.position;//save initial position
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   public void GameOver()
    {
        if(player !=null)
    {
    //disable sprite on death
    player.GetComponent<SpriteRenderer>().enabled = false;

    Debug.Log("Successful disable of player");
    }
    }

}
