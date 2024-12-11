using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public Rigidbody2D playerRb;
    public float speed;
    public float input;
    public SpriteRenderer spriteRenderer;
    public float jumpForce;

    public LayerMask groundLayer;
    public bool isGrounded;
    public Transform feetPosition;
    public float groundCheckCircle;

    public float jumpTime = 0.35f;
    public float jumpTimeCounter;
    public bool isJumping;

    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        //Horizontal movement
        input = Input.GetAxisRaw("Horizontal");
        if(input < 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            spriteRenderer.flipX = true;
        }
        else if(input > 0)
        {
            anim.SetBool("isIdle", false);
            anim.SetBool("isWalking", true);
            spriteRenderer.flipX = false;
        }
        else
        {
            anim.SetBool("isIdle", true);
            anim.SetBool("isWalking", false);
        }
        //Vertical movement

        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundCheckCircle, groundLayer);


        if (isGrounded == true && Input.GetButtonDown("Jump"))
        {
            isJumping = true;
            jumpTimeCounter = jumpTime;
            playerRb.linearVelocity = Vector2.up * jumpForce;
        }

        if (Input.GetButton("Jump") && isJumping == true)
        {
            if(jumpTimeCounter > 0)
            {
                playerRb.linearVelocity = Vector2.up * jumpForce;
                jumpTimeCounter -= Time.deltaTime;
            }
            else
            {
                isJumping = false;
            }
        }

        if (Input.GetButtonUp("Jump"))
        {
            isJumping = false;
        }
    }


    void FixedUpdate()
    {
        playerRb.linearVelocity = new Vector2(input * speed, playerRb.linearVelocity.y);
    }
}