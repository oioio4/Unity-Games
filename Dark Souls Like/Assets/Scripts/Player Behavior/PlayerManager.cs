using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC {
    public class PlayerManager : CharacterManager
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
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

        // Start is called before the first frame update
        void Start()
        {
            cameraHandler = CameraHandler.singleton;
            backStabCollider = GetComponentInChildren<BackStabCollider>();
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
            playerStats = GetComponent<PlayerStats>();
            interactableUI = FindObjectOfType<InteractableUI>();
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            anim.SetBool("isInAir", isInAir);
            isUsingRightHand = anim.GetBool("isUsingRightHand");
            isUsingLeftHand = anim.GetBool("isUsingLeftHand");
            isInvulnerable = anim.GetBool("isInvulnerable");
            
            inputHandler.TickInput(delta);
            playerMovement.HandleRollingAndSprinting(delta);
            playerMovement.HandleJumping();
            playerStats.RegenerateStamina();

            CheckForInteractableObject();
        }

        void FixedUpdate() {
            float delta = Time.fixedDeltaTime;

            playerMovement.HandleMovement(delta);
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
    }
}
