using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyManager : CharacterManager
    {
        EnemyMovementManager enemyMovementManager;
        public bool isPerformingAction;

        [Header("A.I. Settings")]
        public float detectionRadius = 20f;
        public float maximumDetectionAngle = 50f;
        public float minimumDetectionAngle = -50f;

        private void Awake() {
            enemyMovementManager = GetComponent<EnemyMovementManager>();
        }

        private void Update() {

        }

        private void FixedUpdate() {
            HandleCurrentAction();
        }

        private void HandleCurrentAction() {
            if (enemyMovementManager.currentTarget == null) {
                enemyMovementManager.HandleDetection();
            }
            else {
                enemyMovementManager.HandleMoveToTarget();
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
