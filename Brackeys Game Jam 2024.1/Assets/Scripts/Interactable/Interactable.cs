using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    public abstract void OnSelected();
    public abstract void OffSelected();

    public abstract void Interact();
}
