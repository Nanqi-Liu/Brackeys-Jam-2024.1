using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputHandler : MonoBehaviour
{
    public static InputHandler instance;
    public float horizontal;
    public float vertical;
    public float moveAmount;
    public float mouseX;
    public float mouseY;
    public float gamepadX;
    public float gamepadY;

    public bool a_Input;
    public bool interactFlag = false;
    public bool b_Input;
    public bool flashlightFlag = false;

    private InputAction _moveInputAction, _mouseCameraInputAction, _gamepadCameraInputAction;

    Vector2 movementInput;
    Vector2 mouseCameraInput;
    Vector2 gamepadCameraInput;

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
        _moveInputAction = InputManager.inputActions.Player.Movement;
        _moveInputAction.Enable();

        _mouseCameraInputAction = InputManager.inputActions.Player.MouseCamera;
        _mouseCameraInputAction.Enable();

        _gamepadCameraInputAction = InputManager.inputActions.Player.GamepadCamera;
        _gamepadCameraInputAction.Enable();

        InputManager.inputActions.Player.Interact.performed += OnInteract;
        InputManager.inputActions.Player.Interact.Enable();

        InputManager.inputActions.Player.Flashlight.performed += OnFlashlight;
        InputManager.inputActions.Player.Flashlight.Enable();

        InputManager.inputActions.Player.Pause.performed += OnPause;
        InputManager.inputActions.Player.Pause.Enable();
    }

    private void OnDisable()
    {
        _moveInputAction.Disable();

        _mouseCameraInputAction.Disable();

        _gamepadCameraInputAction.Disable();

        InputManager.inputActions.Player.Interact.performed -= OnInteract;

        InputManager.inputActions.Player.Flashlight.performed -= OnFlashlight;

        InputManager.inputActions.Player.Restart.Disable();
    }

    public void TickInput(float delta)
    {
        //RestartGameInput(delta);
        MoveInput(delta);
        //InteractInput(delta);
        //FlashlightInput(delta);
    }

    private void MoveInput(float delta)
    {
        movementInput = _moveInputAction.ReadValue<Vector2>();
        horizontal = movementInput.x;
        vertical = movementInput.y;
        moveAmount = Mathf.Clamp01(Mathf.Abs(horizontal) + Mathf.Abs(vertical));
        mouseCameraInput = _mouseCameraInputAction.ReadValue<Vector2>();
        gamepadCameraInput = _gamepadCameraInputAction.ReadValue<Vector2>();
        mouseX = mouseCameraInput.x;
        mouseY = mouseCameraInput.y;
        gamepadX = gamepadCameraInput.x;
        gamepadY = gamepadCameraInput.y;
    }

    private void OnInteract(InputAction.CallbackContext context)
    {
        interactFlag = true;
    }

    //private void InteractInput(float delta)
    //{
    //    //a_Input = inputActions.PlayerAction.Interact.phase == UnityEngine.InputSystem.InputActionPhase.Performed;
    //    a_Input = inputActions.PlayerAction.Interact.triggered;
    //    if (a_Input)
    //    {
    //        //Debug.Log("interacting");
    //        interactFlag = true;
    //    }
    //}

    private void OnFlashlight(InputAction.CallbackContext context)
    {
        flashlightFlag = true;
    }

    //private void FlashlightInput(float delta)
    //{
    //    b_Input = inputActions.PlayerAction.Flashlight.triggered;
    //    if (b_Input)
    //    {
    //        flashlightFlag = true;
    //    }
    //}

    //private void RestartGameInput(float delta)
    //{
    //    bool r_Input = inputActions.PlayerAction.Restart.triggered;
    //    if (r_Input)
    //    {
    //        if (PauseMenu.IsPaused)
    //        {
    //            PauseMenu.instance.Resume();
    //        }
    //        GameoverController.instance.StartGameover();
    //    }
    //}

    private void OnPause(InputAction.CallbackContext context)
    {
        PauseMenu.instance.Pause();
        InputManager.ToggleActionMap(InputManager.inputActions.Pause);
    }

}