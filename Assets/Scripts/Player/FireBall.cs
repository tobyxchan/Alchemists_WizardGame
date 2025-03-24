using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int damage = 3;        // Damage dealt by the projectile
    [SerializeField] private float lifetime = 2f;   // Lifetime of the projectile


    void Start()
    {
        Destroy(gameObject, lifetime); //destroy object after lifetime ends
    }

    private void OnTriggerEnter2D(Collider2D ballCollision)
    {
        if(ballCollision.CompareTag("Enemy"))
        {
            Enemy enemy = ballCollision.GetComponent<Enemy>();  // Get enemy script
            if(enemy != null)
            {
                enemy.TakeDamage(damage); // Deal damage to enemy
            }

            Destroy(gameObject); // Destroy projectile
        }
    }
}
