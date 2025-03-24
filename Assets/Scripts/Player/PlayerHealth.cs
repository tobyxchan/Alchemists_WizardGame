using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public int maxHearts = 20; //max health count
    public int currentHearts; //current health count

    [SerializeField] Sprite heartFull;
    [SerializeField] Sprite heartEmpty;
    [SerializeField] List<Image> heartList = new List<Image>();

    // Start is called before the first frame update
    void Start()
    {
        currentHearts = maxHearts; //set initial health to full
        UpdateHealth(); //update health UI
    }

    //damage taken method
    public void TakeDamage(int damageToTake)
    {
        if(currentHearts > 0)
        {
            currentHearts -= damageToTake;
            Debug.Log(currentHearts);
        }
        if(currentHearts <=0)
        {
            GameManager.instance.GameOver();
        }
        UpdateHealth();

    }

    //healing
    public void Heal(int amount)
    {
        currentHearts += amount;
        if(currentHearts > maxHearts) currentHearts = maxHearts;
        UpdateHealth();
    }

       public void UpdateHealth()
    {
        int heartListLength = heartList.Count;

        Debug.Log("Heart List Length: " + heartListLength);  // Check if the list is filled
         Debug.Log("Current Hearts: " + currentHearts);       // Verify health value

        Debug.Log("Current hearts:" + currentHearts);

        //check list against player health
        
        for(int i = 0; i < heartListLength; i++)
        {
            if(heartList[i] ==null)

            { Debug.LogError("Heart at index " + i + " is not assigned!");
            return;  // Stop further execution if an item is null
            }

            if (i < currentHearts)
            {
                heartList[i].sprite = heartFull;

            }
            else
            {
                heartList[i].sprite = heartEmpty;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
