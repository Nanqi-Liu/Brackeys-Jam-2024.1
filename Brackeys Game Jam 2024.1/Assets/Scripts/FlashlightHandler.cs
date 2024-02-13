using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightHandler : MonoBehaviour
{
    [SerializeField]
    private Light flashlight;
    public void ToggleFlashlight()
    {
        flashlight.enabled = !flashlight.enabled;
    }
}
