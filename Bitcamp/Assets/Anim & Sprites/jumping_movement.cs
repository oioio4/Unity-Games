using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jumping_movement : MonoBehaviour
{
    int maxValue = 5;
    int minValue = -5; // or whatever you want the min value to be
    float currentValue = 0f; // or wherever you want to start
    int direction = 1; 
    [SerializeField] float timer = 0f;
    [SerializeField] float jumpTimer = 1.085f;
    [SerializeField] float jumpForce = 2f;
    SpriteRenderer spriteRenderer;
    Rigidbody2D rigidBody;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody = GetComponent<Rigidbody2D>();
    }
    void Update() {
        timer += Time.deltaTime;
        currentValue += Time.deltaTime * direction; // or however you are incrementing the position

        if(currentValue >= maxValue) {
            spriteRenderer.flipX  = false;
            direction *= -1;
            currentValue = maxValue;
        } 
        else if (currentValue <= minValue){
            spriteRenderer.flipX = true;
            direction *= -1; 
            currentValue = minValue;
        }

        if (timer >= jumpTimer) {
            timer = 0f;
            rigidBody.AddForce(new Vector3(jumpForce * direction, 0f, 0f), ForceMode2D.Impulse);
        }
    }
}
