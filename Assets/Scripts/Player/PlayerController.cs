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
    [SerializeField] public List<AudioClip> soundEffects; //SFX Manager

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 6f;
    float horizontalMovement;

    [Header("Facing Direction")]
    public int facingDirection = 1; // 1 for right, -1 for left
  
    [Header("Jumping")]
    public float jumpPower = 6f;

    [Header("Ground Check")]
    public Transform groundCheckPosition;
    public Vector2 groundCheckSize = new Vector2(0.5f, 0.05f);
    public LayerMask groundLayer;

    [Header("Gravity")]
    public float baseGravity = 2;
    public float maxFallSpeed = 18f;
    public float fallSpeedMultiplier = 2f;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        rb.velocity = new Vector2(horizontalMovement * moveSpeed, rb.velocity.y);
        Gravity();
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
        if(isGrounded())
        {
            if (context.performed)
            {
                // If held down, jump at full power
                rb.velocity = new Vector2(rb.velocity.x, jumpPower);
                PlayJumpSFX();
            }
                // If not held down button, perform a smaller jump
                else if (context.canceled)
            {
                rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * 0.5f);
                PlayJumpSFX();
            }
        }
    }


    private bool isGrounded()
    {
        if (Physics2D.OverlapBox(groundCheckPosition.position, groundCheckSize, 0, groundLayer))
        {
            return true;
        }
        return false;
    }


    // Check if the player is on the ground
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawWireCube(groundCheckPosition.position, groundCheckSize);
    }

    public void PlayJumpSFX()
    {
        // When player jumps, play first sound effect in SFX list
        AudioSource.PlayClipAtPoint(soundEffects[0], transform.position);
    }
}
