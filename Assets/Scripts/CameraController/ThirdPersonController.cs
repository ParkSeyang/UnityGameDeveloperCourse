using UnityEngine;

namespace Study_Camera.CameraController
{
    public class ThirdPersonController : CameraController
    {
        [SerializeField] private Transform cameraTarget;
        [SerializeField] private Transform cameraTransform;
        [SerializeField] private Transform headPivotTransform;
        
        [SerializeField] protected float maxAngleX = 90;
        [SerializeField] protected float minAngleX = -90;
        
        private Vector2 currentAngle = Vector2.zero; 
        
        protected override void Update()
        {
            base.Update();
            UpdateCameraRotation();
        }

        protected override void UpdateRotation()
        {
            Vector2 mouseInput = new Vector2(
                Input.GetAxis("Mouse X") * horizontalSensitivity, 
                Input.GetAxis("Mouse Y") * verticalSensitivity);
            
            currentAngle.x -= mouseInput.y;

            if (Input.GetKey(KeyCode.LeftAlt))
            {
                currentAngle.y += mouseInput.x;
            }
            else
            {
                transform.Rotate(Vector3.up, mouseInput.x);
                currentAngle.y = 0.0f;
            }
            
            currentAngle.x = Mathf.Clamp(currentAngle.x, minAngleX, maxAngleX);
            headPivotTransform.localRotation = Quaternion.Euler(currentAngle);
            
        }

        private void UpdateCameraRotation()
        {
            cameraTransform.LookAt(cameraTarget);
        }
    }
}