using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LightInteractable : Interactable
{
    public UnityEvent lightsOffAction;
    private bool lightsOn = false;

    public override void InteractionEvent() {
        if (!lightsOn) {
            interactAction.Invoke();
        } else {
            lightsOffAction.Invoke();
        }

        lightsOn = !lightsOn;
    }
}
