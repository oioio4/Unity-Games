using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC
{
    public class EquipmentWindowUI : MonoBehaviour
    {
        public bool rightHandSlot01Selected;
        public bool rightHandSlot02Selected;
        public bool leftHandSlot01Selected;
        public bool leftHandSlot02Selected;

        EquipmentSlotUI[] equipmentSlotUI;

        private void Start() {
            equipmentSlotUI = GetComponentsInChildren<EquipmentSlotUI>();
        }

        public void LoadWeaponsOnEquipmentScreen(PlayerInventory playerInventory) {
            for (int i = 0; i < equipmentSlotUI.Length; i++) {
                if (equipmentSlotUI[i].rightHandSlot01) {
                    equipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[0]);
                }
                else if (equipmentSlotUI[i].rightHandSlot02) {
                    equipmentSlotUI[i].AddItem(playerInventory.weaponsInRightHandSlots[1]);
                }
                else if (equipmentSlotUI[i].leftHandSlot01) {
                    equipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[0]);
                }
                else if (equipmentSlotUI[i].leftHandSlot02) {
                    equipmentSlotUI[i].AddItem(playerInventory.weaponsInLeftHandSlots[1]);
                }
            }
        }

        public void SelectedRightHandSlot01() {
            rightHandSlot01Selected = true;
        }

        public void SelectedRightHandSlot02() {
            rightHandSlot02Selected = true;
        }

        public void SelectedLeftHandSlot01() {
            leftHandSlot01Selected = true;
        }

        public void SelectedLeftHandSlot02() {
            leftHandSlot02Selected = true;
        }
    }
}
