using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class PlayerEquipmentManager : MonoBehaviour
    {
        InputHandler inputHandler;
        PlayerInventory playerInventory;
        PlayerStats playerStats;
        public BlockingCollider blockingCollider;

        [Header("Equipment Model Changers")]
        HelmetModelChanger helmetModelChanger;
        CapeModelChanger capeModelChanger;
        TorsoModelChanger torsoModelChanger;
        HandModelChanger handModelChanger;
        LegModelChanger legModelChanger;
        FootModelChanger footModelChanger;


        [Header("Default Naked Models")]
        public string nakedHeadModel;
        public string nakedTorsoModel;
        public string nakedHandModel;
        public string nakedLegModel;
        public string nakedFootModel;

        private void Awake() {
            inputHandler = GetComponentInParent<InputHandler>();
            playerInventory = GetComponentInParent<PlayerInventory>();
            playerStats = GetComponentInParent<PlayerStats>();
            helmetModelChanger = GetComponentInChildren<HelmetModelChanger>();
            capeModelChanger = GetComponentInChildren<CapeModelChanger>();
            torsoModelChanger = GetComponentInChildren<TorsoModelChanger>();
            handModelChanger = GetComponentInChildren<HandModelChanger>();
            legModelChanger = GetComponentInChildren<LegModelChanger>();
            footModelChanger = GetComponentInChildren<FootModelChanger>();
        }

        private void Start() {
            EquipAllEquipment();
        }

        private void EquipAllEquipment() {
            //
            helmetModelChanger.UnequipAllHelmetModels();

            if (playerInventory.currentHelmet != null) {
                helmetModelChanger.EquipHelmetModel(playerInventory.currentHelmet.helmetModelName);
                playerStats.physicalDamageAbsorptionHead = playerInventory.currentHelmet.physicalDefense;
            }
            else {
                helmetModelChanger.EquipHelmetModel(nakedHeadModel);
            }

            //
            capeModelChanger.UnequipAllCapeModels();

            if (playerInventory.currentCape != null) {
                capeModelChanger.EquipCapeModel(playerInventory.currentCape.capeModelName);
                playerStats.physicalDamageAbsorptionCape = playerInventory.currentCape.physicalDefense;
            }

            //
            torsoModelChanger.UnequipAllTorsoModels();

            if (playerInventory.currentTorso != null) {
                torsoModelChanger.EquipTorsoModel(playerInventory.currentTorso.torsoModelName);
                playerStats.physicalDamageAbsorptionBody = playerInventory.currentTorso.physicalDefense;
            }
            else {
                torsoModelChanger.EquipTorsoModel(nakedTorsoModel);
            }

            //
            handModelChanger.UnequipAllHandModels();

            if (playerInventory.currentHand != null) {
                handModelChanger.EquipHandModel(playerInventory.currentHand.handModelName);
                playerStats.physicalDamageAbsorptionHands = playerInventory.currentHand.physicalDefense;
            }
            else {
                handModelChanger.EquipHandModel(nakedHandModel);
            }

            //
            legModelChanger.UnequipAllLegModels();

            if (playerInventory.currentLeg != null) {
                legModelChanger.EquipLegModel(playerInventory.currentLeg.legModelName);
                playerStats.physicalDamageAbsorptionLegs = playerInventory.currentLeg.physicalDefense;
            }
            else {
                legModelChanger.EquipLegModel(nakedLegModel);
            }

            //
            footModelChanger.UnequipAllFootModels();

            if (playerInventory.currentFoot != null) {
                footModelChanger.EquipFootModel(playerInventory.currentFoot.footModelName);
                playerStats.physicalDamageAbsorptionFeet = playerInventory.currentFoot.physicalDefense;
            }
            else {
                footModelChanger.EquipFootModel(nakedFootModel);
            }
        }

        public void OpenBlockingCollider() {
            if (inputHandler.twoHandFlag) {
                blockingCollider.SetColliderDamageAbsorption(playerInventory.rightWeapon);
            }
            else {
                blockingCollider.SetColliderDamageAbsorption(playerInventory.leftWeapon);
            }

            blockingCollider.EnableBlockingCollider();
        }

        public void CloseBlockingCollider() {
            blockingCollider.DisableBlockingCollider();
        }
    }
}
