using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    
    public float Health {
        set {
            health = value;

            if (value < 0) {
                //animator.SetTrigger("hit");
            }

            if(health <= 0) {
                //Defeated();
            }
        }
        get {
            return health;
        }
    }

    public HealthBar healthBar;
    public float maxHealth = 3f;
    private float health;
    
    public float moveSpeed = 5f;

    public Rigidbody2D rb;
    public Camera cam;

    Vector2 movement;
    Vector2 mousePos;


    void Start() {
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
    }
    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

        healthBar.SetHealth(health);
        if (health <= 0) {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    void FixedUpdate() {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;
    }
}
