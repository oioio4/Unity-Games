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

            inputHandler.TickInput(delta);
            playerMovement.HandleMovement(delta);
            playerMovement.HandleRollingAndSprinting(delta);
            playerMovement.HandleFalling(delta, playerMovement.moveDirection);
        }

        private void FixedUpdate() {
            float delta= Time.fixedDeltaTime;

            if (cameraHandler != null) {
                cameraHandler.FollowTarget(delta);
                cameraHandler.HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            }
        }

        private void LateUpdate() {
            inputHandler.rollflag = false;
            inputHandler.sprintflag = false;
            isSprinting = inputHandler.b_Input;

            if (isInAir) {
                playerMovement.inAirTimer = playerMovement.inAirTimer + Time.deltaTime;
            }
        }
    }
}
