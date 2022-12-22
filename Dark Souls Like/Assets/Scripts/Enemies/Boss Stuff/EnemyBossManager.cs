using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyBossManager : MonoBehaviour
    {
        public string bossName;

        BossHealthUI bossHealthBar;
        EnemyStats enemyStats;
        EnemyAnimatorHandler enemyAnimatorHandler;
        BossCombatStanceState bossCombatStanceState;

        [Header("Second Phase FX")]
        public GameObject particleFX;

        private void Awake() {
            bossHealthBar = FindObjectOfType<BossHealthUI>();
            enemyStats = GetComponent<EnemyStats>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            bossCombatStanceState = GetComponentInChildren<BossCombatStanceState>();
        }

        private void Start() {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth, int maxHealth) {
            bossHealthBar.SetBossCurrentHealth(currentHealth);

            if (currentHealth <= maxHealth / 2 && !bossCombatStanceState.hasPhaseShifted) {
                bossCombatStanceState.hasPhaseShifted = true;
                ShiftToSecondPhase();
            }
        }

        public void ShiftToSecondPhase() {
            enemyAnimatorHandler.anim.SetBool("isInvulnerable", true);
            enemyAnimatorHandler.anim.SetBool("isPhaseShifting", true);
            enemyAnimatorHandler.PlayTargetAnimation("Phase Shift", true);
            bossCombatStanceState.hasPhaseShifted = true;
        }
    }
}
