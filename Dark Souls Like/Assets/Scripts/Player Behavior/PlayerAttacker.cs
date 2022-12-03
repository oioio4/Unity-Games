using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class PlayerAttacker : MonoBehaviour
    {
        CameraHandler cameraHandler;
        AnimatorHandler animatorHandler;
        PlayerEquipmentManager playerEquipmentManager;
        PlayerManager playerManager;
        PlayerStats playerStats;
        PlayerInventory playerInventory;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        LayerMask backStabLayer = 1 << 13;
        LayerMask riposteLayer = 1 << 15;

        private void Awake() {
            cameraHandler = FindObjectOfType<CameraHandler>();
            animatorHandler = GetComponent<AnimatorHandler>();
            playerEquipmentManager = GetComponent<PlayerEquipmentManager>();
            playerManager = GetComponentInParent<PlayerManager>();
            playerStats = GetComponentInParent<PlayerStats>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            inputHandler = GetComponentInParent<InputHandler>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        public void HandleLightWeaponCombo(WeaponItem weapon) {
            if (inputHandler.comboFlag) {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Light_Attack_1) {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_2, true);
                }
                else if (lastAttack == weapon.TH_Light_Attack_1) {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_2, true);
                }
            }
        }

        public void HandleHeavyWeaponCombo(WeaponItem weapon) {
            if (playerStats.currentStamina <= 0) {
                return;
            }

            if (inputHandler.comboFlag) {
                animatorHandler.anim.SetBool("canDoCombo", false);

                if (lastAttack == weapon.OH_Heavy_Attack_1) {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_2, true);
                }
                else if (lastAttack == weapon.TH_Heavy_Attack_1) {
                    animatorHandler.PlayTargetAnimation(weapon.TH_Heavy_Attack_2, true);
                }
            }
        }

        public void HandleLightAttack(WeaponItem weapon) {
            if (playerStats.currentStamina <= 0) {
                return;
            }

            weaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag) {
                animatorHandler.PlayTargetAnimation(weapon.TH_Light_Attack_1, true);
                lastAttack = weapon.TH_Light_Attack_1;
            }
            else {
                animatorHandler.PlayTargetAnimation(weapon.OH_Light_Attack_1, true);
                lastAttack = weapon.OH_Light_Attack_1;
            }
        }
        // implement two hand stuff
        public void HandleHeavyAttack(WeaponItem weapon) {
            if (playerStats.currentStamina <= 0) {
                return;
            }

            weaponSlotManager.attackingWeapon = weapon;

            if (inputHandler.twoHandFlag) {
                animatorHandler.PlayTargetAnimation(weapon.TH_Heavy_Attack_1, true);
                lastAttack = weapon.TH_Heavy_Attack_1;
            }
            else {
                animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
                lastAttack = weapon.OH_Heavy_Attack_1;
            }
        }

        #region Input Actions

        public void HandleRBAction() {
            if (playerInventory.rightWeapon.isMeleeWeapon) {
                PerformRBMeleeActon();
            }
            else if (playerInventory.rightWeapon.isSpellCaster ||
                playerInventory.rightWeapon.isFaithCaster ||
                playerInventory.rightWeapon.isPyroCaster) {
                PerformRBMagicAction(playerInventory.rightWeapon);
            }
        }

        public void HandleRTAction() {
            if (playerInventory.rightWeapon.isMeleeWeapon) {
                PerformRTMeleeActon();
            }
        }

        public void HandleLBAction() {
            PerformLBBlockingAction();
        }

        public void HandleLTAction() {
            if (playerInventory.leftWeapon.isShieldWeapon) {
                PerformLTWeaponArt(inputHandler.twoHandFlag);
            }
            else if (playerInventory.leftWeapon.isMeleeWeapon) {

            }
        }

        #endregion

        #region Attack Actions

        private void PerformRBMeleeActon() {
            if (playerManager.canDoCombo) {
                inputHandler.comboFlag = true;
                HandleLightWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else {
                if (playerManager.isInteracting) {
                    return;
                }
                if (playerManager.canDoCombo) {
                    return;
                }

                animatorHandler.anim.SetBool("isUsingRightHand", true);
                HandleLightAttack(playerInventory.rightWeapon);
            }
        }

        private void PerformRTMeleeActon() {
            if (playerManager.canDoCombo) {
                inputHandler.comboFlag = true;
                HandleHeavyWeaponCombo(playerInventory.rightWeapon);
                inputHandler.comboFlag = false;
            }
            else {
                if (playerManager.isInteracting) {
                    return;
                }
                if (playerManager.canDoCombo) {
                    return;
                }

                animatorHandler.anim.SetBool("isUsingRightHand", true);
                HandleHeavyAttack(playerInventory.rightWeapon);
            }
        }

        private void PerformRBMagicAction(WeaponItem weapon) {
            if (playerManager.isInteracting) {
                return;
            }

            if (weapon.isFaithCaster) {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isFaithSpell) {
                    if (playerStats.currentFocusPoints >= playerInventory.currentSpell.focusPointCost) {
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
                    }
                }
            }
            else if (weapon.isPyroCaster) {
                if (playerInventory.currentSpell != null && playerInventory.currentSpell.isPyroSpell) {
                    if (playerStats.currentFocusPoints >= playerInventory.currentSpell.focusPointCost) {
                        playerInventory.currentSpell.AttemptToCastSpell(animatorHandler, playerStats, weaponSlotManager);
                    }
                }
            }
        }

        private void PerformLTWeaponArt(bool isTwoHanding) {
            if (playerManager.isInteracting) {
                return;
            }

            if (isTwoHanding) {
            }   
            else {
                animatorHandler.PlayTargetAnimation(playerInventory.leftWeapon.weapon_art, true);
            }
        }

        private void SuccessfullyCastSpell() {
            playerInventory.currentSpell.SuccessfullyCastSpell(animatorHandler, playerStats, cameraHandler, weaponSlotManager);
            animatorHandler.anim.SetBool("isFiringSpell", true);
        }

        #endregion

        #region Defense Actions
        private void PerformLBBlockingAction() {
            if (playerManager.isInteracting) {
                return;
            }
            if (playerManager.isBlocking) {
                return;
            }

            animatorHandler.PlayTargetAnimation("Block Start", false, true);
            playerEquipmentManager.OpenBlockingCollider();
            playerManager.isBlocking = true;
        }

        #endregion

        public void AttemptBackStabOrRiposte(){
            if (playerStats.currentStamina <= 0) {
                return;
            }
            
            RaycastHit hit;

            if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, 
            transform.TransformDirection(Vector3.forward), out hit, 0.5f, backStabLayer)) {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null) {
                    playerManager.transform.position = enemyCharacterManager.backStabCollider.criticalDamagerStandPosition.position;
                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.curDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    animatorHandler.PlayTargetAnimation("BackStab", true);
                    AnimatorManager enemyAnimatorManager = enemyCharacterManager.GetComponentInChildren<AnimatorManager>();
                    StartCoroutine(HandleBackStab(enemyAnimatorManager));
                }
            }
            else if (Physics.Raycast(inputHandler.criticalAttackRayCastStartPoint.position, 
            transform.TransformDirection(Vector3.forward), out hit, 0.7f, riposteLayer)) {
                CharacterManager enemyCharacterManager = hit.transform.gameObject.GetComponentInParent<CharacterManager>();
                DamageCollider rightWeapon = weaponSlotManager.rightHandDamageCollider;

                if (enemyCharacterManager != null && enemyCharacterManager.canBeRiposted) {
                    playerManager.transform.position = enemyCharacterManager.riposteCollider.criticalDamagerStandPosition.position;

                    Vector3 rotationDirection = playerManager.transform.root.eulerAngles;
                    rotationDirection = hit.transform.position - playerManager.transform.position;
                    rotationDirection.y = 0;
                    rotationDirection.Normalize();
                    Quaternion tr = Quaternion.LookRotation(rotationDirection);
                    Quaternion targetRotation = Quaternion.Slerp(playerManager.transform.rotation, tr, 500 * Time.deltaTime);
                    playerManager.transform.rotation = targetRotation;

                    int criticalDamage = playerInventory.rightWeapon.criticalDamageMultiplier * rightWeapon.curDamage;
                    enemyCharacterManager.pendingCriticalDamage = criticalDamage;

                    animatorHandler.PlayTargetAnimation("Riposte", true);
                    enemyCharacterManager.GetComponentInChildren<AnimatorManager>().PlayTargetAnimation("Riposted", true);
                }
            }
        }

        private IEnumerator HandleBackStab(AnimatorManager enemyAnimatorManager) {
            enemyAnimatorManager.PlayTargetAnimation("BackStabbed", true);
            yield return new WaitForSeconds(3);
            if (!enemyAnimatorManager.anim.GetBool("isDead")) {
                enemyAnimatorManager.PlayTargetAnimation("BackStabbedAlive", true);
            }
        }
    }
}
