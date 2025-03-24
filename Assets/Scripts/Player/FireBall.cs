using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int damage = 3; // Damage dealt by the projectile
    [SerializeField] private float lifetime = 2f; // Lifetime of the projectile


    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime); //destroy object after lifetime ends
    }

    private void OnCollisionEnter2D(Collider2D ballCollision)
    {
        if(ballCollision.CompareTag("Enemy"))
        {
            Enemy enemy = ballCollision.GetComponent<Enemy>();//get enemy script
            if(enemy != null)
            {
                enemy.TakeDamage(damage); //deal damage
            }

            Destroy(gameObject);//destroy projectile
        }
    }
}
