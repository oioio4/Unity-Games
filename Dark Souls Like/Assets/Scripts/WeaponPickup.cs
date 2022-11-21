using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace NC
{
    public class WeaponPickup : Interactable
    {
        public WeaponItem weapon;

        public override void Interact(PlayerManager playerManager) {
            base.Interact(playerManager);

            PickUpItem(playerManager);
        }

        private void PickUpItem(PlayerManager playerManager) {
            PlayerInventory playerInventory;
            PlayerMovement playerMovement;
            AnimatorHandler animatorHandler;

            playerInventory = playerManager.GetComponent<PlayerInventory>();
            playerMovement = playerManager.GetComponent<PlayerMovement>();
            animatorHandler = playerManager.GetComponentInChildren<AnimatorHandler>();

            playerMovement.rigidbody.velocity = Vector3.zero;
            animatorHandler.PlayTargetAnimation("Pick Up Item", true); 
            playerInventory.weaponsInventory.Add(weapon);
            playerManager.itemInteractableGameObject.GetComponentInChildren<Text>().text = weapon.itemName;
            playerManager.itemInteractableGameObject.GetComponentInChildren<RawImage>().texture = weapon.itemIcon.texture;
            playerManager.itemInteractableGameObject.SetActive(true);
            Destroy(gameObject);
        }
    }
}
