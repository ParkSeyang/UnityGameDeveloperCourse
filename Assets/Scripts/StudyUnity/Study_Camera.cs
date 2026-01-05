using System;
using UnityEngine;

namespace Study_Camera
{
    public class Study_Camera : MonoBehaviour
    {
        // Camera의 핵심 프로퍼티(필드)
        // - Field Of View(FOV) / Size : 얼마나 넓은 시야를 가질지 결정합니다.
        // 값이 클수록 넓은 범위를 렌더링 합니다. => 더 많은 연산이 필요함.
        // (Sniper Rifle을 만들때 사용 가능)
        // - Clipping Planes : 카메라가 렌더링할 가장 가까운 거리와 가장 먼거리를
        // 설정합니다. Near, Far (원경의 거리제한을 통해 최적활때 씁니다)
        // - Culling Mask : 특정 레이어(Layer)의 오브젝트만 선택적으로
        // 렌더링 할 수 있게 합니다.
        // - Priority(Depth) : 씬에 여러대의 카메라가 있을때 렌더링의 순서를 결정합니다.
        // - ViewPortRect : 카메라의 최종 렌더링 결과를 화면에 어떤 영역에
        // 배치할지 결정합니다.

        private void Update()
        {
            if (Input.GetKey(KeyCode.Alpha1))
            {
                Camera.main.fieldOfView += 1f;
            }
            
            if (Input.GetKey(KeyCode.Alpha2))
            {
                Camera.main.nearClipPlane += 1f;
            }
            
            if (Input.GetKey(KeyCode.Alpha3))
            {
                Camera.main.farClipPlane += 1f;
            }
        }
    }
}