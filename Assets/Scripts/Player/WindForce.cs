using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [SerializeField] private int windDamage = 1; //wind damage to enemy
    [SerializeField] private float lifetime = 2f; //time before wind is destroyed

    [SerializeField] private float knockBackForce = 2f; //force of wind knockback
    
    private ParticleSystem windParticleSystem;

    // Start is called before the first frame update
    void Start()
    {
        // Find and play the particle effect if it exists
        windParticleSystem = GetComponentInChildren<ParticleSystem>(); // Get the particle system from the child object
        if (windParticleSystem != null)
        {
            windParticleSystem.Play(); // Play the wind trail particle effect
        }
        else
        {
            Debug.LogWarning("No particle system found on WindForce.");
        }

        Destroy(gameObject, lifetime); // Destroy the wind force object after the lifetime ends
    }
    

     void OnTriggerEnter2D(Collider2D windCollide)
    {
        if(windCollide.CompareTag("Enemy"))
        {
            Enemy enemy = windCollide.GetComponent<Enemy>(); //get enemy script
            Rigidbody2D enemyRb = windCollide.GetComponent<Rigidbody2D>();//get enemy rigid body
            
            if(enemy !=null)
            {
                enemy.TakeDamage(windDamage); //deal damage

                if(enemyRb !=null)
                {
                    //determine knockback direction(push away from winds position)
                    Vector2 knockBackDirection = (windCollide.transform.position - transform.position).normalized;
                    knockBackDirection.y = Mathf.Clamp(knockBackDirection.y,0.6f,1f); //slight vertical force

                    //apply force
                    enemyRb.AddForce(knockBackDirection* knockBackForce, ForceMode2D.Impulse);

                }

            }
            Destroy(gameObject); //destroy projectile after collision
            
        }

        else if (windCollide.CompareTag("Environment"))//destory if it hits terrain
        {
            Destroy(gameObject);
        }

        else if (windCollide.CompareTag("WallTorch") )//extinguish torch if hit
        {
            //find wall torch component
            WallTorchLight torchLit = windCollide.GetComponent<WallTorchLight>();

            if(torchLit !=null)
            {
                if(torchLit.isLit)
                {
                //if it finds component, extinguish torch
                torchLit.Extinguish(); //turn off torch
                Destroy(gameObject); //destory wind attack
                }
            }
            else
            {
                Debug.LogError("walltorchlight component missing");
                Destroy(gameObject); //destroy even if 
            }
        }
    
    }
}
