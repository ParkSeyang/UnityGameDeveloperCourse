using System;
using UnityEngine;

namespace Study_Camera.CameraController
{
    public abstract class CameraController : MonoBehaviour
    {
        [Header("Movement Settings")] 
        [SerializeField] protected float moveSpeed;
        [SerializeField] protected float runSpeed;

        [Header("Mouse Settings")] 
        [SerializeField] protected float horizontalSensitivity = 1.0f;
        [SerializeField] protected float verticalSensitivity = 1.0f;
        
        protected virtual void Start()
        {
            LockCursor();
        }

        protected virtual void Update()
        {
            if(Input.GetKeyDown(KeyCode.Alpha2)) LockCursor();
            UpdatePosition();
            UpdateRotation();
        }

        protected virtual void UpdatePosition()
        {
            Vector2 inputAxis = 
                new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
            Vector3 forward = transform.forward * inputAxis.y;
            Vector3 right = transform.right * inputAxis.x;

            ApplyMoveVector(forward + right);
        }

        protected void ApplyMoveVector(Vector3 direction)
        {
            Vector3 moveVector = direction.normalized;
            float applySpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
            transform.position += moveVector * applySpeed * Time.deltaTime;
        }

        protected abstract void UpdateRotation();

        protected void LockCursor()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}