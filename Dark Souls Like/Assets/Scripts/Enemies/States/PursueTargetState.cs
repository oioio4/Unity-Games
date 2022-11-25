using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class PursueTargetState : State
    {
        public CombatStanceState combatStanceState;

        public override State Tick(EnemyManager enemyManager, EnemyStats enemyStats, EnemyAnimatorHandler enemyAnimatorHandler) {
            if (enemyManager.isPerformingAction) {
                return this;
            }
            
            Vector3 targetDirection = enemyManager.currentTarget.transform.position - transform.position;
            enemyManager.distanceFromTarget = Vector3.Distance(enemyManager.currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.distanceFromTarget > enemyManager.maximumAttackRange) {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
            }

            HandleRotateTowardsTarget(enemyManager);
            transform.position = new Vector3(transform.position.x, enemyManager.navmeshAgent.transform.position.y, transform.position.z);
            enemyManager.navmeshAgent.nextPosition = transform.position;
            //navmeshAgent.transform.localPosition = Vector3.zero;
            enemyManager.navmeshAgent.transform.localRotation = Quaternion.identity;

            if (enemyManager.distanceFromTarget <= enemyManager.maximumAttackRange) {
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
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
            else {
                Vector3 relativeDirection = transform.InverseTransformDirection(enemyManager.navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyManager.enemyRigidbody.velocity;

                enemyManager.navmeshAgent.enabled = true;
                enemyManager.navmeshAgent.SetDestination(enemyManager.currentTarget.transform.position);
                enemyManager.enemyRigidbody.velocity = targetVelocity;
                enemyManager.transform.rotation = Quaternion.Slerp(transform.rotation, enemyManager.navmeshAgent.transform.rotation, enemyManager.rotationSpeed / Time.deltaTime);
            }
        }
    }
}
