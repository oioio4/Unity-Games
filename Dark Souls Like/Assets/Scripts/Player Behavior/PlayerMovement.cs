using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC {
    public class PlayerMovement : MonoBehaviour
    {
        CameraHandler cameraHandler;
        PlayerManager playerManager;
        PlayerStats playerStats;
        Transform cameraObject;
        InputHandler inputHandler;
        public Vector3 moveDirection;

        [HideInInspector] 
        public Transform myTransform;
        [HideInInspector] 
        public AnimatorHandler animatorHandler;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Ground & Air Detection")]
        [SerializeField]
        float groundDetectionRayStartPoint = 0.3f;
        [SerializeField]
        float minimumDistanceNeededToBeginFall = 1f;
        [SerializeField]
        float groundDirectionRayDistance = 0.2f;
        LayerMask ignoreForGroundCheck;
        public float inAirTimer;

        [Header("Movement Stats")]
        [SerializeField]
        float movementSpeed = 5f;
        [SerializeField]
        float walkingSpeed = 6f;
        [SerializeField]
        float sprintSpeed = 8f;
        [SerializeField]
        float rotationSpeed = 20f;
        [SerializeField]
        float fallingSpeed = 45f;

        [Header("Stamina Costs")]
        [SerializeField]
        int rollStaminaCost = 15;
        int backstepStaminaCost = 10;
        int sprintStaminaCost = 1;

        public CapsuleCollider characterCollider;
        public CapsuleCollider characterCollisionBlockerCollider;

        private void Awake() {
            cameraHandler = FindObjectOfType<CameraHandler>();
            playerManager = GetComponent<PlayerManager>();
            playerStats = GetComponent<PlayerStats>();
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
        }

        // Start is called before the first frame update
        void Start()
        {
            cameraObject = Camera.main.transform;
            myTransform = transform;
            animatorHandler.Initialize();

            playerManager.isGrounded = true;
            ignoreForGroundCheck = ~(1 << 11);
            Physics.IgnoreCollision(characterCollider, characterCollisionBlockerCollider, true);
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;



        public void HandleRotation(float delta) {
            if (animatorHandler.canRotate) {
                if (inputHandler.lockOnFlag) {
                    if (inputHandler.sprintflag || inputHandler.rollflag) {
                        Vector3 targetDirection = Vector3.zero;
                        targetDirection = cameraHandler.cameraTransform.forward * inputHandler.vertical;
                        targetDirection += cameraHandler.cameraTransform.right * inputHandler.horizontal;
                        targetDirection.Normalize();
                        targetDirection.y = 0;

                        if (targetDirection == Vector3.zero) {
                            targetDirection = transform.forward;
                        }

                        Quaternion tr = Quaternion.LookRotation(targetDirection);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                        transform.rotation = targetRotation;
                    }
                    else {
                        Vector3 rotationDirection = moveDirection;
                        rotationDirection = cameraHandler.currentLockOnTarget.transform.position - transform.position;
                        rotationDirection.y = 0;
                        rotationDirection.Normalize();
                        Quaternion tr = Quaternion.LookRotation(rotationDirection);
                        Quaternion targetRotation = Quaternion.Slerp(transform.rotation, tr, rotationSpeed * Time.deltaTime);

                        transform.rotation = targetRotation;
                    }
                }
                else {
                    Vector3 targetDir = Vector3.zero;
                    float moveOverride = inputHandler.moveAmount;

                    targetDir = cameraObject.forward * inputHandler.vertical;
                    targetDir += cameraObject.right * inputHandler.horizontal;

                    targetDir.Normalize();
                    targetDir.y = 0;

                    if (targetDir == Vector3.zero) {
                        targetDir = myTransform.forward;
                    }

                    float rs = rotationSpeed;

                    Quaternion tr = Quaternion.LookRotation(targetDir);
                    Quaternion targetRotation = Quaternion.Slerp(myTransform.rotation, tr, rs * delta);

                    myTransform.rotation = targetRotation;
                }
            }

        }

        public void HandleMovement(float delta) {
            if (inputHandler.rollflag) {
                return;
            }

            if (playerManager.isInteracting) {
                return;
            }

            moveDirection = cameraObject.forward * inputHandler.vertical;
            moveDirection += cameraObject.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;

            if (inputHandler.sprintflag && inputHandler.moveAmount > 0.5) {
                speed = sprintSpeed;
                playerManager.isSprinting = true;
                moveDirection *= speed;
                playerStats.DrainStamina(sprintStaminaCost);
            }
            else {
                if (inputHandler.moveAmount < 0.5) {
                    moveDirection *= walkingSpeed;
                    playerManager.isSprinting = false;
                }
                else {
                    moveDirection *= speed;
                    playerManager.isSprinting = false;
                }
            }

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;

            if (inputHandler.lockOnFlag && !inputHandler.sprintflag) {
                animatorHandler.UpdateAnimatorValues(inputHandler.vertical, inputHandler.horizontal, playerManager.isSprinting);
            }
            else {
                animatorHandler.UpdateAnimatorValues(inputHandler.moveAmount, 0, playerManager.isSprinting);

            }
        }

        public void HandleRollingAndSprinting(float delta) {
            if (animatorHandler.anim.GetBool("isInteracting")) {
                return;
            }
            if (playerStats.currentStamina <= 0) {
                return;
            }

            if (inputHandler.rollflag) {
                moveDirection = cameraObject.forward * inputHandler.vertical;
                moveDirection += cameraObject.right * inputHandler.horizontal;

                if (inputHandler.moveAmount > 0) {
                    animatorHandler.PlayTargetAnimation("Rolling", true);
                    moveDirection.y = 0;
                    Quaternion rollRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = rollRotation;
                    playerStats.DrainStamina(rollStaminaCost);
                }
                else {
                    animatorHandler.PlayTargetAnimation("Backstep", true);
                    playerStats.DrainStamina(backstepStaminaCost);
                }
            }
        }

        public void HandleFalling(float delta, Vector3 moveDirection) {
            playerManager.isGrounded = false;
            RaycastHit hit;
            Vector3 origin = myTransform.position;
            origin.y += groundDetectionRayStartPoint;

            if (Physics.Raycast(origin, myTransform.forward, out hit, 0.4f)) {
                moveDirection = Vector3.zero;
            }

            if (playerManager.isInAir) {
                rigidbody.AddForce(-Vector3.up * fallingSpeed);
                rigidbody.AddForce(moveDirection * fallingSpeed / 5f);
            }

            Vector3 dir = moveDirection;
            dir.Normalize();
            origin = origin + dir * groundDirectionRayDistance;

            targetPosition = myTransform.position;

            Debug.DrawRay(origin, -Vector3.up * minimumDistanceNeededToBeginFall, Color.red, 0.1f, false);
            if (Physics.Raycast(origin, -Vector3.up, out hit, minimumDistanceNeededToBeginFall, ignoreForGroundCheck)) {
                normalVector = hit.normal;
                Vector3 tp = hit.point;
                playerManager.isGrounded = true;
                targetPosition.y = tp.y;

                if (playerManager.isInAir) {
                    if (inAirTimer > 0.5f) {
                        animatorHandler.PlayTargetAnimation("Land", true);
                        inAirTimer = 0;
                    }
                    else {
                        animatorHandler.PlayTargetAnimation("Empty", false);
                        inAirTimer = 0;
                    }

                    playerManager.isInAir = false;
                }
            }
            else {
                if (playerManager.isGrounded) {
                    playerManager.isGrounded = false;
                }

                if (playerManager.isInAir == false) {
                    if (playerManager.isInteracting == false) {
                        animatorHandler.PlayTargetAnimation("Falling", true);
                    }

                    Vector3 vel = rigidbody.velocity;
                    vel.Normalize();
                    rigidbody.velocity = vel * (movementSpeed / 2);
                    playerManager.isInAir = true;
                }
            }

            if (playerManager.isInteracting || inputHandler.moveAmount > 0) {
                myTransform.position = Vector3.Lerp(myTransform.position, targetPosition, Time.deltaTime / 0.1f);
            }
            else {
                myTransform.position = targetPosition;
            }
        }

        public void HandleJumping() {
            if (playerManager.isInteracting) {
                return;
            }
            if (playerStats.currentStamina <= 0) {
                return;
            }

            if (inputHandler.jump_Input) {
                if (inputHandler.moveAmount > 0) {
                    moveDirection = cameraObject.forward * inputHandler.vertical;
                    moveDirection += cameraObject.right * inputHandler.horizontal;
                    animatorHandler.PlayTargetAnimation("Jump", true);
                    moveDirection.y = 0;
                    Quaternion jumpRotation = Quaternion.LookRotation(moveDirection);
                    myTransform.rotation = jumpRotation;
                }
            }
        }

        #endregion 
    }

}
