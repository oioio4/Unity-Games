using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class AttackState : State
    {
        public CombatStanceState combatStanceState;
        public PursueTargetState pursueTargetState;
        public RotateTowardsTargetState rotateTowardsTargetState;

        public EnemyAttackAction currentAttack;

        bool willDoCombo = false;
        public bool hasPerformedAttack = false;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler) {
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            HandleRotateTowardsTarget(enemyManager);

            if (distanceFromTarget > enemyManager.maximumAttackRange) {
                return pursueTargetState;
            }

            if (willDoCombo && enemyManager.canDoCombo) {
                AttackTargetWithCombo(enemyAnimatorHandler, enemyManager);
            }

            if (!hasPerformedAttack) {
                AttackTarget(enemyAnimatorHandler, enemyManager);
                RollForComboChance(enemyManager);
            }

            if (willDoCombo && hasPerformedAttack) {
                return this;
            }

            return rotateTowardsTargetState;
        }

        private void AttackTarget(EnemyAnimatorHandler enemyAnimatorHandler, EnemyManager enemyManager) {
            enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            hasPerformedAttack = true;
        }

        private void AttackTargetWithCombo(EnemyAnimatorHandler enemyAnimatorHandler, EnemyManager enemyManager) {
            willDoCombo = false;
            enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
            enemyManager.currentRecoveryTime = currentAttack.recoveryTime;
            currentAttack = null;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager) {
            if (enemyManager.canRotate && enemyManager.isInteracting) {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero) {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
            }
        }

        private void RollForComboChance(EnemyManager enemyManager) {
            float comboChance = Random.Range(0, 100);

            if (enemyManager.allowAIToPerformCombo && comboChance <= enemyManager.comboLikelyhood) {
                if (currentAttack.comboAction != null) {
                    willDoCombo = true;
                    currentAttack = currentAttack.comboAction;
                }
                else {
                    willDoCombo = false;
                    currentAttack = null;
                }
            }
        }
    }
}
