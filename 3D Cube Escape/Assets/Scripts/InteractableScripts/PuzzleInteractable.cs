using System;
using UnityEngine;
using UnityEngine.Events;

// A puzzle object worked through in steps, in order. The stove, for example: put the pot
// of water on it, light it with the matches, drop the egg in, then fish the egg out with
// the spoon. A step can need an item selected in the hotbar and earlier steps (flags) done.
public class PuzzleInteractable : Interactable
{
    [Serializable]
    public class Step
    {
        [Tooltip("Item to have selected in the hotbar, by its icon's name in lower case (\"matches\"). Blank: just click.")]
        public string item;
        [Tooltip("Use the item up.")]
        public bool consumeItem = true;
        [Tooltip("Flags that must already be set, e.g. \"gasOn\".")]
        public string[] needs = new string[0];
        [Tooltip("Clicks it takes, e.g. six taps of the spoon to crack the egg.")]
        public int clicks = 1;
        [Tooltip("Flag to set once done.")]
        public string setFlag;
        [Tooltip("Inventory icon to hand over once done. Takes the used item's slot if it was used up.")]
        public GameObject giveItem;
        [Tooltip("AudioManager sound for each click that works.")]
        public string sound;
        [TextArea] public string wrongItemHint;  // clicked without the right item
        [TextArea] public string notYetHint;     // something else has to happen first
        [TextArea] public string clickHint;      // after each click short of the last
        [TextArea] public string doneHint;
        public UnityEvent onDone = new UnityEvent();
    }

    [SerializeField] private Step[] steps = new Step[0];
    [Tooltip("Turn the collider off when every step is done, e.g. so you can reach the egg inside the bird cage.")]
    [SerializeField] private bool disableColliderWhenDone;

    private int current;
    private int clicks;

    public bool Done => current >= steps.Length;
    public int CurrentStep => current;

    public override void InteractionEvent() {
        if (gm == null) gm = GameManager.instance;                           // switched on this frame, before Start
        if (inventory == null) inventory = FindFirstObjectByType<Inventory>();
        if (Done) {
            Say(description);
            return;
        }

        Step step = steps[current];
        foreach (string flag in step.needs) {
            if (!gm.HasFlag(flag)) {
                Say(step.notYetHint);
                return;
            }
        }

        if (!string.IsNullOrEmpty(step.item) && inventory.SelectedItem != step.item) {
            // holding the wrong thing, or nothing at all
            Say(!string.IsNullOrEmpty(step.wrongItemHint) ? step.wrongItemHint
                : inventory.SelectedItem != null ? "Nothing happens." : description);
            return;
        }

        if (!string.IsNullOrEmpty(step.sound) && AudioManager.instance != null) AudioManager.instance.Play(step.sound);
        if (++clicks < step.clicks) {
            Say(step.clickHint);
            return;
        }
        clicks = 0;

        bool usedUp = !string.IsNullOrEmpty(step.item) && step.consumeItem;
        if (usedUp && step.giveItem != null) inventory.ReplaceCur(step.giveItem);
        else {
            if (usedUp) inventory.DestroyCur();
            if (step.giveItem != null) inventory.Add(step.giveItem);
        }

        gm.SetFlag(step.setFlag);
        Say(step.doneHint);
        current++;
        step.onDone.Invoke();

        if (Done) {
            interactAction.Invoke();
            active = !string.IsNullOrEmpty(description); // still worth a look, or nothing left to do here
            if (disableColliderWhenDone && TryGetComponent(out Collider c)) c.enabled = false;
        }
    }
}
