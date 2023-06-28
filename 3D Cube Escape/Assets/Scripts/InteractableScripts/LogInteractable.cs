using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogInteractable : Interactable
{
    public override void InteractionEvent() {
        GameObject item = inventory.curSelected;
        if (item != null) {
            if (!gm.logFireFlag && item.name.Substring(0, item.name.Length - 7) == "matches") {
                // lights the log in the fireplace on fire
                gm.logFireFlag = true;
                interactAction.Invoke();
                active = false;
            }
        }
    }
}
