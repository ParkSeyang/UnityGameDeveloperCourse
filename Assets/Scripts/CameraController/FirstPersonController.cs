using UnityEngine;

namespace Study_Camera.CameraController
{
    public class FirstPersonController : CameraController
    {
        [SerializeField] private Transform cameraTransform;
        
        protected override void UpdateRotation()
        {
            float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
            float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
            
            transform.Rotate(Vector3.up * mouseX);
            cameraTransform.Rotate(Vector3.right * (-mouseY));
        }
    }
}