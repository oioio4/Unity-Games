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

        private void Awake() {
            bossHealthBar = FindObjectOfType<BossHealthUI>();
            enemyStats = GetComponent<EnemyStats>();
        }

        private void Start() {
            bossHealthBar.SetBossName(bossName);
            bossHealthBar.SetBossMaxHealth(enemyStats.maxHealth);
        }

        public void UpdateBossHealthBar(int currentHealth) {
            bossHealthBar.SetBossCurrentHealth(currentHealth);
        }
    }
}
