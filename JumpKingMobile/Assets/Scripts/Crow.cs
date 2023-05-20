using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crow : MonoBehaviour
{
    private Animator animator;
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 10f, Vector2.up);

        if (hit.collider != null) {
            if (hit.collider.gameObject.tag == "Player") {
                animator.Play("Say");
            }
        }
    }

    private void Caw() {
        audioManager.Play("Caw");    
    }

    private void Fly() {
        audioManager.Play("Fly");
    }

    private void Die() {
        Destroy(gameObject, 3f);
    }

/*
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 8f);
    }
*/
}
