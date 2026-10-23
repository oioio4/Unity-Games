using UnityEngine;
using UnityEngine.Events;

// Something in the room you can click on once you're close enough. It lights up under
// the cursor; clicking runs InteractionEvent, which subclasses override (PuzzleInteractable).
public class Interactable : MonoBehaviour
{
    public GameManager gm;
    public Inventory inventory;

    private MeshRenderer meshRenderer;
    private Outline outline;
    private Color origColor;
    public Color highlightColor;

    public UnityEvent interactAction;

    [SerializeField] private float range = 2f;
    [SerializeField] private bool isInRange = false;
    public bool active = true;

    // what you notice when you click it, if nothing else happens
    [TextArea] [SerializeField] protected string description;

    protected virtual void Start()
    {
        gm = GameManager.instance; // the one that survives scene loads, not a duplicate about to be destroyed
        inventory = FindFirstObjectByType<Inventory>();

        // the tint and the outline are both optional, e.g. the bird cage has neither on its root
        meshRenderer = GetComponent<MeshRenderer>();
        outline = GetComponent<Outline>();
        if (meshRenderer != null) origColor = meshRenderer.material.color;
        if (outline != null) outline.enabled = false;
    }

    private void OnMouseOver() {
        isInRange = PlayerInRange();
        bool usable = isInRange && active && Time.timeScale > 0f;
        Highlight(usable);

        if (usable && Input.GetMouseButtonDown(0)) {
            InteractionEvent();
        }
    }

    private void OnMouseExit() {
        Highlight(false);
    }

    private bool PlayerInRange() {
        foreach (var collider in Physics.OverlapSphere(transform.position, range)) {
            if (collider.CompareTag("Player")) return true;
        }
        return false;
    }

    private void Highlight(bool on) {
        if (meshRenderer != null) meshRenderer.material.color = on ? highlightColor : origColor;
        if (outline != null) outline.enabled = on;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }

    // can override for individual if statement conditions (ie need to be holding a certain item for the action to occur)
    public virtual void InteractionEvent() {
        interactAction.Invoke();
        Say(description);
    }

    protected static void Say(string message) {
        HintText.Show(message);
    }
}
