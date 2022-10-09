using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    public GameObject hitEffect; 
    public GameObject damageText; 
    public float damage = 1f;
    private float canAttack;

    void Start() {
        Destroy(gameObject, 5f);
    }
    
    void OnCollisionEnter2D(Collision2D collision) {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        GameObject dmg = Instantiate(damageText, transform.position, Quaternion.identity);
        Destroy(dmg, 0.2f);
        Destroy(effect, 0.2f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Enemy") {
            // Deal damage to the enemy
            Enemy enemy = collision.GetComponent<Enemy>();

            if(enemy != null) {
                //if (attackSpeed <= canAttack) {
                    enemy.Health -= damage;
                    enemy.isDamaged();
                //}
                //else {
                   // canAttack += Time.deltaTime;
               // }
            }
        }
    }
}
