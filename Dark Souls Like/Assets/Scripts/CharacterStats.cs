using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class CharacterStats : MonoBehaviour
    {
        public int healthLevel = 10;
        public int maxHealth;
        public int currentHealth;

        public int staminaLevel = 10;
        public float maxStamina;
        public float currentStamina;

        public int focusLevel = 10;
        public float maxFocusPoints;
        public float currentFocusPoints;

        public int soulCount = 0;

        [Header("Armor Absorption")]
        public float physicalDamageAbsorptionHead;
        public float physicalDamageAbsorptionBody;
        public float physicalDamageAbsorptionCape;
        public float physicalDamageAbsorptionHands;
        public float physicalDamageAbsorptionLegs;
        public float physicalDamageAbsorptionFeet;

        public bool isDead;

        public virtual void TakeDamage(int physicalDamage, string damageAnimation = "Hurt"){
            if (isDead) {
                return;
            }

            float totalPhysicalDamageAbsorption = 1 - 
            (1 - physicalDamageAbsorptionHead / 100) * 
            (1 - physicalDamageAbsorptionBody / 100) *
            (1 - physicalDamageAbsorptionCape / 100) *
            (1 - physicalDamageAbsorptionHands / 100) *
            (1 - physicalDamageAbsorptionLegs / 100) *
            (1 - physicalDamageAbsorptionFeet / 100);

            physicalDamage = Mathf.RoundToInt(physicalDamage - (physicalDamage * totalPhysicalDamageAbsorption));

            int finalDamage = physicalDamage;

            currentHealth = currentHealth - finalDamage;

            if (currentHealth <= 0) {
                currentHealth = 0;
                isDead = true;
            }
        }
    }
}
