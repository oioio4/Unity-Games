using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public float startSpeed= 10f;
    [HideInInspector]
    public float speed = 10f;

    public float startHealth = 100f;
    private float health;

    public int value = 50;

    public GameObject deathEffect;

    [Header("Unity Things")]
    public Image healthBar;

    private bool Dead = false;

    void Start() {
        speed = startSpeed;
        health = startHealth;
    }

    public void TakeDamage(float dmg) {
        health -= dmg;

        healthBar.fillAmount = health / startHealth;
        
        if (health <= 0 && !Dead) {
            Die();
        }
    }

    public void Slow(float pct) {
        speed = startSpeed * (1f - pct);
    }

    void Die() {
        Dead = true;

        PlayerStats.Money += value;

        GameObject effect = Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(effect, 5f);

        WaveSpawner.EnemiesAlive--;
        Destroy(gameObject);
    }
}
