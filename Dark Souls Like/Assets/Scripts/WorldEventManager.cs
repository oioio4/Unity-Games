using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class WorldEventManager : MonoBehaviour
    {
        public List<FogWall> fogWalls;
        BossHealthUI bossHealthBar;
        EnemyBossManager boss;

        public bool bossFightIsActive;
        public bool bossHasBeenAwakened;
        public bool bossHasBeenDefeated;

        private void Awake() {
            bossHealthBar = FindObjectOfType<BossHealthUI>();
        }

        public void ActivateBossFight() {
            bossFightIsActive = true;
            bossHasBeenAwakened = true;
            bossHealthBar.SetUIHealthBarToActive();

            foreach (var fogWall in fogWalls) {
                fogWall.ActivateFogWall();
            }
        }

        public void BossHasBeenDefeated() {
            bossFightIsActive = false;
            bossHasBeenDefeated = true;

            foreach (var fogWall in fogWalls) {
                fogWall.DeactivateFogWall();
            }
        }
    }
}
