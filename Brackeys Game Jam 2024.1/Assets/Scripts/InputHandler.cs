using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;

    public bool a_Input;
    public bool interactFlag = false;
    public bool b_Input;
    public bool flashlightFlag = false;

    PlayerControls inputActions;

    Vector2 movementInput;
    Vector2 cameraInput;

    private void Start()
    {
        instance = this;
    }

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
        RestartGameInput(delta);
        if (!PauseMenu.IsPaused)
        {
            MoveInput(delta);
            InteractInput(delta);
            //FlashlightInput(delta);
        }
    }

    private void MoveInput(float delta)
    {
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseX = cameraInput.x;
        mouseY = cameraInput.y;
    }

    private void InteractInput(float delta)
    {
        //a_Input = inputActions.PlayerAction.Interact.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
        a_Input = inputActions.PlayerAction.Interact.triggered;
        if (a_Input)
        {
            //Debug.Log("interacting");
            interactFlag = true;
        }
    }

    private void FlashlightInput(float delta)
    {
        b_Input = inputActions.PlayerAction.Flashlight.triggered;
        if (b_Input)
        {
            flashlightFlag = true;
        }
    }

    private void RestartGameInput(float delta)
    {
        bool r_Input = inputActions.PlayerAction.Restart.triggered;
        if (r_Input)
        {
            if (PauseMenu.IsPaused)
            {
                PauseMenu.instance.Resume();
            }
            GameoverController.instance.StartGameover();
        }
    }
}