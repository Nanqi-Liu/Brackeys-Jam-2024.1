using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    Transform cameraObject;

    public GameObject targetObject;
    public Interactable targetInteractable;

    private int layerInteractables;

    [Header("Settings")]
    [SerializeField]
    float maxDistance = 2f;
    [SerializeField]
    LayerMask ignoreLayer;
    // Start is called before the first frame update
    void Start()
    {
        cameraObject = Camera.main.transform;
        layerInteractables = LayerMask.NameToLayer("Interactable");
    }

    private void FixedUpdate()
    {
        // Cast a ray from camera to check if selected interact
        RaycastHit hit;
        if (Physics.Raycast(cameraObject.transform.position, cameraObject.forward, out hit, maxDistance, ignoreLayer) && 
            hit.transform.gameObject.layer == layerInteractables)
        {
            // Do something to hit.transform.gameobject

            // If it hits a different interactable object
            if (targetObject != hit.transform.gameObject)
            {
                // Replace target with the new object
                targetObject = hit.transform.gameObject;

                // Deselect interactable
                if (targetInteractable != null)
                {
                    targetInteractable.OffSelected();
                }

                targetInteractable = targetObject.GetComponent<Interactable>();
                targetInteractable.OnSelected();
            }
        }
        else
        {
            // Do something when it's not interactable

            // Deselect interactable
            if (targetInteractable != null)
            {
                targetInteractable.OffSelected();
            }

            targetInteractable = null;
            targetObject = null;
        }
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
            Gizmos.DrawRay(cameraObject.transform.position, cameraObject.forward);
    }
}
