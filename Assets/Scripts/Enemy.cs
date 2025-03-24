using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //attributes
    [SerializeField] private int enemyMaxHealth = 9; //enemy health
    public float enemySpeed = 3f; //enemy speed
    public int attackDamage = 1; //amount of damage dealt
    public float detectionRange = 7f; //distance of enemy detection of player
    public float attackSpacing = 1f;// time between attacks
    private float lastDamageTime = 0f; //track when last damage dealt

    public int currentEnemyHealth; //enemy current health

    //refs
    private Transform playerPos; //ref to where player is
    private Rigidbody2D rigid; //rb ref
    private PlayerHealth playerHealth;//ref to player health
    private GameObject player; 

    private bool isWalking = false;//track movement

    [SerializeField] LayerMask groundLayerMask;//ground layer assigning
    [SerializeField] Transform groundCheck; //ref to groundcheck

    // Start is called before the first frame update
    void Start()
    {
        playerPos = GameObject.FindWithTag("Player").transform;
        rigid = GetComponent<Rigidbody2D>();
        playerHealth = GameObject.FindWithTag("Player").GetComponent<PlayerHealth>();

        currentEnemyHealth = enemyMaxHealth; //set to max at start
    }

    public void TakeDamage(int damage)
    {
        enemyMaxHealth -= damage;
        Debug.Log("enemy took damage");

        if(enemyMaxHealth <=0)
        {
            Die();
        }
    }
    void FixedUpdate()
    {
        //if within detection range of player, move to player
        //the distance is calculated by comparing player and enemy positions
        float distanceToPlayer = Vector2.Distance(transform.position, playerPos.position);
        if (distanceToPlayer <= detectionRange && IsGrounded())
        {
            MoveToPlayer();

        }
        else
        {
            Idle();
        }

        //flip function
        if(playerPos.position.x < transform.position.x)
        {
            transform.rotation = Quaternion.Euler(0,180,0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(0,0,0);
        }
    }

    //move to player method
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
           // animator.SetBool("isWalking", true);
            }
        }
    }

    private void Idle()
    {
        if (isWalking)
        {
            isWalking = false;
            //animator.SetBool("isWalking", false);
        }
    }

    private bool IsGrounded()
    {
        //ground check raycast, bounces off ground to detect ground collision
        RaycastHit2D hit = Physics2D.Raycast(groundCheck.position, Vector2.down, 0.2f,groundLayerMask);
        return hit.collider !=null; //true if ground is detected
    }

    //if player is in contact with enemy deal 1 damage per second
    private void OnCollisionStay2D(Collision2D enemyColliding)
    {
        if(enemyColliding.gameObject.CompareTag("Player"))
        {
            if (enemyColliding.gameObject.CompareTag("Player"))
            {
                //if attack space has elapsed allow more damage
                if (Time.time >= lastDamageTime + attackSpacing)
                {
                    playerHealth.TakeDamage(attackDamage); //deal damage
                    lastDamageTime = Time.time; //reset damage timer
                }
            }
        }
    }
    private void Die()
    {
        Destroy(gameObject);
    }
}
