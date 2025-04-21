using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelFinish : MonoBehaviour
{
    [SerializeField] private GameObject pressEText; //tells you to press u
    [SerializeField] private SpriteRenderer playerSprite; //ref to player
    [SerializeField] private Image winLogo; //win logo ref
    
    [SerializeField] private Button restartLevel;

    [SerializeField] private Button nextLevel;

    [SerializeField] private TextMeshProUGUI finalTimeText; //text ref for final time
    private LevelTimer levelTimer; //level timer object link

    [SerializeField] private TextMeshProUGUI finalScoreText; //ref to final score


    private Animator winLogoAnimator;

    private bool playerInRange = false;

    private GameManager gameManager;
    
    [SerializeField] private GameObject mainUI;


    void Start()
    {
        pressEText.SetActive(false); //start in off position
        winLogo.gameObject.SetActive(false);//set in off position
        winLogoAnimator = winLogo.GetComponent<Animator>();

        gameManager =GameManager.instance;

        restartLevel.gameObject.SetActive(false);
        nextLevel.gameObject.SetActive(false);

        levelTimer = FindObjectOfType<LevelTimer>();
        finalTimeText.gameObject.SetActive(false); //start in off position until win level

        finalScoreText.gameObject.SetActive(false); //start in off until win
    
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
        pressEText.SetActive(false);

        //disable player and UI
        GameObject.FindWithTag("Player").SetActive(false);

        if(mainUI !=null)
        {
        mainUI.SetActive(false);
        }

        winLogo.gameObject.SetActive(true);
        if(winLogoAnimator !=null)
        {
            winLogoAnimator.SetTrigger("PlayWin"); //trigger anim
        }

        //activate level complete buttons
        restartLevel.gameObject.SetActive(true);
        nextLevel.gameObject.SetActive(true);

        //level score and time
        levelTimer.StopTimer(); //stop clock

        string finalTime = levelTimer.GetElapsedTime(); //get final time
        finalTimeText.text = "Time: " + finalTime;
        finalTimeText.gameObject.SetActive(true); // show time text

        if(finalScoreText !=null)
        {
            //activate and display final score pulled from manager
            finalScoreText.gameObject.SetActive(true); 
            finalScoreText.text = "Gems: " + gameManager.score.ToString();
        }
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
