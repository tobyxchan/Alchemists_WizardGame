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
    private GameObject mainUI; // ref to  main UI
    private List<Image> heartImages = new List<Image>(); //list of heart images
    private AudioSource audioSource;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip damageSFX;
    [SerializeField] private AudioClip deathSFX;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        // Reset health on start
        if (mainUI == null)
        {
            mainUI = GameObject.Find("MainUI"); // Find MainUI if it's not assigned
        }

        FindMainUI(); //find UI on start
        ResetHealth(); //reset health on start
    }

    void FindMainUI()
    {
        mainUI = GameObject.Find("MainUI"); //dynamically find UI

        if(mainUI !=null)
        {
            PopulateHeartList(); //find heart images in UI

        }
        else
        {
            Debug.Log("main ui not found");
        }
    }
    void PopulateHeartList()
    {
        heartImages.Clear(); //clear any existing refs

        if(mainUI !=null)
        {
            Image[] heartUIElements = mainUI.GetComponentsInChildren<Image>();//find all images under main UI

            foreach (Image heart in heartUIElements)
            {
                if(heart.gameObject.name.Contains("Heart"))//make sure its a heart image
                {
                    heartImages.Add(heart);
                }
            }
        }
        else
        {
            Debug.LogError("Main UI is not assigned or found");

        }
        if(heartImages.Count ==0)
        {
            Debug.Log("Player took damage. Current Hearts: " + currentHearts);

        }
    }
    //damage taken method
    public void TakeDamage(int damageToTake)
    {
        if(currentHearts > 0)
        {
            currentHearts -= damageToTake;
            Debug.Log($"Current health: {currentHearts}");
            SoundFXManager.instance.PlaySoundFXClip(damageSFX, transform, 1f);
        }
        if(currentHearts <=0)
        {
            SoundFXManager.instance.PlaySoundFXClip(deathSFX, transform, 1f);
            GameManager.instance.GameOver();
        }
        UpdateHealth();

    }

    //healing
    public void Heal(int amount)
    {
        if(mainUI ==null) //if UI missing find again
        {
            FindMainUI();
        }
        currentHearts += amount;
        if(currentHearts > maxHearts) currentHearts = maxHearts;
        UpdateHealth();
    }

       public void UpdateHealth()
    {
        int heartListLength = heartImages.Count;

        Debug.Log("Heart List Length: " + heartListLength);  // Check if the list is filled
         Debug.Log("Current Hearts: " + currentHearts);       // Verify health value

        Debug.Log("Current hearts:" + currentHearts);

        //check list against player health
        
        for(int i = 0; i < heartListLength; i++)
        {
            if(heartImages[i] ==null)

            { Debug.LogError("Heart at index " + i + " is not assigned!");
            return;  // Stop further execution if an item is null
            }

            if (i < currentHearts)
            {
                heartImages[i].sprite = heartFull;

            }
            else
            {
                heartImages[i].sprite = heartEmpty;
            }
        }
    }
    
    public void ResetHealth()
    {
        currentHearts = maxHearts; //restore health
        UpdateHealth();
    }

}
