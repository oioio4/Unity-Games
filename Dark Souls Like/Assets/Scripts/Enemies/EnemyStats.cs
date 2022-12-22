using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyStats : CharacterStats
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        EnemyBossManager enemyBossManager;
        public UIEnemyHealthBar enemyHealthBar;
        private Renderer modelRenderer;
        private float currentDissolve = 1f;
        private float targetDissolve = 1f;

        public bool isBoss;

        public int soulsAwardedOnDeath = 50;

        private void Awake() {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            enemyBossManager = GetComponent<EnemyBossManager>();
            modelRenderer = GetComponentInChildren<Renderer>();
            maxHealth = SetMaxHealthFromHealthLevel();
            currentHealth = maxHealth;
        }

        // Start is called before the first frame update
        private void Start()
        {   
            if (!isBoss) {
               enemyHealthBar.SetMaxHealth(maxHealth); 
            }
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
                if (enemyManager.isInvulnerable) {
                    return;
                }

                currentHealth = currentHealth - damage;
                
                if (!isBoss) {
                    enemyHealthBar.SetHealth(currentHealth); 
                }
                else if (isBoss && enemyBossManager != null) {
                    enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
                }

                if (currentHealth <= 0) {
                    HandleDeath();
                }
            }
        }

        public override void TakeDamage(int damage, string damageAnimation = "Hurt") {
            if (enemyManager.isInvulnerable) {
                return;
            }

            base.TakeDamage(damage, damageAnimation = "Hurt");

            if (!isBoss) {
               enemyHealthBar.SetHealth(currentHealth); 
            }
            else if (isBoss && enemyBossManager != null) {
                enemyBossManager.UpdateBossHealthBar(currentHealth, maxHealth);
            }

            enemyAnimatorHandler.PlayTargetAnimation(damageAnimation, true);

            if (currentHealth <= 0) {
                HandleDeath();
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
