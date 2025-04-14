using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject pressEText; //tells you to press u
    [SerializeField] private GameObject levelCompleteText; //UI text for level complete
    [SerializeField] private SpriteRenderer playerSprite; //ref to player

    private bool playerInRange = false;



    // Start is called before the first frame update
    void Start()
    {
        pressEText.SetActive(false); //start in off position
        levelCompleteText.SetActive(false);//set in off position

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
        playerSprite.enabled = false;
        pressEText.SetActive(false);
        levelCompleteText.SetActive(true);




    }
}
