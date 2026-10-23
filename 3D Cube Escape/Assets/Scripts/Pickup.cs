using UnityEngine;

// An item lying in the room. Click it when you're close to put its icon in the hotbar.
public class Pickup : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemButton;   // the hotbar icon this becomes; its name is the item's id

    private MeshRenderer meshRenderer;
    private Outline outline;
    private Color origColor;
    public Color highlightColor;

    [SerializeField] private float range = 2.5f;

    // Start is called before the first frame update
    void Start()
    {
        inventory = FindFirstObjectByType<Inventory>();

        meshRenderer = GetComponent<MeshRenderer>();
        outline = GetComponent<Outline>();
        if (meshRenderer != null) origColor = meshRenderer.material.color;
        if (outline != null) outline.enabled = false;
    }

    private void OnMouseOver() {
        bool usable = PlayerInRange() && Time.timeScale > 0f;
        Highlight(usable);

        if (usable && Input.GetMouseButtonDown(0)) {
            InventoryAdd();
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

    public void InventoryAdd() {
        if (inventory == null) inventory = FindFirstObjectByType<Inventory>(); // switched on this frame, before Start
        if (!inventory.Add(itemButton)) {
            HintText.Show("Your hands are full.");
            return;
        }
        if (AudioManager.instance != null) AudioManager.instance.Play("Pickup");
        Destroy(gameObject);
    }
}
