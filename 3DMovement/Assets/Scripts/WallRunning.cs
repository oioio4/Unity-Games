using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallRunning : MonoBehaviour
{
    [Header("Wallrunning")]
    public LayerMask wallLayer;
    public LayerMask groundLayer;
    public float wallRunForce;
    public float wallJumpUpForce;
    public float wallJumpSideForce;
    //public float wallClimbSpeed;
    public float maxWallRunTime;
    private float wallRunTimer;

    [Header("Input")]
    public KeyCode jumpKey = KeyCode.Space;
    //public KeyCode upwardsRunKey = KeyCode.LeftShift;
    //public KeyCode downwardsRunKey = KeyCode.LeftControl;
    //private bool upwardsRunning;
    //private bool downwardsRunning;
    private float hInput;
    private float vInput;

    [Header("Detection")]
    public float wallCheckDistance;
    public float minJumpHeight;
    private RaycastHit leftWallhit;
    private RaycastHit rightWallhit;
    private bool wallLeft;
    private bool wallRight;
    public Vector3 prevWallNormal;

    [Header("Exiting")]
    private bool exitingWall;
    public float exitWallTime;
    private float exitWallTimer;

    [Header("Gravity")]
    public bool useGravity;
    public float gravityCounterForce;

    [Header("References")]
    public Transform orientation;
    public PlayerCam cam;
    private PlayerMovement pm;
    private Rigidbody rb;

    private void Start() {
        pm = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update() {
        CheckForWall();
        StateMachine();
    }

    private void FixedUpdate() {
        if (pm.wallrunning) {
            WallRunningMovement();
        }
    }

    private void CheckForWall() {
        wallRight = Physics.Raycast(transform.position, orientation.right, out rightWallhit, wallCheckDistance, wallLayer);
        wallLeft = Physics.Raycast(transform.position, -orientation.right, out leftWallhit, wallCheckDistance, wallLayer);
    }

    private bool AboveGround() {
        return !Physics.Raycast(transform.position, Vector3.down, minJumpHeight, groundLayer);
    }

    private void StateMachine() {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        //upwardsRunning = Input.GetKey(upwardsRunKey);
        //downwardsRunning = Input.GetKey(downwardsRunKey);

        if ((wallLeft || wallRight) && vInput > 0 && AboveGround() && !exitingWall) {
            Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

            if (!pm.wallrunning && wallNormal != prevWallNormal) {
                StartWallRun();
            }

            if (wallRunTimer > 0) {
                wallRunTimer -= Time.deltaTime;
            }
            
            if (wallRunTimer <= 0 && pm.wallrunning) {
                exitingWall = true;
                exitWallTimer = exitWallTime;
            }

            if (Input.GetKeyDown(jumpKey)) {
                WallJump();
            }
        }
        else if (exitingWall) {
            if (pm.wallrunning) {
                StopWallRun();
            }

            if (exitWallTimer > 0) {
                exitWallTimer -= Time.deltaTime;
            }
            else {
                exitingWall = false;
            }
        }
        else {
           if (pm.wallrunning) {
                StopWallRun();
            } 
        }
    }

    private void StartWallRun() {
        pm.wallrunning = true;

        wallRunTimer = maxWallRunTime;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        cam.DoFov(90f);

        if (wallLeft) {
            cam.DoTilt(-5f);
        }
        else if (wallRight) {
            cam.DoTilt(5f);
        }
    }

    private void WallRunningMovement() {
        rb.useGravity = useGravity;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 wallForward = Vector3.Cross(wallNormal, transform.up);

        if ((orientation.forward - wallForward).magnitude > (orientation.forward + wallForward).magnitude) {
            wallForward = -wallForward;
        }

        rb.AddForce(wallForward * wallRunForce, ForceMode.Force);
        
        rb.velocity = new Vector3(rb.velocity.x, -cam.xRotation / 10f, rb.velocity.z);

        if (!(wallLeft && hInput > 0) && !(wallRight && hInput < 0)) {
            rb.AddForce(-wallNormal * 100, ForceMode.Force);
        }

        if (useGravity) {
            rb.AddForce(transform.up * gravityCounterForce, ForceMode.Force);
        }

        prevWallNormal = wallNormal;
    }

    private void StopWallRun() {
        pm.wallrunning = false;

        cam.DoFov(80f);
        cam.DoTilt(0);
    }

    private void WallJump() {
        exitingWall = true;
        exitWallTimer = exitWallTime;

        Vector3 wallNormal = wallRight ? rightWallhit.normal : leftWallhit.normal;

        Vector3 appliedForce = transform.up * wallJumpUpForce + wallNormal * wallJumpSideForce;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        rb.AddForce(appliedForce, ForceMode.Impulse);
    }
}
