using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class PlayerAttacker : MonoBehaviour
    {
        AnimatorHandler animatorHandler;
        InputHandler inputHandler;
        WeaponSlotManager weaponSlotManager;
        public string lastAttack;

        private void Awake() {
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            inputHandler = GetComponent<InputHandler>();
            weaponSlotManager = GetComponentInChildren<WeaponSlotManager>();
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
            if (inputHandler.comboFlag) {
                animatorHandler.anim.SetBool("canDoCombo", false);
                if (lastAttack == weapon.OH_Heavy_Attack_1) {
                    animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_2, true);
                }
            }
        }

        public void HandleLightAttack(WeaponItem weapon) {
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
            weaponSlotManager.attackingWeapon = weapon;
            animatorHandler.PlayTargetAnimation(weapon.OH_Heavy_Attack_1, true);
            lastAttack = weapon.OH_Heavy_Attack_1;
        }
    }
}
