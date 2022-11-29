using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyAnimatorHandler : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyStats enemyStats;

        private void Awake() {
            anim = GetComponent<Animator>();
            enemyManager= GetComponentInParent<EnemyManager>();
            enemyStats = GetComponentInParent<EnemyStats>();
        }

        public override void TakeCriticalDamage() {
            enemyStats.TakeDamageNoAnimation(enemyManager.pendingCriticalDamage);
            enemyManager.pendingCriticalDamage = 0;
        }

        public void AwardSoulsOnDeath() {
            PlayerStats playerStats = FindObjectOfType<PlayerStats>();
            SoulCountUI soulCountUI = FindObjectOfType<SoulCountUI>();

            if (playerStats != null) {
                playerStats.AddSouls(enemyStats.soulsAwardedOnDeath);

                if (soulCountUI != null) {
                    soulCountUI.SetSoulCountText(playerStats.soulCount);
                }
            }
        }

        private void OnAnimatorMove() {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidbody.velocity = velocity;
        }
    }
}
