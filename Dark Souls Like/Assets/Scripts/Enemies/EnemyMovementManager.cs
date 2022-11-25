using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NC
{
    public class EnemyMovementManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        NavMeshAgent navmeshAgent;
        public Rigidbody enemyRigidbody;

        public CharacterStats currentTarget;
        public LayerMask detectionLayer;

        public float distanceFromTarget;
        public float stoppingDistance = 1f;

        public float rotationSpeed = 25f;

        private void Awake() {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start() {
            navmeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }

        public void HandleDetection() {
            Collider[] colliders = Physics.OverlapSphere(transform.position, enemyManager.detectionRadius, detectionLayer);

            for (int i = 0; i < colliders.Length; i++) {
                CharacterStats characterStats = colliders[i].transform.GetComponent<CharacterStats>();

                if (characterStats != null) {
                    Vector3 targetDirection = characterStats.transform.position - transform.position;
                    float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

                    if (viewableAngle > enemyManager.minimumDetectionAngle && viewableAngle < enemyManager.maximumDetectionAngle) {
                        currentTarget = characterStats;
                    }
                }
            }
        }

        public void HandleMoveToTarget() {
            if (enemyManager.isPerformingAction) {
                return;
            }
            
            Vector3 targetDirection = currentTarget.transform.position - transform.position;
            distanceFromTarget = Vector3.Distance(currentTarget.transform.position, transform.position);
            float viewableAngle = Vector3.Angle(targetDirection, transform.forward);

            if (enemyManager.isPerformingAction) {
                enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                navmeshAgent.enabled = false;
            }
            else {
                if (distanceFromTarget > stoppingDistance) {
                    enemyAnimatorHandler.anim.SetFloat("Vertical", 1, 0.1f, Time.deltaTime);
                }
                else if (distanceFromTarget <= stoppingDistance) {
                    enemyAnimatorHandler.anim.SetFloat("Vertical", 0, 0.1f, Time.deltaTime);
                }
            }

            HandleRotateTowardsTarget();
            transform.position = new Vector3(transform.position.x, navmeshAgent.transform.position.y, transform.position.z);
            navmeshAgent.nextPosition = transform.position;
            //navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleRotateTowardsTarget() {
            if (enemyManager.isPerformingAction) {
                Vector3 direction = currentTarget.transform.position - transform.position;
                direction.y = 0;
                direction.Normalize();

                if (direction == Vector3.zero) {
                    direction = transform.forward;
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed / Time.deltaTime);
            }
            else {
                Vector3 relativeDirection = transform.InverseTransformDirection(navmeshAgent.desiredVelocity);
                Vector3 targetVelocity = enemyRigidbody.velocity;

                navmeshAgent.enabled = true;
                navmeshAgent.SetDestination(currentTarget.transform.position);
                enemyRigidbody.velocity = targetVelocity;
                transform.rotation = Quaternion.Slerp(transform.rotation, navmeshAgent.transform.rotation, rotationSpeed / Time.deltaTime);
            }
        } 
    }
}
