using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpMovement : MonoBehaviour
{
    [SerializeField] float timer = 0f;
    [SerializeField] float jumpTimer = 1.085f;
    [SerializeField] float jumpForce = 2f;
    [SerializeField] float speed = 2f;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update() {
        timer += Time.deltaTime;
        transform.Translate(new Vector3(-1f, 0f, 0f) * speed * Time.deltaTime);

        if (timer >= jumpTimer) {
            timer = 0f;
            rigidBody.AddForce(new Vector3(30f, jumpForce, 0f), ForceMode2D.Impulse);
        }
    }
}

