using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class WeaponSlotManager : MonoBehaviour
    {
        PlayerManager playerManager;
        PlayerInventory playerInventory;
        public WeaponItem attackingWeapon;

        WeaponHolderSlot leftHandSlot;
        WeaponHolderSlot rightHandSlot;
        WeaponHolderSlot backSlot;

        public DamageCollider leftHandDamageCollider;
        public DamageCollider rightHandDamageCollider;

        Animator animator;

        QuickSlotsUI quickSlotsUI;

        PlayerStats playerStats;
        InputHandler inputHandler;

        private void Awake() {
            playerManager = GetComponentInParent<PlayerManager>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            animator = GetComponent<Animator>();
            quickSlotsUI = FindObjectOfType<QuickSlotsUI>();
            playerStats = GetComponentInParent<PlayerStats>();
            inputHandler = GetComponentInParent<InputHandler>();

            WeaponHolderSlot[] weaponHolderSlots = GetComponentsInChildren<WeaponHolderSlot>();
            foreach (WeaponHolderSlot weaponSlot in weaponHolderSlots) {
                if (weaponSlot.isLeftHandSlot) {
                    leftHandSlot = weaponSlot;
                }
                else if (weaponSlot.isRightHandSlot) {
                    rightHandSlot = weaponSlot;
                }
                else if (weaponSlot.isBackSlot) {
                    backSlot = weaponSlot;
                }
            }
        }

        public void LoadWeaponOnSlot(WeaponItem weaponItem, bool isLeft) {
            if (isLeft) {
                leftHandSlot.currentWeapon = weaponItem;
                leftHandSlot.LoadWeaponModel(weaponItem);
                LoadLeftWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(true, weaponItem);

                if (weaponItem != null) {
                    animator.CrossFade(weaponItem.left_hand_idle, 0.2f);
                }
                else {
                    animator.CrossFade("Left Arm Empty", 0.2f);
                }
            }
            else {
                animator.CrossFade("Both Arms Empty", 0.2f);
                
                backSlot.UnloadWeaponAndDestroy();
                
                if (inputHandler.twoHandFlag) {
                    backSlot.LoadWeaponModel(leftHandSlot.currentWeapon);
                    leftHandSlot.UnloadWeaponAndDestroy();
                    animator.CrossFade(weaponItem.th_idle, 0.2f);
                }
                else {
                    if (weaponItem != null) {
                        animator.CrossFade(weaponItem.right_hand_idle, 0.2f);
                    }
                    else {
                        animator.CrossFade("Right Arm Empty", 0.2f);
                    }
                }

                rightHandSlot.currentWeapon = weaponItem;
                rightHandSlot.LoadWeaponModel(weaponItem);
                LoadRightWeaponDamageCollider();
                quickSlotsUI.UpdateWeaponQuickSlotsUI(false, weaponItem);
            }
        }

        #region Handle Weapon Collider
        private void LoadLeftWeaponDamageCollider() {
            leftHandDamageCollider = leftHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            leftHandDamageCollider.curDamage = playerInventory.leftWeapon.baseDamage;
            leftHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }

        private void LoadRightWeaponDamageCollider() {
            rightHandDamageCollider = rightHandSlot.currentWeaponModel.GetComponentInChildren<DamageCollider>();
            rightHandDamageCollider.curDamage = playerInventory.rightWeapon.baseDamage;
            rightHandDamageCollider.characterManager = GetComponentInParent<CharacterManager>();
        }

        public void OpenDamageCollider() {
            if (playerManager.isUsingRightHand) {
                rightHandDamageCollider.EnableDamageCollider();
            }
            else if (playerManager.isUsingLeftHand) {
                leftHandDamageCollider.EnableDamageCollider();
            }
        }

        public void CloseDamageCollider() {
            if (playerManager.isUsingRightHand) {
                rightHandDamageCollider.DisableDamageCollider();
            }
            else if (playerManager.isUsingLeftHand) {
                leftHandDamageCollider.DisableDamageCollider();
            }
        }

        #endregion
        
        #region Handle Stamina Drain
        public void DrainStaminaLightAttack() {
            playerStats.DrainStamina(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.lightAttackMultiplier));
        }

        public void DrainStaminaHeavyAttack() {
            playerStats.DrainStamina(Mathf.RoundToInt(attackingWeapon.baseStamina * attackingWeapon.heavyAttackMultiplier));
        }
        #endregion
    }
}
