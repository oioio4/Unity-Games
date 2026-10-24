using UnityEngine;

// One wheel of a combination: a digit on the safe's dial, or a hand of the clock.
// Each click moves it on a step; its CodeLock checks whether the whole code is right.
public class CodeWheel : Interactable
{
    [SerializeField] private string[] values = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
    [SerializeField] private int index;
    [Tooltip("Shows the current value, e.g. a digit on the safe.")]
    [SerializeField] private TextMesh label;
    [Tooltip("Or turns this by stepRotation per step, e.g. a clock hand.")]
    [SerializeField] private Transform pointer;
    [SerializeField] private Vector3 stepRotation = new Vector3(0f, 0f, 30f);
    [SerializeField] private CodeLock codeLock;

    private Quaternion pointerStart;

    public string Value => values[index];

    private void Awake() {
        if (pointer != null) pointerStart = pointer.localRotation;
    }

    protected override void Start() {
        base.Start();
        Show();
    }

    public override void InteractionEvent() {
        if (codeLock != null && codeLock.Solved && !codeLock.KeepTurning) {
            Say(description);
            return;
        }
        if (codeLock != null && !codeLock.Ready) {
            Say(codeLock.LockedHint);
            return;
        }
        index = (index + 1) % values.Length;
        Show();
        if (codeLock != null) codeLock.Check();
    }

    private void Show() {
        if (label != null) label.text = Value;
        if (pointer != null) pointer.localRotation = pointerStart * Quaternion.Euler(stepRotation * index);
    }
}
