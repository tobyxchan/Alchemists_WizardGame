using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeTrap : MonoBehaviour
{
    [SerializeField] private int damageAmount = 20; //damage dealt

    private void OnTriggerEnter2D(Collider2D spikeCollider)
    {
        if(spikeCollider.CompareTag("Player"))
        {
            Debug.Log("Player has died");
            PlayerHealth playerHealth = spikeCollider.GetComponent<PlayerHealth>();

            if(playerHealth !=null)
            {
                playerHealth.TakeDamage(damageAmount); //apply damage

                //if player reaches 0, trigger game over
                if(playerHealth.currentHearts <=0)
                {
                    GameManager.instance.GameOver();
                }
            }
        }

        if(spikeCollider.CompareTag("Enemy"))
        {
            Debug.Log("Enemy has died");
            Enemy enemy = spikeCollider.GetComponent<Enemy>();

            if(enemy !=null)
            {
                enemy.TakeDamage(damageAmount); //apply damage

                if(enemy.currentEnemyHealth <=0)
                {
                    Destroy(spikeCollider.gameObject);
                }
            }
        }
    }

}
