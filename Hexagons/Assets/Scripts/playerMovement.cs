using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class playerMovement : MonoBehaviour
{
    public float moveSpeed = 200f;
    public ParticleSystem deathEffect;
    public CameraShake cameraShake;
    public GameManager gameManager;

    float movement = 0f;
    Vector3 touchPosition;

    // Update is called once per frame
    void Start() {
        GetComponent<SpriteRenderer>().enabled = true;
    }

    void Update() 
    {
        /*
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);
            touchPosition = Camera.main.ScreenToWorldPoint(touch.position);
        }
        if (touchPosition.x < 0) {
            movement = -1f;
        }
        else if (touchPosition.x > 0) {
            movement = 1f;
        }
        else {
            movement = 0f;
        }
        */
    
        movement = Input.GetAxisRaw("Horizontal");
          
    }
    private void FixedUpdate()
    {
        transform.RotateAround(Vector3.zero, Vector3.forward, movement * Time.fixedDeltaTime * -moveSpeed);
       
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(cameraShake.Shake(0.15f, 0.4f));
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Invoke("Restart", 0.5f);
        GetComponent<SpriteRenderer>().enabled = false;
    }

    private void Restart() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}