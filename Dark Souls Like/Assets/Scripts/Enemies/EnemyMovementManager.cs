using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyMovementManager : MonoBehaviour
    {
        EnemyManager enemyManager;
        EnemyAnimatorHandler enemyAnimatorHandler;

        private void Awake() {
            enemyManager = GetComponent<EnemyManager>();
            enemyAnimatorHandler = GetComponentInChildren<EnemyAnimatorHandler>();
        }

        private void Start() {
        }
    }
}
