using System;
using UnityEngine;
using Obstacle;

public class Study_RigidBody : MonoBehaviour
{
    // Rigidbody
    // - 물리 기반의 움직임을 처리하는 가장 중요한 컴포넌트
    // - 운동을 시뮬레이션 할 때 사용함. (운동하는 물체에는 붙혀라 라는 뜻)
    // - 중력 적용 , 힘(충격) 적용, 충돌 감지 등의 역할을 수행합니다.
    
    // Rigidbody의 주요 프로퍼티
    // - mass : 물체의 질량을 설정
    // - drag(linearDamping) : 저항을 설정합니다(가속운동을 감속시키는 등의 역할)
    // - angularDrag : 회전 저항 (각속도를 감속시키는 역할)
    // - userGravity : 중력 적용 여부
    // - isKinematic : 물리엔진의 영향을 받는지 여부(true일 경우 영향을 받지 않음)
    //                주로 단순 충돌감지(트리거) 용으로 사요합니다.
    // - constrains : 위치 및 회전 축을 고정하는 옵션
    // - Interpolate & Collision Detection : 물리엔진에게 해당 개체가 얼마나 주요한
    //     물리 오브젝트인지 알려주는 용도로 쓰임. 최적화할때 중요함
    //     주 개체(탄환, 폭발범위, 판정)일 경우 Interpolate, Continuous 로 설정
    //     일반 개체(충돌 대상, 몬스터)일 경우 None, Discrete로 설정해서 사용
    
    // Rigidbody의 주요 프로퍼티(코드)
    // - linearVelocity : 가속도 값을 get/set 할수 있는 프로퍼티. 
    // - angularVelocity : 회전 가속도 값을 get/set 할수 있는 프로퍼티
    // PS : 둘다 Vector3.Zero를 대입하면 해당 rigidbody가 운동을 멈춥니다
    
    // Rigidbody의 주요 Methods(함수)
    // - .AddForce : 물체에 힘을 가하여 움직이게 합니다.
    //              ForceMode.Force : 지속적인 힘
    //              ForceMode.Impulse : 순간적인 힘
    // - .AddTorque : 물체에 회전력을 가합니다.
    //              바퀴가 굴러가거나 물체를 회전시킬때 사용합니다
    // - .MovePosition : 물체를 지정된 위치로 이동시킵니다.
    //                  텔레포트와 달리 이동 경로에 있는 물리 객체와의 충돌을
    //                  처리하며 이동합니다. transform.position을 수정하는것과 다름.
    //                  FixedUpdate에서 반영이 되며 주로 물리 업데이트에서 호출함
    // - Sleep() / WakeUp() : 물리 연산을 강제로 중지하거나 깨움.
    
    
    [SerializeField] private float power;
    private Rigidbody rb;
    private Vector3 startPosition;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        startPosition = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log($"rb.linearVelocity = {rb.linearVelocity}");
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //rb.AddForce(Vector3.forward * power, ForceMode.Impulse);
            //rb.AddTorque(Vector3.right * power, ForceMode.Impulse);
            rb.MovePosition(new Vector3(0.5f,0.5f, 18.5f));
            //transform.position = new Vector3(0.5f, 0.5f, 18.5f);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            transform.position = startPosition;
        }
    }
}
