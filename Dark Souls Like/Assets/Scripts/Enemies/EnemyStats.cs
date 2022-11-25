using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyStats : CharacterStats
    {
        private bool isDead = false;

        Animator animator;

        private void Awake() {
            animator = GetComponentInChildren<Animator>();
        }

        // Start is called before the first frame update
        void Start()
        {   
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamage(int damage) {
            if (!isDead) {
                currentHealth = currentHealth - damage;

                animator.Play("Hurt");

                if (currentHealth <= 0) {
                    currentHealth = 0;
                    animator.Play("Death");
                    isDead = true;
                }
            }
        }
    }
}
