using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static PlayerControls inputActions;
    public static event Action<InputActionMap> actionMapChange;
    private void Awake()
    {
        inputActions = new PlayerControls();
        Debug.Log("Init");
    }
    // Start is called before the first frame update
    void Start()
    {
        ToggleActionMap(inputActions.Player);
    }

    public static void ToggleActionMap(InputActionMap actionMap)
    {
        if (actionMap.enabled)
            return;

        inputActions.Disable();
        actionMapChange?.Invoke(actionMap);
        actionMap.Enable();
    }
}
