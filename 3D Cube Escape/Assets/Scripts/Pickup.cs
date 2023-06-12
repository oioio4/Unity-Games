using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public Inventory inventory;
    public GameObject itemButton;

    private MeshRenderer meshRenderer;
    private Outline outline;
    private Color origColor;
    public Color highlightColor;

    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        outline = GetComponent<Outline>();
        origColor = meshRenderer.material.color;
    }

    private void OnMouseOver() {
        meshRenderer.material.color = highlightColor;
        outline.enabled = true;

        if (/*Input.GetKeyDown(KeyCode.E)*/ Input.GetMouseButtonDown(0)) {
                inventoryAdd();
        }
    }

    private void OnMouseExit() {
        meshRenderer.material.color = origColor;
        outline.enabled = false;
    }

    private void inventoryAdd() {
        for (int i = 0; i < inventory.slots.Length; i++) {
            if (inventory.isFull[i] == false) {
                inventory.isFull[i] = true;
                Instantiate(itemButton, inventory.slots[i].transform, false);
                Destroy(gameObject);
                break;
            }
        }
    }
}
