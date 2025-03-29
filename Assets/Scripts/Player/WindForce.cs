using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForce : MonoBehaviour
{
    [SerializeField] private int windDamage = 1; //wind damage to enemy
    [SerializeField] private float lifetime = 2f; //time before wind is destroyed

    [SerializeField] private float knockBackForce = 2f; //force of wind knockback
    
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifetime); //destory after lifetime ends
    }

    private void OnTriggerEnter2D(Collider2D windCollide)
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
                    knockBackDirection.y = 0; //dont knock in vertical

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
    }
}
