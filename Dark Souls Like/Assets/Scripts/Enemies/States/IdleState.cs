using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class IdleState : State
    {
        public PursueTargetState pursueTargetState;
        public LayerMask detectionLayer;

        private void Awake() {
            //pursueTargetState = GetComponent<PursueTargetState>();
        }

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler) {
            #region Enemy Target Detection
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);
            for (int i = 0; i < colliders.Length; i++) {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null) {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle) {
                        enemyManager.currentTarget = characterStats;
                    }
                }
            }
            #endregion

            if (enemyManager.currentTarget != null) {
                return pursueTargetState;
            }
            else {
                return this;
            }   
        }
    }
}
