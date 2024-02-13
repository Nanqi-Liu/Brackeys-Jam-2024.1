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
        throw new System.NotImplementedException();
    }

    public override void OffSelected()
    {
        material.SetInt("_Enable", 0);
    }

    public override void OnSelected()
    {
        material.SetInt("_Enable", 1);
    }
}
