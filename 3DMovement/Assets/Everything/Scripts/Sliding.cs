using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sliding : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Transform player;
    private Rigidbody rb;
    private PlayerMovement pm;
    public PlayerCam cam;
    public GameObject speedLines;

    [Header("Sliding")]
    public float maxSlideTime;
    public float slideForce;
    private float slideTimer;

    public float slideYScale;
    private float startYScale;

    [Header("Input")]
    public KeyCode slideKey = KeyCode.LeftControl;
    private float hInput;
    private float vInput;

    private void Start() {
        rb = GetComponent<Rigidbody>();
        pm = GetComponent<PlayerMovement>();

        startYScale = player.localScale.y;
    }

    private void Update() {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(slideKey) && (hInput != 0 || vInput != 0) && !pm.crouching) {
            StartSlide();
        }
        else if (Input.GetKeyUp(slideKey) && pm.sliding) {
            StopSlide();
        }
    }

    private void FixedUpdate() {
        if (pm.sliding) {
            SlidingMovement();
        }
    }

    private void StartSlide() {
        pm.sliding = true;

        player.localScale = new Vector3(transform.localScale.x, slideYScale, transform.localScale.z);
        rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);

        slideTimer = maxSlideTime;

        cam.DoFov(90f);

        speedLines.SetActive(true);
    }

    private void SlidingMovement() {
        Vector3 inputDirection = orientation.forward * vInput + orientation.right * hInput;

        if (!pm.OnSlope() || rb.velocity.y > -0.1f) {
            rb.AddForce(inputDirection.normalized * slideForce, ForceMode.Force);

            slideTimer -= Time.fixedDeltaTime;
        }
        else {
            rb.AddForce(pm.SlopeDirection(inputDirection) * slideForce, ForceMode.Force);
        }

        if (slideTimer <= 0) {
            StopSlide();
        }
    }

    private void StopSlide() {
        pm.sliding = false;
        player.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);

        cam.DoFov(80f);

        speedLines.SetActive(false);
    }
}
