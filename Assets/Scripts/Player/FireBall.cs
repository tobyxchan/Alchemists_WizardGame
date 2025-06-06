using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int damage = 3;        // Damage dealt by the projectile
    [SerializeField] private float lifetime = 2f;   // Lifetime of the projectile

    private bool hasHit = false; //prevent multiple hits

    void Start()
    {
        Destroy(gameObject, lifetime); //destroy object after lifetime ends
    }

    public int GetDamage()
    {
        return damage;
    }

    private void OnTriggerEnter2D(Collider2D ballCollision)
    {
        if(hasHit) return; //stop multiple hits

        if(ballCollision.CompareTag("Enemy"))
        {
            Enemy enemy = ballCollision.GetComponent<Enemy>();  // Get enemy script
            if(enemy != null)
            {
                enemy.TakeDamage(damage); // Deal damage to enemy
                hasHit = true; 
            }

            Destroy(gameObject); // Destroy projectile
        }
        else if (ballCollision.CompareTag("Environment"))// Destroy when hitting walls/floor
        {
            Destroy(gameObject);
        }
    }
}
