using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodCrate : MonoBehaviour
{

    [SerializeField] private int health = 6; //crate health
    [SerializeField] private GameObject greenGemPrefab;// ref to gem
    [SerializeField] private int gemCount = 3; //number of gems to spawn

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
            Instantiate(greenGemPrefab, transform.position, Quaternion.identity);

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

