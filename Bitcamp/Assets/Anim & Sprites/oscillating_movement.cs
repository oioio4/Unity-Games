using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class oscillating_movement : MonoBehaviour
{
    int maxValue = 5;
    int minValue = -5; // or whatever you want the min value to be
    float currentValue = 0f; // or wherever you want to start
    int direction = 1; 
    [SerializeField] float speed = 1;
    SpriteRenderer spriteRenderer;

    void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    void Update() {
        currentValue += speed * Time.deltaTime * direction; // or however you are incrementing the position

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

        transform.position = new Vector3(currentValue, 0, 0);
    }
}
