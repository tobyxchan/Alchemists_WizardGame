using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaPotion : MonoBehaviour
{

public void OnTriggerEnter2D (Collider2D manaCollide)
{
    if(manaCollide.CompareTag("Player"))
    {
        ManaBar manaBar = manaCollide.GetComponent<ManaBar>();
        if(manaBar !=null)
        {
            manaBar.RefillMana(); //call method to fill
        }

        Destroy(gameObject); //remove potion after collect
    }
}

}
