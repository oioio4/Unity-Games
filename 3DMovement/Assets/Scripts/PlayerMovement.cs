using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    private float moveSpeed;
    public float walkSpeed;
    public float sprintSpeed;
    public float slideSpeed;
    public float wallrunSpeed;
    public float climbSpeed;

    private float desiredMoveSpeed;
    private float lastDesiredMoveSpeed;

    public float speedIncreaseMultiplier;
    public float slopeIncreaseMultiplier;

    public float groundDrag;
    private bool keepMomentum;

    [Header("Jumping")]
    public float jumpForce;
    public float jumpCooldown;
    public float airMultiplier;
    [SerializeField] private bool canJump;

    [Header("Crouching")]
    public float crouchSpeed;
    public float crouchYScale;
    private float startYScale;

    [Header("KeyBinds")]
    public KeyCode jumpKey = KeyCode.Space;
    public KeyCode sprintKey = KeyCode.LeftShift;
    public KeyCode crouchKey = KeyCode.LeftControl;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask groundLayer;
    public bool grounded;

    [Header("Slope Handling")]
    public float maxSlopeAngle;
    private RaycastHit slopeHit;
    private bool exitingSlope;
    [SerializeField] private bool slope;

    [Header("References")]
    public Transform orientation;
    public Climbing climbingScript;

    float hInput;
    float vInput;

    Vector3 moveDirection;

    Rigidbody rb;

    public MovementState state;
    public enum MovementState {
       freeze, unlimited, walking, sprinting, crouching, sliding, wallrunning, climbing, air
    }

    public bool sliding;
    public bool wallrunning;
    public bool climbing;

    public bool freeze;
    public bool unlimited;
    public bool restricted;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        canJump = true;

        startYScale = transform.localScale.y;
    }

    private void Update() {
        MyInput();
        SpeedControl();
        StateHandler();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, groundLayer);

        if (grounded) {
            rb.drag = groundDrag;
            GetComponent<WallRunning>().lastWall = null;
        }
        else {
            rb.drag = 0;
        }

        slope = OnSlope();
    }

    private void FixedUpdate() {
        MovePlayer();
    }

    private void MyInput() {
        hInput = Input.GetAxisRaw("Horizontal");
        vInput = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && grounded) {
            canJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpCooldown);
        }

        if (Input.GetKeyDown(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, crouchYScale, transform.localScale.z);
            rb.AddForce(Vector3.down * 5f, ForceMode.Impulse);
        }
        else if (Input.GetKeyUp(crouchKey)) {
            transform.localScale = new Vector3(transform.localScale.x, startYScale, transform.localScale.z);
        }
    }

    private void StateHandler() {
        if (freeze) {
            state = MovementState.freeze;
            rb.velocity = Vector3.zero;
            desiredMoveSpeed = 0f;
        }
        else if (unlimited) {
            state = MovementState.unlimited;
            desiredMoveSpeed = 999f;
            return;
        }
        else if (climbing) {
            state = MovementState.climbing;
            desiredMoveSpeed = climbSpeed;
        }
        else if (wallrunning) {
            state = MovementState.wallrunning;
            desiredMoveSpeed = wallrunSpeed;
        }
        else if (sliding) {
            state = MovementState.sliding;

            if (OnSlope() && rb.velocity.y < 0.1f) {
                desiredMoveSpeed = slideSpeed;
                keepMomentum = true;
            }
            else {
                desiredMoveSpeed = sprintSpeed;
            }
        }
        else if (Input.GetKey(crouchKey)) {
            state = MovementState.crouching;
            desiredMoveSpeed = crouchSpeed;
        }
        else if (grounded && Input.GetKey(sprintKey)) {
            state = MovementState.sprinting;
            desiredMoveSpeed = sprintSpeed;
        }
        else if (grounded) {
            state = MovementState.walking;
            desiredMoveSpeed = walkSpeed;
        }
        else {
            state = MovementState.air;
        }

        bool desiredMoveSpeedChange = desiredMoveSpeed != lastDesiredMoveSpeed;

        if (desiredMoveSpeedChange) {
            if (keepMomentum) {
                StopAllCoroutines();
                StartCoroutine(TransitionMoveSpeed());
            }
            else {
                moveSpeed = desiredMoveSpeed;
            }
        }

        /*
        if (Mathf.Abs(desiredMoveSpeed - lastDesiredMoveSpeed) > 4f && moveSpeed != 0) {
            StopAllCoroutines();
            StartCoroutine(TransitionMoveSpeed());
        }
        else {
            moveSpeed = desiredMoveSpeed;
        }
        */

        lastDesiredMoveSpeed = desiredMoveSpeed;

        if (Mathf.Abs(desiredMoveSpeed - moveSpeed) < 0.1f) {
            keepMomentum = false;
        }
    }

    private IEnumerator TransitionMoveSpeed() {
        float time = 0f;
        float difference = Mathf.Abs(desiredMoveSpeed - moveSpeed);
        float startVal = moveSpeed;

        while (time < difference) {
            moveSpeed = Mathf.Lerp(startVal, desiredMoveSpeed, time / difference);

            if (OnSlope()) {
                float slopeAngle = Vector3.Angle(Vector3.up, slopeHit.normal);
                float slopeAngleIncrease = 1 + (slopeAngle / 90f);

                time += Time.deltaTime * speedIncreaseMultiplier * slopeIncreaseMultiplier;
            }
            else {
                time += Time.deltaTime * speedIncreaseMultiplier;
            }

            yield return null;
        }

        moveSpeed = desiredMoveSpeed;
    }

    private void MovePlayer() {
        if (restricted || climbingScript.exitingWall) {
            return;
        }
        
        moveDirection = orientation.forward * vInput + orientation.right * hInput;

        if (OnSlope() && !exitingSlope) {
            rb.AddForce(SlopeDirection(moveDirection) * moveSpeed * 20f, ForceMode.Force);

            if (rb.velocity.y > 0) {
                rb.AddForce(Vector3.down * 150f, ForceMode.Force);
            }
        }
        else if (grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded) {
            rb.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplier, ForceMode.Force);
        }

        if (!wallrunning) {
            rb.useGravity = !OnSlope();
        }
    }

    private void SpeedControl() {
        // slope momentum
        if (OnSlope() && !exitingSlope) {
            if (rb.velocity.magnitude > moveSpeed) {
                rb.velocity = rb.velocity.normalized * moveSpeed;
            }
        }
        else {
            //float time = 0f;
            Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

            if (flatVel.magnitude > moveSpeed) {
                /*
                float difference = Mathf.Abs(rb.velocity.magnitude - moveSpeed);

                while (time < difference) {
                    rb.velocity = rb.velocity.normalized * Mathf.Lerp(rb.velocity.magnitude, moveSpeed, time / difference);
                    time += Time.deltaTime;
                }
                */
                Vector3 limitedVel = flatVel.normalized * moveSpeed;
                rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
            }
        }
    }

    private void Jump() {
        exitingSlope = true;

        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump() {
        canJump = true;

        exitingSlope = false;
    }

    public bool OnSlope() {
        if (Physics.Raycast(transform.position, Vector3.down, out slopeHit, playerHeight * 0.5f + 0.3f)) {
            float angle = Vector3.Angle(Vector3.up, slopeHit.normal);
            return angle < maxSlopeAngle && angle != 0;
        }

        return false;
    }

    public Vector3 SlopeDirection(Vector3 direction) {
        return Vector3.ProjectOnPlane(direction, slopeHit.normal).normalized;
    }
}
