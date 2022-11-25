using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyManager : CharacterManager
    {
        EnemyMovementManager enemyMovementManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        public bool isPerformingAction;

        public EnemyAttackAction[] enemyAttacks;
        public EnemyAttackAction currentAttack;

        [Header("A.I. Settings")]
        public float detectionRadius = 20f;
        public float maximumDetectionAngle = 50f;
        public float minimumDetectionAngle = -50f;

        public float currentRecoveryTime = 0f;

        private void Awake() {
            enemyMovementManager = GetComponent<EnemyMovementManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        }

        private void Update() {
            HandleRecoveryTimer();
        }

        private void FixedUpdate() {
            HandleCurrentAction();
        }

        private void HandleCurrentAction() {
            if (enemyMovementManager.currentTarget != null) {
                enemyMovementManager.distanceFromTarget = 
                Vector3.Distance(enemyMovementManager.currentTarget.transform.position, transform.position);
            }

            if (enemyMovementManager.currentTarget == null) {
                enemyMovementManager.HandleDetection();
            }
            else if (enemyMovementManager.distanceFromTarget > enemyMovementManager.stoppingDistance){
                enemyMovementManager.HandleMoveToTarget();
            }
            else if (enemyMovementManager.distanceFromTarget <= enemyMovementManager.stoppingDistance){
                AttackTarget();
            }
        }

        private void HandleRecoveryTimer() {
            if (currentRecoveryTime > 0) {
                currentRecoveryTime -= Time.deltaTime;
            }

            if (isPerformingAction) {
                if (currentRecoveryTime <= 0) {
                    isPerformingAction = false;
                }
            }
        }

        private void AttackTarget() {
            if (isPerformingAction) {
                return;
            }
            if (currentAttack == null) {
                GetNewAttack();
            }
            else {
                isPerformingAction = true;
                currentRecoveryTime = currentAttack.recoveryTime;
                enemyAnimatorHandler.PlayTargetAnimation(currentAttack.actionAnimation, true);
                currentAttack = null;
            }
        }

        private void GetNewAttack() {
            Vector3 targetDirection = enemyMovementManager.currentTarget.transform.position - transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);
            enemyMovementManager.distanceFromTarget = Vector3.Distance(enemyMovementManager.currentTarget.transform.position, transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++) {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyMovementManager.distanceFromTarget  <= enemyAttackAction.maximumDistanceNeededToAttack
                    && enemyMovementManager.distanceFromTarget  >= enemyAttackAction.minimumDistanceNeededToAttack) {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                            && viewableAngle >= enemyAttackAction.minimumAttackAngle) {
                                maxScore += enemyAttackAction.attackScore;
                            }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++) {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (enemyMovementManager.distanceFromTarget  <= enemyAttackAction.maximumDistanceNeededToAttack
                    && enemyMovementManager.distanceFromTarget  >= enemyAttackAction.minimumDistanceNeededToAttack) {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                            && viewableAngle >= enemyAttackAction.minimumAttackAngle) {
                                if (currentAttack != null) {
                                    return;
                                }

                                temporaryScore += enemyAttackAction.attackScore;

                                if (temporaryScore > randomValue) {
                                    currentAttack = enemyAttackAction;
                                }
                            }
                }
            }
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red; //replace red with whatever color you prefer
            Gizmos.DrawWireSphere(transform.position, detectionRadius);

            Vector3 fovLine1 = Quaternion.AngleAxis(maximumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Vector3 fovLine2 = Quaternion.AngleAxis(minimumDetectionAngle, transform.up) * transform.forward * detectionRadius;
            Gizmos.color = Color.blue;
            Gizmos.DrawRay(transform.position, fovLine1);
            Gizmos.DrawRay(transform.position, fovLine2);
        }
    }
}
