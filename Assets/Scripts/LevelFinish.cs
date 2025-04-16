using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject pressEText; //tells you to press u
    [SerializeField] private SpriteRenderer playerSprite; //ref to player
    [SerializeField] private Image winLogo; //win logo ref

    private Animator winLogoAnimator;

    private bool playerInRange = false;

    private GameManager gameManager;



    // Start is called before the first frame update
    void Start()
    {
        pressEText.SetActive(false); //start in off position
        winLogo.gameObject.SetActive(false);//set in off position
        winLogoAnimator = winLogo.GetComponent<Animator>();

        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();

    }

//if player enters trigger zone, "press E" text shows and sets in range to true
    private void OnTriggerEnter2D(Collider2D doorCollide)
    {
        if(doorCollide.CompareTag("Player"))
        {
            pressEText.SetActive(true);
            playerInRange = true;
        }
    }

//if you leave trigger zone it removes text and wont allow interaction
    private void OnTriggerExit2D(Collider2D doorNoCollide)
    {
        if(doorNoCollide.CompareTag("Player"))
        {
            pressEText.SetActive(false);
            playerInRange =false;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            CompleteLevel();

        }
    }

//temporary level complete setup for functioning prototype
    private void CompleteLevel()
    {
        gameManager.hasPlayerWon = true;
        
        playerSprite.enabled = false;
        pressEText.SetActive(false);

        winLogo.gameObject.SetActive(true);
        if(winLogoAnimator !=null)
        {
            winLogoAnimator.SetTrigger("PlayWin"); //trigger anim
        }

        //disable movement
        GameObject player = GameObject.FindWithTag("Player");

        if(player !=null)
        {
            player.GetComponent<PlayerController>().canMove = false;
        }



    }
}
