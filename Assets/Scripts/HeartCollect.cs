using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollect : MonoBehaviour
{
    public int heartValue = 3; //amount of hearts gained after collect

    private PlayerController player;//player ref
    private PlayerHealth playerHealth;//health ref

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D heartCollider)
    {
        //get health
        PlayerHealth playerHealth = heartCollider.GetComponent<PlayerHealth>();
        PlayerController player = heartCollider.GetComponent<PlayerController>();

        if(heartCollider.CompareTag("Player"))//if player collects
        {
            if(playerHealth !=null)
            {
                playerHealth.Heal(heartValue); //heal by collect value

            }
            
            Destroy(gameObject);
        }
    }
}
