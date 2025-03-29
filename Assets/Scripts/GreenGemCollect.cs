using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenGemCollect : MonoBehaviour
{
    [Header("Sound Effects")]
    private AudioSource audioSource;
    [SerializeField] private AudioClip gemSFX;
    
    
    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

//when triggered by player it will collect, add score and play sound
    private void OnTriggerEnter2D(Collider2D gemCollide)
    {
        if(gemCollide.CompareTag("Player"))
        {
            GameManager.instance.AddScore(1);
            SoundFXManager.instance.PlaySoundFXClip(gemSFX, transform, 1f);
            Destroy(gameObject);
        }
    }

}
