using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MovePlatformLever : MonoBehaviour
{

    private bool playerInRange = false; //track player in range
    private bool isActivated = false; //prevent multiple activations

    [SerializeField] private Sprite leverOnSprite; //sprite when on
    [SerializeField] private Sprite leverOffSprite; //sprite when off

    [SerializeField] private MovingPlatform platform; //ref to platform

    [SerializeField] private GameObject pressE; 


    private SpriteRenderer spriteRenderer;
    

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = leverOffSprite; //set default to off

        pressE.SetActive(false); //hide at start

    }

    void Update()
    {
        if(playerInRange && !isActivated && Input.GetKeyDown(KeyCode.E))
        {
            ActivateLever();
        }
    }

    private void ActivateLever()
    {
        isActivated = true;
        spriteRenderer.sprite = leverOnSprite; //change lever to on sprite
        platform.StartMoving();

    }
  public void OnTriggerEnter2D(Collider2D leverCollide)
  {
    if(leverCollide.CompareTag("Player"))

    {
        pressE.SetActive(true); //show press e
        playerInRange = true;
    }
  }

  private void OnTriggerExit2D(Collider2D leverExit)
  {
    if(leverExit.CompareTag("Player"))
    {
        playerInRange = false;
        pressE.SetActive(false); //hide text
    }
  }
}
