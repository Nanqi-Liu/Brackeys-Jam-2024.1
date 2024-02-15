using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Interactable
{
    Material material;
    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }
    public override void Interact()
    {
        FlashlightHandler.instance.BatteryRecharge();
        // TODO: Add a sound trigger
        Destroy(gameObject);
    }

    public override void OffSelected()
    {
        // Highlight
        material.SetInt("_Enable", 0);
    }

    public override void OnSelected()
    {
        // De-highlight
        material.SetInt("_Enable", 1);
    }
}
