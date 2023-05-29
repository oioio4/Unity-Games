using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointSystem : MonoBehaviour
{
    private Vector3 respawnPoint;
    private Rigidbody2D rb;
    private Animator animator;

    [SerializeField] private GameObject respawnParticles;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        respawnPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D col) {
        if (col.tag == "FallDetector") {
            transform.position = respawnPoint;
            rb.velocity = new Vector2(0f, 0f);
            animator.Play("Respawn");
            GameObject particles = Instantiate(respawnParticles, this.transform);
            Destroy(particles, 1.5f);
        } else if (col.tag == "Checkpoint") {
            respawnPoint = col.gameObject.transform.position; 
        }
    } 
}
