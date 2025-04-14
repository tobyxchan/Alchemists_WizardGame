using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    // References to Components
    public Rigidbody2D rb;
    private AudioSource audioSource;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    float horizontalMovement;
   

    [Header("Facing Direction")]
    public int facingDirection = 1; // 1 for right, -1 for left
  
    [Header("Jumping")]
    public float jumpPower = 8f;
    private int jumpCount =0; //tracks jumps
    private int maxJumps = 2;//allow only 2 jumps

    [Header("Ground Check")]
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
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
    }


    void Update()
    {


        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        Gravity();
        
        //reset jumps when hit ground
        if(isGrounded() && rb.velocity.y <0)
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


    private bool isGrounded()
    {
        RaycastHit2D hit = Physics2D.BoxCast( groundCheckPosition.position, groundCheckSize,0f,Vector2.down,0.25f,groundLayer);
        return hit.collider !=null;
    }


    // Check if the player is on the ground
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPosition.position, groundCheckSize);
    }
}
