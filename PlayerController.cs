using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private float groundedDistance;
    [SerializeField] private float coyoteTime;
    private float coyoteTimer = 0.0f;

    public Animator animator;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // CHECKING IF THE PLAYER IS ON THE GROUND
        var rayPosition = spriteRenderer.bounds.center;
        rayPosition.y -= spriteRenderer.bounds.extents.y + groundedDistance*2;
        var isGrounded = Physics2D.Raycast(rayPosition, -transform.up, 
            groundedDistance);
        
        animator.SetBool("Grounded", isGrounded);

        // COYOTE TIME INCREMENTS + CHECKS
        if (isGrounded)
        {
            coyoteTimer = 0;
        }
        else
        {
            coyoteTimer += Time.deltaTime;
        }

        // JUMP INPUT
        if (Input.GetButtonDown("Jump") && (isGrounded || coyoteTimer < coyoteTime))
        {
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, jumpVelocity);
            coyoteTimer = coyoteTime;
        }

        // HORIZONTAL MOVEMENT INPUT
        if (Input.GetAxis("Horizontal") < 0)
        {
            rigidBody.velocity = new Vector2(moveSpeed * -1.0f, rigidBody.velocity.y);
            animator.SetBool("Moving", true);
            rigidBody.transform.localScale = new Vector3(-1, 1, 1);
        }
        else if (Input.GetAxis("Horizontal") > 0)
        {
            rigidBody.velocity = new Vector2(moveSpeed, rigidBody.velocity.y);
            animator.SetBool("Moving", true);
            rigidBody.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            rigidBody.velocity = new Vector2(0.0f, rigidBody.velocity.y);
            animator.SetBool("Moving", false);
        }
    }
}
