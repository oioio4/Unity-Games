using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    /* jump mechanics */
    [SerializeField] private bool jumpHold = false;
    [SerializeField] private bool jumping = false;
    [SerializeField] private bool falling = false;
    [SerializeField] private bool landing = false;
    [SerializeField] private bool canMove = true;
    public float jumpStrength = 0f;
    [SerializeField] private float horizontalStrength = 6f;
    [SerializeField] private float screenDiv;

    [SerializeField] private bool left = false;
    [SerializeField] private float jumpCooldown = 0.2f;
    [SerializeField] private float jumpTimer = 0f;

    [SerializeField] private bool isGrounded = true;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private PhysicsMaterial2D bounceMat, normMat;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2d;
    private SpriteRenderer sr;
    private Animator animator;
    private AudioManager audioManager;

    public Vector2 touchPos;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxCollider2d = GetComponent<BoxCollider2D>();
        sr = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
        
        screenDiv = Screen.width / 2;

        rb.sharedMaterial = normMat;
    }

    // Update is called once per frame
    void Update()
    {
        // ground check 
        isGrounded = groundCheck();

        // State Machine
        animator.SetBool("Ground", isGrounded);
        animator.SetBool("JumpHold", jumpHold);
        animator.SetBool("Jump", jumping);
        animator.SetBool("Fall", falling);
        animator.SetBool("Land", landing);

        // while in the air
        if (jumping || falling) {

            // increment jump timer
            jumpTimer += Time.deltaTime;

            if (jumpTimer > 0.3f && isGrounded) {
                jumping = false;
                jumpTimer = 0f;
            }

            // check to bounce off walls when jumping 
            
            int dir = left ? -1 : 1;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * dir, 1.5f, groundLayer);
            Debug.DrawRay(transform.position, Vector2.right * 1.5f * dir, Color.red);

            if (hit.collider != null) {
                /*
                float speed = rb.velocity.magnitude;
                Vector3 direction = Vector3.Reflect(rb.velocity.normalized, hit.normal);

                rb.velocity = direction * speed;
                */

                rb.sharedMaterial = bounceMat;
            } else {
                rb.sharedMaterial = normMat;
            }
        }

        // check if falling in the air after jumping
        if (!isGrounded && jumping && rb.velocity.y < 0.5f) {
            falling = true;
        }


        // check if falling long enough to land face first
        if (falling) {
            jumping = false;
            if (jumpTimer >= 2f) {
                canMove = false;
                landing = true;
            } 
        }

        // check transition when hitting ground after falling
        if (isGrounded) {
            rb.sharedMaterial = normMat;

            if (landing) {
                Invoke("ReenableMovement", 1f);
                jumpTimer = 0f;
            } else if (falling) {
                falling = false;
                audioManager.Play("Land");
                jumpTimer = 0f;
            }
        }

        if (isGrounded && canMove) {

            // if not holding jump then count down jump cooldown timer
            if (!jumpHold) {
                jumpCooldown -= Time.deltaTime;
            }
            
            
            if (Input.touchCount > 0) {
                Touch touch = Input.GetTouch(0);
                touchPos = touch.position;
            
            
            // Keyboard input changing direction
            /*
            if (Input.GetKeyDown(KeyCode.A)) {
                left = true;
            } else if (Input.GetKeyDown(KeyCode.D)) {
                left = false;
            }
            */

            if (touch.phase == TouchPhase.Began /* Input.GetKeyDown(KeyCode.Space)*/ && jumpCooldown <= 0f) {
                if (jumpHold == false) {
                    jumpHold = true;
                    
                    /* check direction of jump & flip sprite accordingly */
                    
                    if (touchPos.x < screenDiv) {
                        left = true;
                    } else {
                        left = false;
                    }

                    if (left) {
                        sr.flipX = true;
                    } else {
                        sr.flipX = false;
                    }
                    
                }

                jumpCooldown = 0.2f;
            } 

            if (jumpHold) {
                jumpStrength += Time.deltaTime*40;
            }
            
            if (touch.phase == TouchPhase.Ended /*Input.GetKeyUp(KeyCode.Space)*/ && jumpHold) {
                /* ceiling on jump strength */
                jumpStrength = Mathf.Min(jumpStrength, 40f);

                /* determine direction & strength of jump */
                int dir = left ? -1 : 1;

                rb.velocity = new Vector2(dir * horizontalStrength, jumpStrength);

                audioManager.Play("Jump");

                /* resets jump strength and jumphold vars */
                Invoke("ResetJump", 0.01f);
            }
            //} 
        }
        }
    }

    private void ResetJump() {
        jumping = true;
        jumpHold = false;
        jumpStrength = 0;
    }

    private void ReenableMovement() {
        falling = false;
        landing = false;
        canMove = true;
    }

    private void LandSound() {
        audioManager.Play("Fall");
    }

    private bool groundCheck() {
        float extraHeight = 1f;
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider2d.bounds.center, boxCollider2d.bounds.size, 0f, Vector2.down, extraHeight, groundLayer);

        return raycastHit.collider != null;
    }
}

