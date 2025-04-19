using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollect : MonoBehaviour
{
    public int heartValue = 3; // Amount of hearts gained after collect

    private PlayerController player;    // Player ref
    private PlayerHealth playerHealth;  // Health ref
    private AudioSource audioSource;    // Audio source ref

    [SerializeField] private AudioClip heartSFX;

    [SerializeField] private GameObject healthCollectParticle;


    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
    }

    public void OnTriggerEnter2D(Collider2D heartCollider)
    {

        // Get health
        PlayerHealth playerHealth = heartCollider.GetComponent<PlayerHealth>();
        PlayerController player = heartCollider.GetComponent<PlayerController>();

        if(heartCollider.CompareTag("Player"))// If player collects
        {
            //particle sim
        if(healthCollectParticle != null)
        {
            Instantiate(healthCollectParticle,transform.position, Quaternion.identity);
        }
            if(playerHealth !=null)
            {
                // Heal by collect value
                playerHealth.Heal(heartValue); 
                // Play heal SFX
                SoundFXManager.instance.PlaySoundFXClip(heartSFX, transform, 1f);
            }
            
            Destroy(gameObject);
        }
    }
}
