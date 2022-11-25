using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EnemyAnimatorHandler : AnimatorManager
    {
        EnemyMovementManager enemyMovementManager;

        private void Awake() {
            anim = GetComponent<Animator>();
            enemyMovementManager= GetComponentInParent<EnemyMovementManager>();
        }

        private void OnAnimatorMove() {
            float delta = Time.deltaTime;
            enemyMovementManager.enemyRigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            enemyMovementManager.enemyRigidbody.velocity = velocity;
        }
    }
}
