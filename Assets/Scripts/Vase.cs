using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vase : MonoBehaviour
{

    [SerializeField] private int health = 6; //crate health
    [SerializeField] private GameObject greenGemPrefab;// ref to gem
    [SerializeField] private int gemCount = 3; //number of gems to spawn
    [SerializeField] private float scatterForce = 2f; //gem scatter force

    public void TakeDamage(int damage)
    {
        health -=damage; //reduce health
        if(health <= 0)
        {
            DestroyCrate();
        }
    }
    private void DestroyCrate()
    {
        //spawn green gems
        for(int i = 0; i < gemCount; i++)
        {

            // random offset around vase
            Vector2 gemOffset = new Vector2(Random.Range(-0.5f,0.5f), Random.Range(0.2f,0.6f));
            Vector2 spawnPosition = (Vector2)transform.position + gemOffset;
            
            //spawn the gem with offset
            GameObject gem = Instantiate(greenGemPrefab, spawnPosition, Quaternion.identity);
        
            //add scatter force to gems
            Rigidbody2D gemRB = gem.GetComponent<Rigidbody2D>();
            if(gemRB !=null)
            {
                //random direction
                Vector2 forceDirection = new Vector2(Random.Range(-1f,1f), Random.Range(0.5f,1f)).normalized;
                gemRB.AddForce(forceDirection * scatterForce, ForceMode2D.Impulse);
            }
        }

        Destroy(gameObject);

    }

    private void OnTriggerEnter2D(Collider2D fireCollision)
{

    if(fireCollision.CompareTag("Fireball"))
    {
        FireBall fireball = fireCollision.GetComponent<FireBall>();
        if(fireball !=null)
        {
            TakeDamage(fireball.GetDamage()); //apply damage
            Destroy(fireCollision.gameObject); //destroy fire ball on impact
        }
    }
}


}

