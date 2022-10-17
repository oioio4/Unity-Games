using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    Animator animator;

    public float Health {
        set {
            health = value;

            if (value < 0) {
               animator.SetTrigger("hit");
            }

            if(health <= 0) {
               Defeated();
            }
        }
        get {
            return health;
        }
    }

    public GameObject damageText; 

    public HealthBar healthBar;
    public float maxHealth = 3f;
    private float health;

    public float damage = 1f;
    public float attackSpeed = 1f;
    private float curTime;

    public float moveSpeed = 5f;
    private bool damaged = false;
    public Rigidbody2D rb;
    GameObject player;

    void Start() {
        animator = GetComponent<Animator>();
        animator.SetBool("moving", true);
        healthBar.SetMaxHealth(maxHealth);
        health = maxHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        curTime = 0f;
    }

    void Update() {
        if (damaged) {
            animator.SetBool("hit", true);
            GameObject dmg = Instantiate(damageText, transform.position, Quaternion.identity);
            Destroy(dmg, 0.2f);
            damaged = false;
        }
        else {
            animator.SetBool("hit", false);
        }
        healthBar.SetHealth(health);
    }

    void FixedUpdate() {
        Vector2 dir = player.transform.position - transform.position;
        rb.MovePosition(rb.position + dir * moveSpeed * Time.fixedDeltaTime);
    }

    public void Defeated(){
        ScoreText.scoreNum += 10;
        Destroy(gameObject);
    }

    public void isDamaged() {
        damaged = true;
    }

    public void increaseHealth(float hp) {
        maxHealth += hp;
    }

    public void setHealth(float hp) {
        maxHealth = hp;
    }

    void OnTriggerStay2D(Collider2D collision) {
        if(collision.tag == "Player") {
            // Deal damage to the enemy
            PlayerControl player = collision.GetComponent<PlayerControl>();

            if(player != null) {
                if (curTime <= 0) {
                    player.Health -= damage;
                    curTime = attackSpeed;
                }
                else {
                    curTime -= Time.deltaTime;
                }
            }
        }
    }
}
