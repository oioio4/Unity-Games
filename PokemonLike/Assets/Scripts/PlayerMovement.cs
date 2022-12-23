using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public LayerMask obstaclesLayer;
    public LayerMask grassLayer;

    private bool isWalking;
    private Vector2 input;

    private Animator animator;

    private void Awake() {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isWalking) {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            if (input.x != 0) {
                input.y = 0;
            }

            if (input != Vector2.zero) {
                animator.SetFloat("Xmove", input.x);
                animator.SetFloat("Ymove", input.y);

                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos)) {
                   StartCoroutine(Move(targetPos)); 
                }
            }
        }

        animator.SetBool("isWalking", isWalking);
    }

    IEnumerator Move(Vector3 targetPos) {
        isWalking = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon) {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = targetPos;
        isWalking = false;

        CheckForEncounters();
    }

    private bool IsWalkable(Vector3 targetPos) {
        if (Physics2D.OverlapCircle(targetPos, 0.2f, obstaclesLayer) != null) {
            return false;
        }

        return true;
    }

    private void CheckForEncounters() {
        if (Physics2D.OverlapCircle(transform.position, 0.1f, grassLayer) != null) {
            if (Random.Range(1, 101) <= 10) {
                Debug.Log("Wild Pokemon");
            }
        }
    }

    /*
    private void OnDrawGizmosSelected() {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(transform.position, 1f);
    }
    */
}
