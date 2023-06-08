using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerNearby : MonoBehaviour
{
    private Animator animator;
    private AudioManager audioManager;

    [SerializeField] private bool activated = false;
    [SerializeField] RaycastHit2D hit;

    public UnityEvent action;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col != null) {
            if (col.gameObject.tag == "Player") {
                action.Invoke();
                activated = true;
            }
        }
    }
}

