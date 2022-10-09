using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    float hmove = 0f;
    bool jump;
    public Rigidbody2D body;

    // Update is called once per frame
    void Update()
    {
        hmove = Input.GetAxis("Horizontal") * speed;
    }

    void FixedUpdate() {
        body.velocity = new Vector2(hmove, body.velocity.y);
        if (Input.GetKey(KeyCode.Space) && !jump) {
            body.velocity = new Vector2(body.velocity.x, speed);
        }
    }

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.CompareTag("ground")) {
            jump = false;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.CompareTag("ground")) {
            jump = true;
        }
    }
    
}
