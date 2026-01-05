using UnityEngine;

public class Study_ThirdPersonController : MonoBehaviour
{
    // WASD로 좌표이동
    // L-Shift로 달리기
    // 마우스로 회전
    [Header("Movement Settings")] 
    [SerializeField] private float moveSpeed;
    [SerializeField] private float runSpeed;

    [Header("Mouse Settings")] 
    [SerializeField] private float horizontalSensitivity = 1.0f;
    [SerializeField] private float verticalSensitivity = 1.0f;

    [SerializeField] private Transform cameraTarget;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private Transform headPivotTransform;
    
    private float angleX = 0.0f;
    private float angleY = 0.0f;
    [SerializeField] private float maxAngleX = 90;
    [SerializeField] private float minAngleX = -90;
    
    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        UpdatePosition();
        UpdateRotation();
        UpdateCameraRotation();
    }

    //이동은 자기 local 기준 좌표계로 이동
    private void UpdatePosition()
    {
        Vector2 inputAxis = 
            new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        Vector3 forward = transform.forward * inputAxis.y;
        Vector3 right = transform.right * inputAxis.x;
        
        Vector3 moveVector = (forward + right).normalized;
        float applySpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        transform.position += moveVector * applySpeed * Time.deltaTime;
    }
    
    //회전도 자기 local 기준으로 회전.
    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;

        angleX -= mouseY;
        angleX = Mathf.Clamp(angleX, minAngleX, maxAngleX);

        if (Input.GetKey(KeyCode.LeftAlt))
        {
            angleY += mouseX;
        }
        else
        {
            transform.Rotate(Vector3.up, mouseX);
            angleY = 0.0f;
        }
        
        headPivotTransform.localRotation = Quaternion.Euler(angleX, angleY, 0);
    }

    private void UpdateCameraRotation()
    {
        // Transform Method
        //.LookAt() 매개변수로 주어진 Transform을 바라보도록 합니다
        cameraTransform.LookAt(cameraTarget);
    }
}