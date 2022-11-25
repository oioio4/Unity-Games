using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace NC
{
    public class EnemyManager : CharacterManager
    {
        EnemyMovementManager enemyMovementManager;
        EnemyAnimatorHandler enemyAnimatorHandler;
        EnemyStats enemyStats;

        public State currentState;
        public CharacterStats currentTarget;
        public NavMeshAgent navmeshAgent;
        public Rigidbody enemyRigidbody;

        public bool isPerformingAction;

        public float distanceFromTarget;
        public float rotationSpeed = 25f;
        public float maximumAttackRange = 1.5f;

        [Header("A.I. Settings")]
        public float detectionRadius = 20f;
        public float maximumDetectionAngle = 50f;
        public float minimumDetectionAngle = -50f;
        public float viewableAngle;

        public float currentRecoveryTime = 0f;

        private void Awake() {
            enemyMovementManager = GetComponent<EnemyMovementManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
            enemyStats = GetComponent<EnemyStats>();
            navmeshAgent = GetComponentInChildren<NavMeshAgent>();
            enemyRigidbody = GetComponent<Rigidbody>();
        }

        private void Start() {
            navmeshAgent.enabled = false;
            enemyRigidbody.isKinematic = false;
        }

        private void Update() {
            HandleRecoveryTimer();
        }

        private void FixedUpdate() {
            HandleStateMachine();
        }

        private void HandleStateMachine() {
            if (currentState != null) {
                State nextState = currentState.Tick(this, enemyStats, enemyAnimatorHandler);

                if (nextState != null) {
                    SwitchToNextState(nextState);
                }
            }
        }

        private void SwitchToNextState(State state) {
            currentState = state;
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
