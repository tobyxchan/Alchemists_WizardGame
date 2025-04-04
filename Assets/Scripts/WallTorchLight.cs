using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class WallTorchLight : MonoBehaviour
{

    private Light2D torchLight; //ref to torch light

    private bool isLit = false; //track if lit or not

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    [SerializeField] private Sprite wallTorchOn;
    [SerializeField] private Sprite wallTorchOff;

    [SerializeField] private float fadeDuration = 0.3f; //time to faed out

    // Start is called before the first frame update
    void Start()
    {

        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();

        torchLight = GetComponentInChildren<Light2D>(); //light2d is child of torch and grabbed

        if(torchLight !=null)
        {
            torchLight.intensity = 0f; //start with no light
        }

        if(animator !=null)
        {
            animator.SetBool("isLit", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D fireCollide)
    {
        if(fireCollide.CompareTag("Fireball") && !isLit)
        {
            IgniteTorch();
            Destroy(fireCollide.gameObject);
        }
    }

    private void IgniteTorch()
    {
        if(torchLight !=null)
        {
            isLit = true; 
            torchLight.intensity = 2f; //light torch
            Invoke(nameof(Extinguish), 8f); //call extinguish method after 7 seconds
            spriteRenderer.sprite = wallTorchOn; //switch to on sprite
            animator.SetBool("isLit", true);
        }
    }
        public void Extinguish()
        {
            if(!isLit) return; //if extinguished do nothing
            isLit = false;
            spriteRenderer.sprite = wallTorchOff;
            animator.SetBool("isLit",false);

            if(torchLight !=null)
            {
                StartCoroutine(FadeOutLight()); //smothly fade light
            }
        }

        private IEnumerator FadeOutLight()
        {
            float startIntensity = torchLight.intensity;
            float elapsedTime = 0f;

            //continue to fade until duration finished
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                torchLight.intensity = Mathf.Lerp(startIntensity,0f,elapsedTime/fadeDuration);
                yield return null;

            }

            torchLight.intensity = 0f; //ensure fully off
        }
    
}
