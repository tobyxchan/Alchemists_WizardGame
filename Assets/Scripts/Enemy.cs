using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Attributes
    [SerializeField] private int enemyMaxHealth = 9; //enemy health
    public float enemySpeed = 3f; //enemy speed
    public int attackDamage = 1; //amount of damage dealt
    public float detectionRange = 7f; //distance of enemy detection of player
    public float attackSpacing = 1f;// time between attacks
    private float lastDamageTime = 0f; //track when last damage dealt
    public int currentEnemyHealth; //enemy current health

    [SerializeField] private GameObject deathParticlePrefab;

    

    // References
    private Transform playerPos; //ref to where player is
    private Rigidbody2D rigid; //rb ref
    private PlayerHealth playerHealth;//ref to player health
    private GameObject player; // ref to player
    private AudioSource audioSource; // ref to audio source

    private GameManager gameManager; //manager ref

    private bool isWalking = false;//track movement

    private Animator animator;

    [SerializeField] LayerMask groundLayerMask;//ground layer assigning
    [SerializeField] Transform groundCheck; //ref to groundcheck
    [SerializeField] private AudioClip damageSFX;




    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform; //find player location
        rigid = GetComponent<Rigidbody2D>(); //get rigid body
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();
        audioSource = GetComponent<AudioSource>();

        gameManager = GameObject.Find("Managers").GetComponent<GameManager>();

        currentEnemyHealth = enemyMaxHealth; //set to max at start

        animator = GetComponent<Animator>();
    }

    //method to deal damage to enemy. if enemy reaches 0 health they die
    public void TakeDamage(int damage)
    {
        currentEnemyHealth = Mathf.Max(0, currentEnemyHealth - damage); //reduce current by damage

        Debug.Log($"Enemy took {damage} damage, remaining health: {currentEnemyHealth}");

//kill if 0 health
        if(currentEnemyHealth <=0)
        {
            Die();
        }

        // Play damage SFX
        SoundFXManager.instance.PlaySoundFXClip(damageSFX, transform, 1f);
    }

    void FixedUpdate()
    {
        //stop player tracking once won
        if(gameManager.hasPlayerWon)
        {
            Idle();
            return;
        }

        //if within detection range of player, move to player
        //the distance is calculated by comparing player and enemy positions
        //must be on same y axis and within x axis range
        float distanceToPlayerX = Mathf.Abs(playerPos.position.x - transform.position.x);
        float distanceToPlayerY = Mathf.Abs(playerPos.position.y - transform.position.y);

        //y range threshold
        float yDetectionThreshold = 0.5f;
        
        // logic for checking if enemy should be idle or moving
        if (distanceToPlayerX <= detectionRange && distanceToPlayerY < yDetectionThreshold && IsGrounded())
        {
            MoveToPlayer();

        }
        else
        {
            Idle();
        }

        //flip sprite function
        if(playerPos.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    // Move to player method
    private void MoveToPlayer()
    {
        if(IsGrounded())
        {
        //vector points from enemy to player
        Vector2 moveDirection = (playerPos.position - transform.position).normalized;

        //moves in that direction at enemy speed within real time on x axis
        Vector2 enemyMovement = new Vector2(moveDirection.x * enemySpeed * Time.deltaTime,0);
        transform.position += (Vector3) enemyMovement;

        //update walking anim
        if(!isWalking)
            {
            isWalking = true;
            animator.SetBool("isMoving", true);
            }
        }
    }

    private void Idle()
    {
        if (isWalking)
        {
            isWalking = false;
            animator.SetBool("isMoving", false);
        }
    }

    private bool IsGrounded()
    {
        //ground check raycast, bounces off ground to detect ground collision
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f,groundLayerMask);
        return hit.collider !=null; //true if ground is detected
    }

    // If player is in contact with enemy deal 1 damage per second
    private void OnTriggerStay2D(Collider2D enemyColliding)
    {
        if(enemyColliding.gameObject.CompareTag("Player"))
        {
                //if attack space has elapsed allow more damage
                if (Time.time >= lastDamageTime + attackSpacing)
                {
                    playerHealth.TakeDamage(attackDamage); //deal damage
                    lastDamageTime = Time.time; //reset damage timer
                }
            
        }
    }

    private void Die()
    {
        //spawn particle sim
        if(deathParticlePrefab !=null)
        {
            Instantiate(deathParticlePrefab, transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
