using System;
using UnityEngine;

namespace Study_Camera.Study_ObjectPool
{
    public class Study_ObjectPool : MonoBehaviour
    {
        // Study_ObjectPool은 09. State-Observer-ObjectPool씬에서
        // 인게임 시스템이나 메인시스템을 대체하는 역할의 컴포넌트 입니다

        private CombatEventBinder combatEventBinder = new CombatEventBinder();

        private void OnEnable()
        {
            combatEventBinder.Enable();
        }

        private void OnDisable()
        {
            combatEventBinder.Disable();
        }
    }
}