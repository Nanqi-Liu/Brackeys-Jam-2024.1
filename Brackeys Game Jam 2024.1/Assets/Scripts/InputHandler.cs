using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PA
{
    public class InputHandler : MonoBehaviour
    {
        public float horizontal;
        public float vertical;
        public float moveAmount;
        public float mouseX;
        public float mouseY;

        public bool a_Input;
        public bool interactFlag = false;

        PlayerControls inputActions;

        Vector2 movementInput;
        Vector2 cameraInput;

        private void FixedUpdate()
        {
            float delta = Time.fixedDeltaTime;
        }

        public void OnEnable()
        {
            if (inputActions == null)
            {
                inputActions = new PlayerControls();
                inputActions.PlayerMovement.Movement.performed += inputActions => movementInput = inputActions.ReadValue<Vector2>();
                inputActions.PlayerMovement.Camera.performed += i => cameraInput = i.ReadValue<Vector2>();
            }

            inputActions.Enable();
        }

        private void OnDisable()
        {
            inputActions.Disable();
        }

        public void TickInput(float delta)
        {
            MoveInput(delta);
            HandleInteractInput(delta);
        }

        private void MoveInput(float delta)
        {
            horizontal = movementInput.x;
            vertical = movementInput.y;
            moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            mouseX = cameraInput.x;
            mouseY = cameraInput.y;
        }

        private void HandleInteractInput(float delta)
        {
            //a_Input = inputActions.PlayerAction.Interact.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
            a_Input = inputActions.PlayerAction.Interact.triggered;
            if (a_Input)
            {
                //Debug.Log("interacting");
                interactFlag = true;
            }
        }
    }
}
