using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindForceAttack : MonoBehaviour
{
    [SerializeField] float attackDelay = 0.5f; //space between attacks
    [SerializeField] GameObject windAttackPrefab; //place for wind object
    [SerializeField] Transform windProjectileSpawnPoint; //where it shoots from
    [SerializeField] float windSpeed = 8f; //speed of wind force

    private Animator animator;
    private AudioSource audioSource;
    [SerializeField] private AudioClip windAttackSFX;

    private bool canAttack = true;

    public int facingDirection = 1; //1 = right, -1 = lefft
    private PlayerController player; //player ref

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>(); //ref to player script
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(1) && canAttack)
        {
            StartCoroutine(WindAttack());

        }
    }

    private IEnumerator WindAttack()
    {
        canAttack = false; //prevent spamming
        GameObject windforce = Instantiate(windAttackPrefab,windProjectileSpawnPoint.position,Quaternion.identity);

        //Get players direction
        int direction = player.facingDirection;

        //anim
        animator.SetBool("windAttack", true);

        //apply velocity
        Rigidbody2D rigid = windforce.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(windSpeed * direction,0);

        //flip if moving left
        if(direction == -1)
        {
            windforce.transform.localScale = new Vector3(-1,1,1);
        }

        // Play SFX
        SoundFXManager.instance.PlaySoundFXClip(windAttackSFX, transform, 1f);

        yield return new WaitForSeconds(attackDelay); //wait for cooldown
        canAttack = true;
        
    }
}
