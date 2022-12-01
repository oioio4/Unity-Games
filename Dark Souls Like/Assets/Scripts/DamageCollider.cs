using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class DamageCollider : MonoBehaviour
    {
        public CharacterManager characterManager;
        BoxCollider damageCollider;

        public int curDamage = 25;

        private void Awake() {
            damageCollider = GetComponent<BoxCollider>();
            damageCollider.gameObject.SetActive(true);
            damageCollider.isTrigger = true;
            damageCollider.enabled = false;
        }

        public void EnableDamageCollider() {
            damageCollider.enabled = true;
        }

        public void DisableDamageCollider() {
            damageCollider.enabled = false;
        }

        private void OnTriggerEnter(Collider collision) {
            if (collision.tag == "Player") {
                PlayerStats playerStats = collision.GetComponent<PlayerStats>();
                CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

                if (enemyCharacterManager != null) {
                    if (enemyCharacterManager.isParrying) {
                        // check if able to parry
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking) {
                        float physicalDamageAfterBlock = curDamage - (curDamage * shield.blockingPhysicalDamageAbsorption / 100);

                        if (playerStats != null) {
                            playerStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Blocked");
                            return;
                        }
                    }
                }

                if (playerStats != null) {
                    playerStats.TakeDamage(curDamage);
                }
            }

            if (collision.tag == "Enemy") {
                EnemyStats enemyStats = collision.GetComponent<EnemyStats>();
                CharacterManager enemyCharacterManager = collision.GetComponent<CharacterManager>();
                BlockingCollider shield = collision.transform.GetComponentInChildren<BlockingCollider>();

                if (enemyCharacterManager != null) {
                    if (enemyCharacterManager.isParrying) {
                        // check if able to parry
                        characterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Parried", true);
                        return;
                    }
                    else if (shield != null && enemyCharacterManager.isBlocking) {
                        float physicalDamageAfterBlock = curDamage - (curDamage * shield.blockingPhysicalDamageAbsorption / 100);

                        if (enemyStats != null) {
                            enemyStats.TakeDamage(Mathf.RoundToInt(physicalDamageAfterBlock), "Blocked");
                            return;
                        }
                    }
                }

                if (enemyStats != null) {
                    enemyStats.TakeDamage(curDamage);
                }
            }
        }
    }
}
