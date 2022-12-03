using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    [CreateAssetMenu(menuName = "Items/Consumables/Flask")]
    public class FlaskItem : ConsumableItem
    {
        [Header("Flask Type")]
        public bool estusFlask;
        public bool ashenFlask;

        [Header("Recovery Amount")]
        public int healthRecoveryAmount;
        public int focusPointAmount;

        [Header("Recovery FX")]
        public GameObject recoveryFX;

        public override void AttemptToConsumeItem(AnimatorHandler animatorHandler, WeaponSlotManager weaponSlotManager, PlayerEffectsManager playerEffectsManager) {
            base.AttemptToConsumeItem(animatorHandler, weaponSlotManager, playerEffectsManager);
            GameObject flask = Instantiate(itemModel, weaponSlotManager.rightHandSlot.transform);
            playerEffectsManager.currentParticleFX = recoveryFX;
            playerEffectsManager.amountToHeal = healthRecoveryAmount;
            playerEffectsManager.instantiatedFXModel = flask;
            weaponSlotManager.rightHandSlot.UnloadWeapon();
        }
    }
}
