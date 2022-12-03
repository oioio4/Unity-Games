using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class PlayerEffectsManager : MonoBehaviour
    {
        PlayerStats playerStats;
        WeaponSlotManager weaponSlotManager;
        public GameObject currentParticleFX;
        public GameObject instantiatedFXModel;
        public int amountToHeal;

        private void Awake() {
            playerStats = GetComponentInParent<PlayerStats>();
            weaponSlotManager = GetComponent<WeaponSlotManager>();
        }

        public void HealPlayerFromEffect() {
            playerStats.HealPlayer(amountToHeal);
            GameObject healParticles = Instantiate(currentParticleFX, playerStats.transform);
            Destroy(instantiatedFXModel.gameObject);
            weaponSlotManager.LoadBothWeaponsOnSlots();
        }
    }
}
