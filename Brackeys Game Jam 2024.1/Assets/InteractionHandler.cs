using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionHandler : MonoBehaviour
{
    Transform cameraObject;

    public GameObject targetObject;

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
        Debug.Log(layerInteractables);
    }

    private void FixedUpdate()
    {
        // Cast a ray from camera to check if selected interact
        RaycastHit hit;
        if (Physics.Raycast(cameraObject.transform.position, cameraObject.forward, out hit, maxDistance, ignoreLayer))
        {
            if (hit.transform.gameObject.layer == layerInteractables)
            {
                targetObject = hit.transform.gameObject;
                // Do something to hit.transform.gameobject
            }
            else
            {
                // Do something when it's not interactable
                targetObject = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        if(Application.isPlaying)
            Gizmos.DrawRay(cameraObject.transform.position, cameraObject.forward);
    }
}
