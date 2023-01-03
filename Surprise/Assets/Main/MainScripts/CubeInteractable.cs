using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class CubeInteractable : MonoBehaviour
{
    MeshRenderer meshRenderer;
    Color origColor;
    public Color highlightColor;
    public GameObject collect;
    public UnityEvent interactAction;
    public GameObject particles;
    public bool active = true;

    [SerializeField] private bool isInRange;

    private void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        origColor = meshRenderer.material.color;
    }

    private void Update() {
        bool playerExists = false;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (var collider in colliders) {
            if (collider.tag == "Player") {
                isInRange = true;
                playerExists = true;
            }
            else if (!playerExists) {
                isInRange = false;
            }
        }
        playerExists = false;
    }

    private void OnMouseOver() {
        if (isInRange && active) {
            meshRenderer.material.color = highlightColor;
            collect.SetActive(true);
            if (Input.GetKeyDown(KeyCode.E)) {
                collect.SetActive(false);
                interactAction.Invoke();
            }
        }
        else {
            meshRenderer.material.color = origColor;
            collect.SetActive(false); 
        }
    }

    private void OnMouseExit() {
        meshRenderer.material.color = origColor;
        collect.SetActive(false);
    }

    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 2f);
    }

    public void DestroyParticles() {
        Destroy(particles);
    }
}
