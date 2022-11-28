using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyStats : CharacterStats
    {
        Animator animator;
        private Renderer modelRenderer;
        public float currentDissolve = 1f;
        public float targetDissolve = 1f;

        private void Awake() {
            animator = GetComponentInChildren<Animator>();
            modelRenderer = GetComponentInChildren<Renderer>();
        }

        // Start is called before the first frame update
        private void Start()
        {   
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        private void Update() {
            currentDissolve = Mathf.Lerp(currentDissolve, targetDissolve, 0.5f * Time.deltaTime);
            modelRenderer.material.SetFloat("_Health", currentDissolve);
        }

        private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        public void TakeDamageNoAnimation(int damage) {
            if (!isDead) {
                currentHealth = currentHealth - damage;

                if (currentHealth <= 0) {
                    currentHealth = 0;
                    //animator.Play("Death");
                    isDead = true;
                }
            }
        }

        public void TakeDamage(int damage) {
            if (!isDead) {
                currentHealth = currentHealth - damage;

                animator.Play("Hurt");

                if (currentHealth <= 0) {
                    currentHealth = 0;
                    animator.Play("Death");
                    isDead = true;
                    // triggers dissolve shader
                    targetDissolve = 0f;
                }
            }
        }
    }
}
