using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallAttack : MonoBehaviour
{

    [SerializeField] float attackDelay = 0.4f; //delay between attacks
    [SerializeField] GameObject fireballPrefab; //fireball projectile
    [SerializeField] Transform projectileSpawnPoint;// where it shoots from
    [SerializeField] float projectileSpeed = 8f;//speed of ball

    private bool canAttack = true; 
    public int facingDirection = 1; // 1 = right, -1 = left
    private PlayerController player; //ref to player

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<PlayerController>(); //get ref to player script

    }

    // Update is called once per frame
    void Update()
    {
        
        if( Input.GetMouseButtonDown(0)&& canAttack)
        {
            StartCoroutine(FireAttack());
        }
        
    }
    private IEnumerator FireAttack()
    {
        canAttack = false;//prevent spamming
        GameObject fireball = Instantiate(fireballPrefab,projectileSpawnPoint.position,Quaternion.identity);


        // Get player's direction
        int direction = player.facingDirection;

        // Apply velocity
        Rigidbody2D rigid = fireball.GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(projectileSpeed * direction, 0);

        // Flip fireball sprite if moving left
        if (direction == -1)
            fireball.transform.localScale = new Vector3(-1, 1, 1);

        yield return new WaitForSeconds(attackDelay); //wait for cooldown
        canAttack = true;
    }
}
