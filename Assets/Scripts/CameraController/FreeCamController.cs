using UnityEngine;

namespace Study_Camera.CameraController
{
    public class FreeCamController : CameraController
    {
        [SerializeField] protected float maxAngleX = 90;
        [SerializeField] protected float minAngleX = -90;

        private Vector2 currentAngle = Vector2.zero; 
        
        protected override void UpdatePosition()
        {
            Vector2 inputAxis = 
                new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
            Vector3 forward = transform.forward * inputAxis.y;
            Vector3 right = transform.right * inputAxis.x;
            
            inputAxis.y += Input.GetKey(KeyCode.Q) ? 1 : 0;
            inputAxis.y += Input.GetKey(KeyCode.E) ? -1 : 0;
            Vector3 up = transform.up * inputAxis.y;

            ApplyMoveVector((forward + right + up));
        }

        protected override void UpdateRotation()
        {
            Vector2 mouseInput = new Vector2(
                Input.GetAxis("Mouse X") * horizontalSensitivity, 
                Input.GetAxis("Mouse Y") * verticalSensitivity);

            currentAngle.x += mouseInput.x;
            currentAngle.y -= mouseInput.y;
            
            currentAngle.x = Mathf.Clamp(currentAngle.x, minAngleX, maxAngleX);
            transform.localRotation = Quaternion.Euler(currentAngle.x, currentAngle.y, 0);
        }
    }

}