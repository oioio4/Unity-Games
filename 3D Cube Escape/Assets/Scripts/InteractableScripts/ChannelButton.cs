using UnityEngine;

// A channel button on the TV.
public class ChannelButton : Interactable
{
    [SerializeField] private ChannelTV tv;
    [SerializeField] private int channel = 1;

    public override void InteractionEvent() {
        tv.Press(channel);
    }
}
