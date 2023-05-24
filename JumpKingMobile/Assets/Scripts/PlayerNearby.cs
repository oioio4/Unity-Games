using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerNearby : MonoBehaviour
{
    private Animator animator;
    private AudioManager audioManager;

    public UnityEvent action;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit2D hit = Physics2D.CircleCast(transform.position, 5f, Vector2.up);

        if (hit.collider != null) {
            if (hit.collider.gameObject.tag == "Player") {
                action.Invoke();
            }
        }
    }


    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(transform.position, 8f);
    }

}

