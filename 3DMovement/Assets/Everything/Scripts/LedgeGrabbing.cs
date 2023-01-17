using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LedgeGrabbing : MonoBehaviour
{
    [Header("References")]
    public PlayerMovement pm;
    public Transform orientation;
    public Transform cam;
    public Rigidbody rb;

    [Header("Ledge Grabbing")]
    public float moveToLedgeSpeed;
    public float maxLedgeGrabDistance;

    public float minTimeOnLedge;
    private float timeOnLedge;

    public bool holding;

    [Header("Ledge Jumping")]
    public KeyCode jumpKey = KeyCode.Space;
    public float ledgeJumpForwardForce;
    public float ledgeJumpUpwardForce;

    [Header("Ledge Detection")]
    public float ledgeDetectionLength;
    public float ledgeSphereCastRadius;
    public LayerMask ledgeLayer;

    private Transform lastLedge;
    private Transform curLedge;
    private RaycastHit ledgeHit;

    [Header("Exiting")]
    public bool exitingLedge;
    public float exitLedgeTime;
    private float exitLedgeTimer;

    private void Update() {
        LedgeDetection();
        SubStateMachine();
    }

    private void SubStateMachine() {
        float hInput = Input.GetAxisRaw("Horizontal");
        float vInput = Input.GetAxisRaw("Vertical");
        bool input = hInput != 0 || vInput != 0;

        if (holding) {
            OnLedge();

            timeOnLedge += Time.deltaTime;

            if (timeOnLedge > minTimeOnLedge && input) {
                ExitLedgeHold();
            }

            if (Input.GetKeyDown(jumpKey)) {
                LedgeJump();
            }
        }
        else if (exitingLedge) {
            if (exitLedgeTimer > 0) {
                exitLedgeTimer -= Time.deltaTime;
            }
            else {
                exitingLedge = false;
            }
        }
    }

    private void LedgeDetection() {
        bool ledgeDetected = Physics.SphereCast(transform.position, ledgeSphereCastRadius, cam.forward, out ledgeHit, ledgeDetectionLength, ledgeLayer);

        if (!ledgeDetected) {
            return;
        }

        float distanceToLedge = Vector3.Distance(transform.position, ledgeHit.transform.position);

        if (ledgeHit.transform == lastLedge) {
            return;
        }

        if (distanceToLedge < maxLedgeGrabDistance && !holding) {
            EnterLedgeHold();
        }
    }

    private void EnterLedgeHold() {
        holding = true;

        pm.unlimited = true;
        pm.restricted = true;

        curLedge = ledgeHit.transform;
        lastLedge = ledgeHit.transform;

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
    }

    private void OnLedge() {
        rb.useGravity = false;

        Vector3 directionToLedge = curLedge.position - transform.position;
        float distanceToLedge = Vector3.Distance(transform.position, curLedge.position);

        if (distanceToLedge > 0.7f) {
            if (rb.velocity.magnitude < moveToLedgeSpeed) {
                rb.AddForce(directionToLedge.normalized * moveToLedgeSpeed * 1500f * Time.deltaTime);
            }
        }    
        else {
            if (!pm.freeze) {
                pm.freeze = true;
            }

            if (pm.unlimited) {
                pm.unlimited = false;
            }
        }

        if (distanceToLedge > maxLedgeGrabDistance) {
            ExitLedgeHold();
        }
    }

    private void ExitLedgeHold() {
        exitingLedge = true;
        exitLedgeTimer = exitLedgeTime;

        holding = false;
        timeOnLedge = 0f;

        pm.freeze = false;
        pm.restricted = false;

        rb.useGravity = true;

        StopAllCoroutines();
        Invoke(nameof(ResetLastLedge), 1f); 
    }

    private void ResetLastLedge() {
        lastLedge = null;
    }

    private void LedgeJump() {
        ExitLedgeHold();

        Invoke(nameof(DelayedJumpForce), 0.1f);
    }

    private void DelayedJumpForce() {
        Vector3 appliedForce = cam.forward * ledgeJumpForwardForce + orientation.up * ledgeJumpUpwardForce;
        rb.velocity = Vector3.zero;
        rb.AddForce(appliedForce, ForceMode.Impulse);
    }
}
