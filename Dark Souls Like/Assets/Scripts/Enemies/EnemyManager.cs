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
        public bool isInteracting;

        public float rotationSpeed = 300f;
        public float maximumAttackRange = 1.5f;

        [Header("Combat Flags")]
        public bool canDoCombo;

        [Header("A.I. Settings")]
        public float detectionRadius = 20f;
        public float maximumDetectionAngle = 50f;
        public float minimumDetectionAngle = -50f;
        public float currentRecoveryTime = 0f;

        [Header("A.I. Combat")]
        public bool allowAIToPerformCombo;
        public float comboLikelyhood;
        public bool isPhaseShifting;

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
            HandleStateMachine();

            isRotatingWithRootMotion = enemyAnimatorHandler.anim.GetBool("isRotatingWithRootMotion");
            isInteracting = enemyAnimatorHandler.anim.GetBool("isInteracting");
            isPhaseShifting = enemyAnimatorHandler.anim.GetBool("isPhaseShifting");
            isInvulnerable = enemyAnimatorHandler.anim.GetBool("isInvulnerable");
            canDoCombo = enemyAnimatorHandler.anim.GetBool("canDoCombo");
            canRotate = enemyAnimatorHandler.anim.GetBool("canRotate");
            enemyAnimatorHandler.anim.SetBool("isDead", enemyStats.isDead);
        }

        private void FixedUpdate() {
            transform.position = new Vector3(transform.position.x, navmeshAgent.transform.position.y, transform.position.z);
            navmeshAgent.nextPosition = transform.position;
            //navmeshAgent.transform.localPosition = Vector3.zero;
            navmeshAgent.transform.localRotation = Quaternion.identity;
        }

        private void HandleStateMachine() {
            if (enemyStats.isDead) {
                return;
            }

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
