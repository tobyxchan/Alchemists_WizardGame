using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlatformLever : MonoBehaviour
{

    private bool playerInRange = false; //track player in range
    private bool isActivated = false; //prevent multiple activations

    [SerializeField] private Sprite leverOnSprite; //sprite when on
    [SerializeField] private Sprite leverOffSprite; //sprite when off

    [SerializeField] private MovingPlatform platform; //ref to platform

    private SpriteRenderer spriteRenderer;
    

    
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = leverOffSprite; //set default to off

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
        platform.gameObject.SetActive(true); //activate platform

        platform.StartMoving();

    }
  public void OnTriggerEnter2D(Collider2D leverCollide)
  {
    if(leverCollide.CompareTag("Player"))

    {
        playerInRange = true;
    }
  }

  private void OnTriggerExit2D(Collider2D leverExit)
  {
    if(leverExit.CompareTag("Player"))
    {
        playerInRange = false;
    }
  }
}
