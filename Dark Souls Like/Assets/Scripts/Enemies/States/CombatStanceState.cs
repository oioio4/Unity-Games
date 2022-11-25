using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public PursueTargetState pursueTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler) {
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            // could circle around player

            if (enemyManager.currentRecoveryTime <= 0 && enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange) {
                return attackState;
            }
            else if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange) {
                return pursueTargetState;
            }
            else {
                return this;
            }
        }
    }
}
