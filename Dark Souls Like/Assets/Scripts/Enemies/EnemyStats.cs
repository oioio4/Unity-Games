using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyStats : CharacterStats
    {
        EnemyAnimatorHandler enemyAnimatorHandler;
        public UIEnemyHealthBar enemyHealthBar;
        private Renderer modelRenderer;
        private float currentDissolve = 1f;
        private float targetDissolve = 1f;

        public int soulsAwardedOnDeath = 50;

        private void Awake() {
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            modelRenderer = GetComponentInChildren<Renderer>();
        }

        // Start is called before the first frame update
        private void Start()
        {   
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
            enemyHealthBar.SetMaxHealth(maxHealth);
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
                 enemyHealthBar.SetHealth(currentHealth);

                if (currentHealth <= 0) {
                    HandleDeath();
                }
            }
        }

        public override void TakeDamage(int damage, string damageAnimation = "Hurt") {
            if (!isDead) {
                currentHealth = currentHealth - damage;
                enemyHealthBar.SetHealth(currentHealth);

                enemyAnimatorHandler.PlayTargetAnimation(damageAnimation, true);

                if (currentHealth <= 0) {
                    HandleDeath();
                }
            }
        }

        private void HandleDeath() {
            currentHealth = 0;
            enemyAnimatorHandler.PlayTargetAnimation("Death", true);
            isDead = true;
            // triggers dissolve shader
            targetDissolve = 0f;
        }
    }
}
