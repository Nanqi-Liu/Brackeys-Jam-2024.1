using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashlightHandler : MonoBehaviour
{
    public static FlashlightHandler instance;

    [SerializeField]
    private Light flashlight;

    public delegate void FlashlightDepleteAction();
    public static event FlashlightDepleteAction OnFlashlightDeplete;

    public float maxEnergy = 10;
    [SerializeField]
    float batteryEnergy = 5;
    public float currEnergy;

    private bool depleted = false;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        currEnergy = maxEnergy;
        flashlight.enabled = true;
    }

    private void FixedUpdate()
    {
        float delta = Time.deltaTime;
        if (flashlight.enabled)
            currEnergy = Mathf.Max(0, currEnergy - delta);
        if (currEnergy <= 0 && flashlight.enabled && !depleted)
        {
            depleted = true;
            //flashlight.enabled = false;
            GameoverController.instance.StartGameover();
        }
    }
    public void ToggleFlashlight()
    {
        if (currEnergy > 0)
            flashlight.enabled = !flashlight.enabled;
        else
            flashlight.enabled = false;
    }

    public void BatteryRecharge()
    {
        currEnergy = Mathf.Min(currEnergy + batteryEnergy, maxEnergy);
    }

    public int CalcBatteryCell(int maxCell)
    {
        return (int)Mathf.Ceil(currEnergy / (maxEnergy / maxCell));
    }
}
