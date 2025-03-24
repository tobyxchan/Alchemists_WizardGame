using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGemCollect : MonoBehaviour
{
    
    private void OnTriggerEnter2D(Collider2D gemCollide)
    {
        if(gemCollide.CompareTag("Player"))
        {
            GameManager.instance.AddScore(1);
            Destroy(gameObject);
        }
    }

}
