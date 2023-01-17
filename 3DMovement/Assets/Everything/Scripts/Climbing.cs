using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Climbing : MonoBehaviour
{
    [Header("References")]
    public Transform orientation;
    public Rigidbody rb;
    public LayerMask wallLayer;
    public PlayerMovement pm;
    public LedgeGrabbing2 lg;

    [Header("Climbing")]
    public float climbSpeed;
    public float maxClimbTime;
    private float climbTimer;
    private bool climbing;

    [Header("ClimbJumping")]
    public float ClimbJumpUpForce;
    public float ClimbJumpBackForce;
    public KeyCode jumpKey = KeyCode.Space;
    public int climbJumps;
    private int climbJumpsLeft;

    [Header("Detection")]
    public float detectionLength;
    public float sphereCastRadius;
    public float maxWallLookAngle;
    private float wallLookAngle;

    private RaycastHit frontWallHit;
    private bool wallFront;

    private Transform lastWall;
    private Vector3 lastWallNormal;
    public float minWallNormalChange;

    [Header("Detection")]
    public bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    private void Start() {
        lg = GetComponent<LedgeGrabbing2>();
    }

    private void Update() {
        WallCheck();
        StateMachine();

        if (climbing && !exitingWall) {
            ClimbingMovement();
        }
    }

    private void StateMachine() {
        if (lg.holding) {
            if (climbing) {
                StopClimbing();
            }
        }
        else if (wallFront && Input.GetKey(KeyCode.W) && wallLookAngle < maxWallLookAngle && !exitingWall) {
            if (!climbing && climbTimer > 0) {
                StartClimbing();
            }

            if (climbTimer > 0) {
                climbTimer -= Time.deltaTime;
            }
            if (climbTimer < 0) {
                StopClimbing();
            }
        }
        else if (exitingWall) {
            if (climbing) {
                StopClimbing();
            }

            if (exitWallTimer > 0) {
                exitWallTimer -= Time.deltaTime;
            }
            if (exitWallTimer < 0) {
                exitingWall = false;
            }
        }
        else {
            if (climbing) {
                StopClimbing();
            }
        }

        if (wallFront && Input.GetKeyDown(jumpKey) && climbJumpsLeft > 0) {
            ClimbJump();
        }
    }

    private void WallCheck() {
        wallFront = Physics.SphereCast(transform.position, sphereCastRadius, orientation.forward, out frontWallHit, detectionLength, wallLayer);
        wallLookAngle = Vector3.Angle(orientation.forward, -frontWallHit.normal);

        bool newWall = frontWallHit.transform != lastWall || Mathf.Abs(Vector3.Angle(lastWallNormal, frontWallHit.normal)) > minWallNormalChange;

        if ((wallFront && newWall) || pm.grounded) {
            climbTimer = maxClimbTime;
            climbJumpsLeft = climbJumps;
        }
    }

    private void StartClimbing() {
        climbing = true;
        pm.climbing = true;

        lastWall = frontWallHit.transform;
        lastWallNormal = frontWallHit.normal;
    }

    private void ClimbingMovement() {
        rb.velocity = new Vector3(rb.velocity.x, climbSpeed, rb.velocity.z);
    }

    private void StopClimbing() {
        climbing = false;
        pm.climbing = false;
    }

    private void ClimbJump() {
        if (pm.grounded || lg.holding || lg.exitingLedge) {
            return;
        }

        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 appliedForce = transform.up * ClimbJumpUpForce + frontWallHit.normal * ClimbJumpBackForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(appliedForce, ForceMode.Impulse);

        climbJumpsLeft--;
    }
}
