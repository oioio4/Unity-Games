using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireplaceInteractable : Interactable
{
    public override void InteractionEvent() {
        GameObject item = inventory.curSelected;
        if (item != null) {
            if (item.name.Substring(0, item.name.Length - 7) == "log") {
                // gets rid of the item from the inventory
                Destroy(item);
                inventory.curSelected = null;
                inventory.isFull[inventory.curSlotIndex] = false;
                inventory.DeselectAll();

                gm.logFlag = true;
                interactAction.Invoke();
            }
        }
    }
}
