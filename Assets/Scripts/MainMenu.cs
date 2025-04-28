using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void StartGame()
    {
        Time.timeScale = 1f;
        // Loads the next scene in the scene manager
        SceneManager.LoadScene("SampleScene");

        GameManager.instance.GameOver();
    }

    public void QuitGame()
    {
        // Closes the application
        Application.Quit();
    }
}
