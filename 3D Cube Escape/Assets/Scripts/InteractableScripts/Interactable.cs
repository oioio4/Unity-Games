using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

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

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();

        meshRenderer = GetComponent<MeshRenderer>();
        outline = GetComponent<Outline>();
        origColor = meshRenderer.material.color;

        outline.enabled = false;
    }

    private void Update() {
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (var collider in colliders) {
            if (collider.tag == "Player") {
                isInRange = true;
            } else {
                isInRange = false;
            }
        }
    }

    private void OnMouseOver() {
        if (isInRange) {
            meshRenderer.material.color = highlightColor;
            outline.enabled = true;

            if (/*Input.GetKeyDown(KeyCode.E)*/ Input.GetMouseButtonDown(0)) {
                InteractionEvent();
            }
        } else {
            meshRenderer.material.color = origColor;
            outline.enabled = false;
        }
    }

    private void OnMouseExit() {
        meshRenderer.material.color = origColor;
        outline.enabled = false;
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }


    // can override for individual if statement conditions (ie need to be holding a certain item for the action to occur)
    public virtual void InteractionEvent() {
        interactAction.Invoke();
    }
}