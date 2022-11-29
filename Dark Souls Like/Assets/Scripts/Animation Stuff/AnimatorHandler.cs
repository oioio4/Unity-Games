using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC {
    public class AnimatorHandler : AnimatorManager
    {
        PlayerManager playerManager;
        PlayerStats playerStats;
        InputHandler inputHandler;
        PlayerMovement playerMovement;
        int vertical;
        int horizontal;

        public void Initialize() {
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            anim = GetComponent<Animator>();
            inputHandler = GetComponentInParent<InputHandler>();
            playerMovement = GetComponentInParent<PlayerMovement>();
            vertical = Animator.StringToHash("Vertical");
            horizontal = Animator.StringToHash("Horizontal");
        }

        public void UpdateAnimatorValues(float verticalMovement, float horizontalMovement, bool isSprinting) {
            #region Vertical
            float v = 0;

            if (verticalMovement > 0 && verticalMovement < 0.55f) {
                v = 0.5f;
            }
            else if (verticalMovement > 0.55f) {
                v = 1;
            }
            else if (verticalMovement < 0 && verticalMovement > -0.55f) {
                v = -0.5f;
            }
            else if (verticalMovement < -0.55f) {
                v = -1f;
            }
            else {
                v = 0;
            }
            #endregion

            #region Horizontal
            float h = 0;

            if (horizontalMovement > 0 && horizontalMovement < 0.55f) {
                h = 0.5f;
            }
            else if (horizontalMovement > 0.55f) {
                h = 1;
            }
            else if (horizontalMovement < 0 && horizontalMovement > -0.55f) {
                h = -0.5f;
            }
            else if (horizontalMovement < -0.55f) {
                h = -1f;
            }
            else {
                h = 0;
            }
            #endregion

            if (isSprinting) {
                v = 2;
                h = horizontalMovement;
            }

            anim.SetFloat(vertical, v, 0.1f, Time.deltaTime);
            anim.SetFloat(horizontal, h, 0.1f, Time.deltaTime);
        }

        public void EnableCombo() {
            anim.SetBool("canDoCombo", true);
        }

        public void DisableCombo() {
            anim.SetBool("canDoCombo", false);
        }

        public void EnableIsInvulnerable() {
            anim.SetBool("isInvulnerable", true);
        }

        public void DisableIsInvulnerable() {
            anim.SetBool("isInvulnerable", false);
        }

        public override void TakeCriticalDamage() {
            playerStats.TakeDamageNoAnimation(playerManager.pendingCriticalDamage);
            playerManager.pendingCriticalDamage = 0;
        }

        // just so no error on player death
        public void AwardSoulsOnDeath() {
            if (playerStats != null) {
                playerStats.AddSouls(0);
            }
        }

        private void OnAnimatorMove() {
            if (playerManager.isInteracting == false) {
                return;
            }

            float delta = Time.deltaTime;
            playerMovement.rigidbody.drag = 0;
            Vector3 deltaPosition = anim.deltaPosition;
            deltaPosition.y = 0;
            Vector3 velocity = deltaPosition / delta;
            playerMovement.rigidbody.velocity = velocity;
        }
    }

}
