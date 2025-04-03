using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{

    public int maxMana = 100;
    private int currentMana;

    public float manaRegenerate = 10f; //restore per second
    public Slider manaBar; //image UI ref

    // Start is called before the first frame update
    void Start()
    {

        //find slider dynamically
        if(manaBar ==null)
        {
            manaBar = GameObject.Find("ManaBar").GetComponent<Slider>();
        }
        
        currentMana = maxMana;
        UpdateManaBar();
        StartCoroutine(RegenerateMana());
    }

    public bool UseMana(int amount)
    {
        //enough mana to use ability
        if(currentMana >= amount)
        {
            currentMana -= amount;
            UpdateManaBar();
            return true; 
        }

        return false; //not enough mana to use
    }

//method to work out amount of seconds to take for adding the manaRegenerate var.
    private IEnumerator RegenerateMana()
    {
        while (true)
        {
            //wait 1 second per tick
            yield return new WaitForSeconds(1f);

            //if less than max - repeat and update UI
            if(currentMana < maxMana)
            {
                currentMana +=(int)manaRegenerate;
                if(currentMana > maxMana) currentMana = maxMana;
                UpdateManaBar();
            }
        }
    }
    private void UpdateManaBar()
    {
        //fill UI element based on amount of mana player has 
      if(manaBar !=null)
      {
        manaBar.value = (float)currentMana / maxMana;
      }
    }
    
}
