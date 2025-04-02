using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Fireplace : MonoBehaviour
{
    private Light2D fireLight; //ref to fireplace light

    private bool isLit = false; //track if lit or not

    private SpriteRenderer spriteRenderer; 

    private Animator animator;

    [SerializeField] private Sprite firePlaceOn;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //light is child of main sprite
        animator = GetComponent<Animator>();

        fireLight = GetComponentInChildren<Light2D>();

        if(fireLight !=null)
        {
            fireLight.intensity = 0f; //start with no light
        }

        if(animator !=null)
        {
            animator.SetBool("isLit", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D fireplaceCollide)
    {
        if(fireplaceCollide.CompareTag("Fireball") && !isLit)
        {
            IgniteFire();
            Destroy(fireplaceCollide.gameObject);
        }
    }

    private void IgniteFire()
    {
        if(fireLight !=null)
        {
            isLit = true;
            fireLight.intensity = 3f; //light fire with set intensity
            spriteRenderer.sprite = firePlaceOn; //switch sprite on
            animator.SetBool("isLit", true);
        }
    }
}
