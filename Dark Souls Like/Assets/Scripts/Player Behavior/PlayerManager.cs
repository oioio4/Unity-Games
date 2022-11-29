using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC {
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator anim;
        public CameraHandler cameraHandler;
        AnimatorHandler animatorHandler;
        PlayerStats playerStats;
        PlayerMovement playerMovement;

        InteractableUI interactableUI;
        public GameObject interactableUIGameObject;
        public GameObject itemInteractableGameObject;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;
        public bool isUsingRightHand;
        public bool isUsingLeftHand;
        public bool isInvulnerable;

        private void Awake() {
            //cameraHandler = CameraHandler.singleton;
            inputHandler = GetComponent<InputHandler>();
            animatorHandler = GetComponentInChildren<AnimatorHandler>();
            anim = GetComponentInChildren<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
            playerStats = GetComponent<PlayerStats>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            isInvulnerable = anim.GetBool("isInvulnerable");
            anim.SetBool("isInAir", isInAir);
            anim.SetBool("isDead", playerStats.isDead);
            
            inputHandler.TickInput(delta);
            animatorHandler.canRotate = anim.GetBool("canRotate");
            playerMovement.HandleRollingAndSprinting(delta);
            playerMovement.HandleJumping();
            playerStats.RegenerateStamina();

            CheckForInteractableObject();
        }

        void FixedUpdate() {
            float delta = Time.fixedDeltaTime;

            playerMovement.HandleMovement(delta);
            playerMovement.HandleRotation(delta);
            playerMovement.HandleFalling(delta, playerMovement.moveDirection);
    
    /*
            if (cameraHandler != null) {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
            */
        }

        private void LateUpdate() {
            inputHandler.rollflag = false;
            //inputHandler.sprintflag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false;
            inputHandler.jump_Input = false;
            inputHandler.inventory_Input = false;
            isSprinting = inputHandler.b_Input;

            float delta = Time.deltaTime;
            if (cameraHandler != null) {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }

            if (isInAir) {
                playerMovement.inAirTimer = playerMovement.inAirTimer + Time.deltaTime;
            }
        }
        
        #region Player Interactions

        
        public void CheckForInteractableObject() {
            RaycastHit hit;

            if (Physics.SphereCast(transform.position, 0.4f, transform.forward, out hit, 1f, cameraHandler.ignoreLayers)) {
                if (hit.collider.tag == "Interactable") {
                    Interactable interactableObject = hit.collider.GetComponent<Interactable>();

                    if (interactableObject != null) {
                        string interactableText = interactableObject.interactableText;
                        interactableUI.interactableText.text = interactableText;
                        interactableUIGameObject.SetActive(true);

                        if (inputHandler.a_Input) {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
            else {
                if (interactableUIGameObject != null) {
                    interactableUIGameObject.SetActive(false);
                }

                if (itemInteractableGameObject != null && inputHandler.a_Input) {
                    itemInteractableGameObject.SetActive(false);
                }
            }
        }

        public void OpenChestInteraction(Transform playerStandingPosition) {
            playerMovement.rigidbody.velocity = Vector3.zero;
            transform.position = playerStandingPosition.transform.position;
            animatorHandler.PlayTargetAnimation("Open Chest", true);
        }
        
        #endregion
        
    }
}
