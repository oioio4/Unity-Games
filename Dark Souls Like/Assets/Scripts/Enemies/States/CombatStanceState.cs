using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class CombatStanceState : State
    {
        public AttackState attackState;
        public EnemyAttackAction[] enemyAttacks;
        public PursueTargetState pursueTargetState;

        private bool randomDestinationSet = false;
        private float verticalMovementValue = 0;
        private float horizontalMovementValue = 0;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler) {
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            enemyAnimatorHandler.anim.SetFloat("Vertical", verticalMovementValue, 0.2f, Time.deltaTime);
            enemyAnimatorHandler.anim.SetFloat("Horizontal", horizontalMovementValue, 0.2f, Time.deltaTime);
            attackState.hasPerformedAttack = false;

            if (enemyManager.isInteracting) {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 0);
                enemyAnimatorHandler.anim.SetFloat("Horizontal", 0);
                return this;
            }

            if (distanceFromTarget > enemyManager.maximumAttackRange) {
                return pursueTargetState;
            }

            if (!randomDestinationSet) {
                randomDestinationSet = true;
                DecideCirclingAction(enemyAnimatorHandler);
            }

            HandleRotateTowardsTarget(enemyManager);

            if (enemyManager.currentRecoveryTime <= 0 && attackState.currentAttack != null) {
                randomDestinationSet = false;
                return attackState;
            }
            else {
                GetNewAttack(enemyManager);
            }

            return this;
        }

        private void HandleRotateTowardsTarget(EnemyManager enemyManager) {
            if (enemyManager.isPerformingAction) {
                Vector3 direction = enemyManager.currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero) {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed * Time.deltaTime);
            }
            else {
                enemyManager.navmeshAgent.enabled = true;
                enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

                float rotationToApplyToDynamicEnemy = Quaternion.Angle(enemyManager.transform.rotation, Quaternion.LookRotation(enemyManager.navmeshAgent.desiredVelocity.normalized));
                if (distanceFromTarget > 5) enemyManager.navmeshAgent.angularSpeed = 500f;
                else if (distanceFromTarget < 5 && Mathf.Abs(rotationToApplyToDynamicEnemy) < 30) enemyManager.navmeshAgent.angularSpeed = 50f;
                else if (distanceFromTarget < 5 && Mathf.Abs(rotationToApplyToDynamicEnemy) > 30) enemyManager.navmeshAgent.angularSpeed = 500f;

                Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
                Quaternion rotationToApplyToStaticEnemy = Quaternion.LookRotation(targetDirection);


                if (enemyManager.navmeshAgent.desiredVelocity.magnitude > 0)
                {
                    enemyManager.navmeshAgent.updateRotation = false;
                    enemyManager.transform.rotation = Quaternion.RotateTowards(enemyManager.transform.rotation,
                    Quaternion.LookRotation(enemyManager.navmeshAgent.desiredVelocity.normalized), enemyManager.navmeshAgent.angularSpeed * Time.deltaTime);
                }
                else
                {
                    enemyManager.transform.rotation = Quaternion.RotateTowards(enemyManager.transform.rotation, rotationToApplyToStaticEnemy, enemyManager.navmeshAgent.angularSpeed * Time.deltaTime);
                /*
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

                enemyManager.navmeshAgent.enabled = true;
                enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidbody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(enemyManager.transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed * Time.deltaTime);
           */ 
                }
            }
        }

        private void DecideCirclingAction(EnemyAnimatorHandler enemyAnimatorHandler) {
            WalkAroundTarget(enemyAnimatorHandler);
        }

        private void WalkAroundTarget(EnemyAnimatorHandler enemyAnimatorHandler) {
            verticalMovementValue = 0.5f;

            horizontalMovementValue = Random.Range(-1, 1);

            if (horizontalMovementValue <= 1 && horizontalMovementValue >= 0) {
                horizontalMovementValue = 1f;
            }
            else if (horizontalMovementValue >= -1 && horizontalMovementValue < 0) {
                horizontalMovementValue = -1f;
            }
        }

        private void GetNewAttack(EnemyManager enemyManager) {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float viewableAngle = Vector3.Angle(targetDirection, enemyManager.transform.forward);
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);

            int maxScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++) {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget  <= enemyAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget  >= enemyAttackAction.minimumDistanceNeededToAttack) {
                        if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                            && viewableAngle >= enemyAttackAction.minimumAttackAngle) {
                                if (enemyAttackAction.attackScore > maxScore) {
                                    maxScore = enemyAttackAction.attackScore;
                                }
                                //maxScore += enemyAttackAction.attackScore;
                            }
                }
            }

            int randomValue = Random.Range(0, maxScore);
            int temporaryScore = 0;

            for (int i = 0; i < enemyAttacks.Length; i++) {
                EnemyAttackAction enemyAttackAction = enemyAttacks[i];

                if (distanceFromTarget  <= enemyAttackAction.maximumDistanceNeededToAttack
                    && distanceFromTarget  >= enemyAttackAction.minimumDistanceNeededToAttack) {
                    if (viewableAngle <= enemyAttackAction.maximumAttackAngle
                        && viewableAngle >= enemyAttackAction.minimumAttackAngle) {
                        if (attackState.currentAttack != null) {
                            return;
                        }

                        temporaryScore = enemyAttackAction.attackScore;
                        //temporaryScore += enemyAttackAction.attackScore;

                        if (temporaryScore >= randomValue) {
                            attackState.currentAttack = enemyAttackAction;
                        }
                    }
                }
            }
        }
    }
}
