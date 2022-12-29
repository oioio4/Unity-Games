using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public LayerMask obstaclesLayer;

    private bool isMoving;
    private bool isJumping;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private Animator animator;

    private float jumpTimer = 0.05f;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded()) {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);;
            isJumping = true;
            jumpTimer = 0.05f;
        }

        if (!isGrounded()) {
            jumpTimer -= Time.deltaTime;
        }
        else if (jumpTimer <= 0) {
            isJumping = false;
        }

        animator.SetBool("isJumping", isJumping);
    }

    private void FixedUpdate() {
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        float dirX = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(dirX * moveSpeed, rb.velocity.y);
        if (dirX != 0) {
            animator.SetFloat("Xmove", dirX);
            isMoving = true;
        }
        else {
            isMoving = false;
        }
        animator.SetBool("isMoving", isMoving);
    }

    private bool isGrounded() {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, 0.05f, obstaclesLayer);
        return hit.collider != null;
    }
}
