using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GooProjectile : MonoBehaviour
{
    public float gooSpeed = 5f;//speed of projectile
    public int gooDamage = 1; //damage per hit to player


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * gooSpeed * Time.deltaTime); //move it forward

    }

    private void OnTriggerEnter2D(Collider2D gooCollide)
    {
        if(gooCollide.CompareTag("Player")) //check if it hit player
        {
            PlayerHealth playerHealth = gooCollide.GetComponent<PlayerHealth>();
            if(playerHealth !=null)
        {
            playerHealth.TakeDamage(gooDamage); //deal damage

        }

        Destroy(gameObject); //destroy after collide with player
        }
        else if (gooCollide.CompareTag("Environment"))//destory when hitting walls/floor
        {
            Destroy(gameObject);
        }
    }
    
}
