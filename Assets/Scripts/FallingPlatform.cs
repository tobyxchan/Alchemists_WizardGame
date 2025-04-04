using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingPlatform : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private bool isCrumbling = false; //prevent multiple triggers, toggle 

    [SerializeField] private float crumbleDelay = 1f;//time before falling
    

    [SerializeField] private Animator animator;

    

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();


    }

   private void OnCollisionEnter2D(Collision2D floorCollide)
   {

    if(floorCollide.gameObject.CompareTag("Player") && !isCrumbling)
    {
        StartCoroutine(Crumble());
    }
   }

   private IEnumerator Crumble()
   {
    isCrumbling = true;

    //trigger crumble anim
    if(animator !=null)
    {
        animator.SetTrigger("Crumble");
    }

    yield return new WaitForSeconds(crumbleDelay); //wait 1 second

    //remove sprite and collider
    spriteRenderer.enabled = false;
    boxCollider.enabled = false;

   }

}
