using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    InputHandler inputHandler;
    InteractionHandler interactionHandler;
    FlashlightHandler flashlightHandler;
    // Start is called before the first frame update
    private void Start()
    {
        inputHandler = GetComponent<InputHandler>();
        interactionHandler = GetComponent<InteractionHandler>();
        flashlightHandler = GetComponent<FlashlightHandler>();
    }

    private void FixedUpdate()
    {
        HandleInteraction();
        HandleFlashlight();
    }

    #region Interaction

    public void HandleInteraction()
    {
        if (inputHandler.interactFlag)
        {
            inputHandler.interactFlag = false;

            if (interactionHandler.targetInteractable != null)
            {
                interactionHandler.targetInteractable.Interact();
            }
        }
    }

    #endregion

    #region Flashlight

    private void HandleFlashlight()
    {
        if (inputHandler.flashlightFlag)
        {
            inputHandler.flashlightFlag = false;

            flashlightHandler.ToggleFlashlight();
        }
    }

    #endregion
}
