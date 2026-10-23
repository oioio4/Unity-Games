using UnityEngine;
using UnityEngine.Events;

// Use an item on something as often as you like (nothing is used up), e.g. the telescope
// at the window. For one-off steps use PuzzleInteractable.
public class UseItemTrigger : Interactable
{
    [Tooltip("Item to have selected in the hotbar. Blank: just click.")]
    [SerializeField] private string item;
    [SerializeField] private string[] needs = new string[0];
    [TextArea] [SerializeField] private string notYetHint;
    [TextArea] [SerializeField] private string wrongItemHint;
    [SerializeField] private UnityEvent onUse = new UnityEvent();

    public override void InteractionEvent() {
        if (gm == null) gm = GameManager.instance;
        if (inventory == null) inventory = FindFirstObjectByType<Inventory>();

        foreach (string flag in needs) {
            if (!gm.HasFlag(flag)) {
                Say(notYetHint);
                return;
            }
        }
        if (!string.IsNullOrEmpty(item) && inventory.SelectedItem != item) {
            Say(!string.IsNullOrEmpty(wrongItemHint) ? wrongItemHint : description);
            return;
        }
        onUse.Invoke();
    }
}
