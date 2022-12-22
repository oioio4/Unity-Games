using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyAnimatorHandler : AnimatorManager
    {
        EnemyManager enemyManager;
        EnemyBossManager enemyBossManager;
        EnemyStats enemyStats;

        private void Awake() {
            anim = GetComponent<Animator>();
            enemyManager= GetComponentInParent<EnemyManager>();
            enemyBossManager = GetComponentInParent<EnemyBossManager>();
            enemyStats = GetComponentInParent<EnemyStats>();
        }

        public override void TakeCriticalDamage() {
            enemyStats.TakeDamageNoAnimation(enemyManager.pendingCriticalDamage);
            enemyManager.pendingCriticalDamage = 0;
        }

        public void EnableCombo() {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo() {
            anim.SetBool("canDoCombo", false);
        }

        public void EnableIsInvulnerable() {
            anim.SetBool("isInvulnerable", true);
        }

        public void DisableIsInvulnerable() {
            anim.SetBool("isInvulnerable", false);
        }

        public void EnableIsParrying() {
            enemyManager.isParrying = true;
        }

        public void DisableIsParrying() {
            enemyManager.isParrying = false;
        }

        public void EnableCanBeRiposted() {
            enemyManager.canBeRiposted = true;
        }

        public void DisableCanBeRiposted() {
            enemyManager.canBeRiposted = false;
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

        public void InstantiateBossParticleFX() {
            BossFXTransform bossFXTransform = GetComponentInChildren<BossFXTransform>();

            GameObject phaseFX = Instantiate(enemyBossManager.particleFX, bossFXTransform.transform);
        }

        private void OnAnimatorMove() {
            float delta = Time.deltaTime;
            enemyManager.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyManager.enemyRigidbody.velocity = velocity;

            if (enemyManager.isRotatingWithRootMotion) {
                enemyManager.transform.rotation *= anim.deltaRotation;
            }
        }
    }
}
