using System;
using UnityEngine;

public class StudyFirstPersonController : MonoBehaviour
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

    //과제 힌트
    [SerializeField] private Transform cameraTransform; 
    
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
    }

    //이동은 자기 local 기준 좌표계로 이동
    private void UpdatePosition()
    {
        // .GetAxis와 .GetAxisRaw의 차이는 보정(입력에 따른 보간값)이 적용되느냐 차이 입니다.
        // GetAxis의 경우 사용자 입력이 있을때는 1에서 시작해서, 없을때는 0까지 천천히 감속되어 적용됩니다.
        // (GetAxis는 서서히 떨어짐)
        // GetAxisRaw의 경우 사용자의 입력이 있을때는 무조건 1, 없을때는 바로 0으로 적용됩니다.
        
        // 사용자의 입력 받기
        Vector2 inputAxis = 
            new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        // 현재 transform의 올바른 좌표계로 바꿔주기
        Vector3 forward = transform.forward * inputAxis.y;
        Vector3 right = transform.right * inputAxis.x;
        
        //위의 forward와 right를 합치면?
        //입력에 따라 캐릭터가 이동할 방향벡터가 나옵니다.
        //(이제부터 moveVector라고 부르겠습니다)
        Vector3 moveVector = (forward + right).normalized;
        float applySpeed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : moveSpeed;
        transform.position += moveVector * applySpeed * Time.deltaTime;
    }

    //회전도 자기 local 기준으로 회전.
    private void UpdateRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * horizontalSensitivity;
        
        //Rotate함수는 특정 축(Axis)으로 Euler를 더해줌
        //(나중에는 Quaternion단위로 사용함. 사실은 더해주는게 아님. 곱해주는거임)
        transform.Rotate(Vector3.up * mouseX);
        
        //아래 코드가 아님은 알았습니다.
        // float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
        // transform.Rotate(Vector3.right * (-mouseY));
        
        // Y축 회전은 플레이어의 회전에 반영하되,
        // X축 회전은 카메라 회적에 반영을 해야합니다.
        float mouseY = Input.GetAxis("Mouse Y") * verticalSensitivity;
        cameraTransform.Rotate(Vector3.right * (-mouseY));
    }
}
