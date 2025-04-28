using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    private bool isPaused = false;
    
    public FireBallAttack fireAttack;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("ESC pressed");

            if(isPaused)
            {
                Resume();
            }

            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if(fireAttack !=null)
        {
            fireAttack.canAttack = true;
        }
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if(fireAttack !=null)
        {
            fireAttack.canAttack = false;
        }


    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f; //reset time before switching scenes
        SceneManager.LoadScene("MainMenu"); 
        Destroy(GameManager.instance.gameObject);
    }
}
