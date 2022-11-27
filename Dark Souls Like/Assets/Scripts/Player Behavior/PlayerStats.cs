using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class PlayerStats : CharacterStats
    {
        PlayerManager playerManager;
        HealthBar healthBar;
        StaminaBar staminaBar;
        FocusPointBar focusPointBar;

        AnimatorHandler animatorHandler;

        public float staminaRegenerationAmount = 20f;
        public float staminaRegenTimer = 0f;

        private void Awake() {
            playerManager = GetComponent<PlayerManager>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();

            healthBar = FindObjectOfType<HealthBar>();
            staminaBar = FindObjectOfType<StaminaBar>();
            focusPointBar = FindObjectOfType<FocusPointBar>();
        }

        // Start is called before the first frame update
        void Start()
        {   
            maxHealth = SetMaxHealthFromHealthLevel();
            maxStamina = SetMaxStaminaFromStaminaLevel();
            maxFocusPoints = SetMaxFocusPointsFromFocusLevel();
            currentHealth = maxHealth;
            currentStamina = maxStamina;
            currentFocusPoints = maxFocusPoints;
            healthBar.SetMaxHealth(maxHealth);
            healthBar.SetCurrentHealth(currentHealth);
            staminaBar.SetMaxStamina(maxStamina);
            staminaBar.SetCurrentStamina(currentStamina);
            focusPointBar.SetMaxFocusPoints(maxFocusPoints);
            focusPointBar.SetCurrentFocusPoints(currentFocusPoints);
        }

        private int SetMaxHealthFromHealthLevel() {
            maxHealth = healthLevel * 10;
            return maxHealth;
        }

        private float SetMaxStaminaFromStaminaLevel() {
            maxStamina = staminaLevel * 10;
            return maxStamina;
        }

        private float SetMaxFocusPointsFromFocusLevel() {
            maxFocusPoints = focusLevel * 10;
            return maxFocusPoints;
        }

        public void TakeDamage(int damage) {
            if (playerManager.isInvulnerable) {
                return;
            }
            if (isDead) {
                return;
            }
            currentHealth = currentHealth - damage;

            healthBar.SetCurrentHealth(currentHealth);

            animatorHandler.PlayTargetAnimation("Hurt", true);

            if (currentHealth <= 0) {
                currentHealth = 0;
                animatorHandler.PlayTargetAnimation("Death", true);
                isDead = true;
            }
        }

        public void DrainStamina(int drain) {
            currentStamina -= drain;

            staminaBar.SetCurrentStamina(currentStamina);
        }

        public void DeductFocusPoints(int focusPoints) {
            currentFocusPoints = currentFocusPoints - focusPoints;

            if (currentFocusPoints < 0) {
                currentFocusPoints = 0;
            }
            
            focusPointBar.SetCurrentFocusPoints(currentFocusPoints);
        }

        public void RegenerateStamina() {
            if (playerManager.isInteracting) {
                staminaRegenTimer = 0f;
            }
            else {
                staminaRegenTimer += Time.deltaTime;
                if (currentStamina < maxStamina && staminaRegenTimer > 1f) {
                    currentStamina += staminaRegenerationAmount * Time.deltaTime;
                    staminaBar.SetCurrentStamina(Mathf.RoundToInt(currentStamina));
                }
            }
        }

        public void HealPlayer(int healAmount) {
            currentHealth = currentHealth + healAmount;

            if (currentHealth > maxHealth) {
                currentHealth = maxHealth;
            }

            healthBar.SetCurrentHealth(currentHealth);
        }
    }
}
