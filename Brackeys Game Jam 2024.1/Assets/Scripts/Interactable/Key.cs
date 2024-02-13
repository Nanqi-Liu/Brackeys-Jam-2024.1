using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Interactable
{
    [SerializeField]
    Door targetDoor;

    Material material;

    private void Start()
    {
        material = GetComponent<Renderer>().material;
    }

    public override void Interact()
    {
        // Pickup the key
        targetDoor.hasKey += 1;
        AudioManager.instance.PlaySound("KeyPickup");
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
