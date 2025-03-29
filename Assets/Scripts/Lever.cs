using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Lever : MonoBehaviour
{
    [SerializeField] private Sprite leverOnSprite;
    [SerializeField] private Sprite leverOffSprite;
    [SerializeField] private Transform trapdoor;
    [SerializeField] private float moveSpeed = 2f;

    [SerializeField] private float moveDistance = 2f;

    [SerializeField] private GameObject pressEText; //text ref


    private SpriteRenderer spriteRenderer;
    private bool isOn = false;
    private bool playerNearby = false;
    private Vector3 originalTrapdoorPosition; //stores start pos


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = leverOffSprite; //start with off sprite
        originalTrapdoorPosition = trapdoor.position; //store stat pos

        pressEText.SetActive(false); //hide at start
    }

    // Update is called once per frame
    void Update()
    {
        if(playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ToggleLever();
        }
    }

    private void ToggleLever()
    {
        isOn = !isOn; //switch state

        if(isOn)
        {
            spriteRenderer.sprite = leverOnSprite; //change to on sprite
            StartCoroutine(MoveTrapdoor(originalTrapdoorPosition + new Vector3(moveDistance,0,0))); //move trapdoor

        }
        else 
        {
            spriteRenderer.sprite = leverOffSprite; //change back to off switch
            StartCoroutine(MoveTrapdoor(originalTrapdoorPosition)); //move back to start

        }
    }

//method to move the trapdoor back and forth to set positions after lever is pulled
    IEnumerator MoveTrapdoor(Vector3 targetPosition)
    {
        while(Vector3.Distance(trapdoor.position,targetPosition) > 0.01f)
        {
            //move trapdoor to target position at set speed
            trapdoor.position = Vector3.MoveTowards(trapdoor.position, targetPosition, moveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    //when in range of trigger, display text and allow interaction
    private void OnTriggerEnter2D(Collider2D levCollide)
    {
        if(levCollide.CompareTag("Player"))
        {
            playerNearby = true;
            pressEText.SetActive(true); //show "pres E"
        }
    }

    private void OnTriggerExit2D(Collider2D levExCollide)
    {
        if(levExCollide.CompareTag("Player"))
        {
            playerNearby = false;
            pressEText.SetActive(false);//hide text
        }
    }
}
