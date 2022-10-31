using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float startSpeed= 10f;
    [HideInInspector]
    public float speed = 10f;

    public float health = 100f;

    public int value = 50;

    public GameObject deathEffect;

    void Start() {
        speed = startSpeed;
    }

    public void TakeDamage(float dmg) {
        health -= dmg;
        
        if (health <= 0) {
            Die();
        }
    }

    public void Slow(float pct) {
        speed = startSpeed * (1f - pct);
    }

    void Die() {
        PlayerStats.Money += value;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);
        Destroy(gameObject);
    }
}
