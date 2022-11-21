using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NC {
    public class PlayerManager : MonoBehaviour
    {
        InputHandler inputHandler;
        Animator anim;
        CameraHandler cameraHandler;
        PlayerMovement playerMovement;

        public bool isInteracting;

        [Header("Player Flags")]
        public bool isSprinting;
        public bool isInAir;
        public bool isGrounded;
        public bool canDoCombo;

        // Start is called before the first frame update
        void Start()
        {
            cameraHandler = CameraHandler.singleton;
            inputHandler = GetComponent<InputHandler>();
            anim = GetComponentInChildren<Animator>();
            playerMovement = GetComponent<PlayerMovement>();
        }

        // Update is called once per frame
        void Update()
        {
            float delta = Time.deltaTime;
            isInteracting = anim.GetBool("isInteracting");
            canDoCombo = anim.GetBool("canDoCombo");
            
            inputHandler.TickInput(delta);
            playerMovement.HandleMovement(delta);
            playerMovement.HandleRollingAndSprinting(delta);
            playerMovement.HandleFalling(delta, playerMovement.moveDirection);

            CheckForInteractableObject();
        }

        void FixedUpdate() {
            float delta = Time.fixedDeltaTime;

            if (cameraHandler != null) {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate() {
            inputHandler.rollflag = false;
            inputHandler.sprintflag = false;
            inputHandler.rb_Input = false;
            inputHandler.rt_Input = false;
            inputHandler.d_Pad_Up = false;
            inputHandler.d_Pad_Down = false;
            inputHandler.d_Pad_Left = false;
            inputHandler.d_Pad_Right = false;
            inputHandler.a_Input = false;
            isSprinting = inputHandler.b_Input;

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

                        if (inputHandler.a_Input) {
                            hit.collider.GetComponent<Interactable>().Interact(this);
                        }
                    }
                }
            }
        }
    }
}
