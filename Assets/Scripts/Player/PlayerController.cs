using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // References to Components
    public Rigidbody2D rb;
    private AudioSource audioSource;
    private Animator animator;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    float horizontalMovement;
    public bool canMove = true;
   

    [Header("Facing Direction")]
    public int facingDirection = 1; // 1 for right, -1 for left
  
    [Header("Jumping")]
    public float jumpPower = 8f;
    private int jumpCount =0; //tracks jumps
    private int maxJumps = 2;//allow only 2 jumps

    float jumpDir;

    const float JUMP_DIR_GROUNDED = 0.5f; //fixed values for jump direction
    const float JUMP_DIR_UPWARDS = 1f;
    const float JUMP_DIR_DOWNWARDS = 0f;

    [Header("Ground Check")]
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.8f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip jumpSFX;
    
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
    }


    void Update()
    {

        if(!canMove)
        {
            rb.velocity = new Vector2(0, rb.velocity.y); //stop movement
            return; //dont process movement
        }

        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        Gravity();
        
        //reset jumps when hit ground
        if(IsGrounded() && rb.velocity.y <0)
        {
            jumpCount = 0;
        }

        // Rotate player sprite based on direction they are moving in
        if (rb.velocity.x < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            facingDirection = -1;  // Player facing left
        }
        else if (rb.velocity.x > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            facingDirection = 1;   // Player facing right
        }


        //animation set running to true
        bool isMoving = Mathf.Abs(horizontalMovement) > 0.1f;
        animator.SetBool("isRunning", isMoving && IsGrounded());
        
        
    }

void FixedUpdate()
    {
        //update jumpDir based on vertical velocity and ground state
        if (IsGrounded())
        {
            jumpDir = JUMP_DIR_GROUNDED;
        }
        else
        {
            if (rb.velocity.y > 0f)
            {
                jumpDir = JUMP_DIR_UPWARDS;
            }
            else
            {
                jumpDir = JUMP_DIR_DOWNWARDS;
            }
        }

        //set jumpDir parameter in the animator
        animator.SetFloat("jumpDir", jumpDir);
    }
    
    private void Gravity()
    {
        if(rb.velocity.y < 0)
        {
            // Player falls increasingly faster
            rb.gravityScale = baseGravity * fallSpeedMultiplier;
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Max(rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            rb.gravityScale = baseGravity;
        }
    }



    public void MovementScript(InputAction.CallbackContext context)
    {
        horizontalMovement = context.ReadValue<Vector2>().x;
    }


    public void Jump(InputAction.CallbackContext context)
    {

        if (context.performed && jumpCount < maxJumps)
        {
            // If held down, jump at full power
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            jumpCount++;
        }
            
            // If not held down button, perform a smaller jump
        if (context.canceled && rb.velocity.y > 0 )
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.75f);
        }

        // Play Jump SFX
        SoundFXManager.instance.PlaySoundFXClip(jumpSFX, transform, 1f);
    }


    private bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast( groundCheckPosition.position, groundCheckSize,0f,Vector2.down,0.4f,groundLayer);
        return hit.collider !=null;
    }


    // Check if the player is on the ground
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPosition.position, groundCheckSize);
    }
}
