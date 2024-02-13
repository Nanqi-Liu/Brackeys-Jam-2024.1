using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PA
{
    public class PlayerLocomotion : MonoBehaviour
    {
        Transform cameraObject;
        InputHandler inputHandler;
        Vector3 moveDirection;

        InteractionHandler interactionHandler;

        [HideInInspector]
        public Transform myTransform;

        public new Rigidbody rigidbody;
        public GameObject normalCamera;

        [Header("Stats")]
        [SerializeField]
        float movementSpeed = 5f;

        [SerializeField]
        float lookSpeed = 0.1f;
        [SerializeField]
        float pivotSpeed = 0.03f;

        private float lookAngle;
        private float pivotAngle;
        public float minimumPivot = -85;
        public float maximumPivot = 85;

        private void Start()
        {
            rigidbody = GetComponent<Rigidbody>();
            inputHandler = GetComponent<InputHandler>();
            interactionHandler = GetComponent<InteractionHandler>();
            cameraObject = Camera.main.transform;
            myTransform = transform;
        }

        public void Update()
        {
            float delta = Time.deltaTime;

            inputHandler.TickInput(delta);
        }

        public void FixedUpdate()
        {
            float delta = Time.deltaTime;
            HandleMovement(delta);
            HandleCameraRotation(delta, inputHandler.mouseX, inputHandler.mouseY);
            HandleInteraction(delta);
        }

        #region Movement
        Vector3 normalVector;
        Vector3 targetPosition;

        public void HandleCameraRotation(float delta, float mouseXInput, float mouseYInput)
        {
            lookAngle += (mouseXInput * lookSpeed) / delta;
            pivotAngle -= (mouseYInput * pivotSpeed) / delta;
            pivotAngle = Mathf.Clamp(pivotAngle, minimumPivot, maximumPivot);

            Vector3 rotation = Vector3.zero;
            rotation.y = lookAngle;
            Quaternion targetRotation = Quaternion.Euler(rotation);
            myTransform.rotation = targetRotation;

            rotation = Vector3.zero;
            rotation.x = pivotAngle;

            targetRotation = Quaternion.Euler(rotation);
            cameraObject.localRotation = targetRotation;
        }

        public void HandleMovement(float delta)
        {
            moveDirection = myTransform.forward * inputHandler.vertical;
            moveDirection += myTransform.right * inputHandler.horizontal;
            moveDirection.Normalize();
            moveDirection.y = 0;

            float speed = movementSpeed;
            moveDirection *= speed;

            Vector3 projectedVelocity = Vector3.ProjectOnPlane(moveDirection, normalVector);
            rigidbody.velocity = projectedVelocity;
        }

        #endregion

        #region Interaction

        public void HandleInteraction(float delta)
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
    }
}
