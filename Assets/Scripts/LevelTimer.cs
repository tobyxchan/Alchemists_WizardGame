using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelTimer : MonoBehaviour
{

    private float timePassed = 0f; //elapsed time
    private bool ticking = true; //time ticking away
    public TextMeshProUGUI timerText; //timer text refs

    // Update is called once per frame
    void Update()
    {
        if (ticking)
        {
            timePassed += Time.deltaTime; //time passed linked to time.deltatime
            UpdateTimerUI(); //update visual timer
        }
    }

    private void UpdateTimerUI()
    {
        //time passed in % of 1 hour of seconds divided by seconds in a minute
        int minutes = Mathf.FloorToInt(timePassed /60);
        //time passed in percent of seconds in a minute
        int seconds = Mathf.FloorToInt(timePassed % 60); 
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public void StopTimer()
    {
        ticking = false;
    }
    
    public void ResetTimer()
    {
        timePassed = 0f;
        UpdateTimerUI();
    }

    public string GetElapsedTime()
    {
  
        //time passed in % of 1 hour of seconds divided by seconds in a minute
        int minutes = Mathf.FloorToInt(timePassed / 60);

        //time passed in percent of seconds in a minute
        int seconds = Mathf.FloorToInt(timePassed % 60); 

        return string.Format("{0:00}:{1:00}",minutes,seconds); 
    }
}
