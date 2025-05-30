using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{

    public float maxMana = 100;
    private float currentMana;

    public float manaRegenerate = 10f; //restore per second
    public Slider manaBar; //image UI ref

    // Start is called before the first frame update
    void Start()
    {

        manaBar = GameManager.instance?.manaSlider;
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
            //wait one frame
            yield return null;

            //if less than max - repeat and update UI
            if(currentMana < maxMana)
            {
                currentMana += manaRegenerate * Time.deltaTime;
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

    public void RefillMana()
    {
        currentMana = maxMana; 
        UpdateManaBar();
    }    
}
