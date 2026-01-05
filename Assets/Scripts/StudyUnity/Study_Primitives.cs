using System;
using UnityEngine;

public class Study_Primitives : MonoBehaviour
{
    // Unity의 기본 도형은 Primitive 라고 부릅니다.
    // Unity Editor에서 우클릭 -> 3D Object를 통해서 생성 가능합니다.
    // 잘 쓰이진 않지만, 동적으로 생성을 할 수가 있습니다.
    // GameObject.CreatePrimitive(PrimitiveType.) 함수를 이용해 생성이 가능합니다.
    
    // 동적으로 생성되는 유니티 엔진의 컴포넌트들은 실제 메모리에 할당되는 속도가
    // 다를 수 있습니다. 따라서 컴포넌트의 순서가 보장되지 않을 수 있습니다.
    // (Prefab으로 패킹이 되어있으면 보장됩니다)
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject cylinder = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
            cylinder.transform.position = new Vector3(0, 5, 0);
            
            // 동적으로 컴포넌트를 붙히는 방법에대해서 알아봅시다.
            // GameObject의 정적 함수를 사용하여 동적으로 컴포넌트를 추가할 수 있습니다.
            // .AddComponent()
            // Unity에서 할 수 있는 가장 간단한 종속성 주입(D.I) 방법입니다.
            // 근데 사실, AddComponent보다는 DI 시스템을 구현해서 사용하는게 더 효율적임
            // Zenject, VContainer 등의 라이브러리들이 요즘 많이 사용됩니다.
            
            cylinder.AddComponent<Rigidbody>();
        }
    }
}
