using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;
        public RotateTowardsTargetState rotateTowardsTargetState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler) {
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - enemyManager.transform.position;
            float distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, enemyManager.transform.position);
            float viewableAngle = Vector3.SignedAngle(targetDirection, enemyManager.transform.forward, Vector3.up);

            HandleRotateTowardsTarget(enemyManager);

            if (enemyManager.isInteracting) {
                return this;
            }

            if (enemyManager.isPerformingAction) {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                return this;
            }

            if (distanceFromTarget > enemyManager.maximumAttackRange) {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }

            if (distanceFromTarget <= enemyManager.maximumAttackRange) {
                return combatStanceState;
            }
            else {
                return this;
            }
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
    }
}
