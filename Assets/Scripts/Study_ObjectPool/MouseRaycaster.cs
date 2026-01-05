using System;
using Study_Camera.CombatSystem;
using UnityEngine;

namespace Study_Camera.Study_ObjectPool
{
    public class MouseRaycaster : MonoBehaviour
    {
        //마우스 클릭, Camera, Raycast를 이용해서 보스와 충돌 검사를 수행하는 구조 짜보기.

        [SerializeField] private LayerMask targetLayerMask;
        private Camera cam;

        [Header("Test FX")] 
        [SerializeField] private FxObject hitFX;

        private ObjectPool hitFXPool = new ObjectPool();
        
        private void Start()
        {
            cam = Camera.main;
            
            hitFXPool.Initialize(hitFX, 3, 1, transform);
        }

        private void Update()
        {
            //Mouse0 = 왼쪽 클릭
            //Mouse1 = 오른쪽 클릭
            //Mouse2 = 휠 클릭
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                //.ScreenPointToRay(Vector3 position) position : 화면 기준 좌표
                Ray ray = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out RaycastHit hitInfo, 1000, targetLayerMask))
                {
                    var combatSystem = CombatSystem.CombatSystem.Instance;

                    if (combatSystem.HasHitTarget(hitInfo.collider))
                    {
                        HitInfo hitTargetInfo = new HitInfo();
                        hitTargetInfo.hitTarget = combatSystem.GetHitTarget(hitInfo.collider);
                        hitTargetInfo.parameter = 1; 
                        hitTargetInfo.receiver = hitTargetInfo.hitTarget.Owner;
                        hitTargetInfo.gameObject = hitInfo.collider.gameObject;
                        hitTargetInfo.position = hitInfo.point;
                        
                        CombatEvent @event = new CombatEvent();
                        @event.Sender = null;
                        @event.Receiver = hitTargetInfo.receiver;
                        @event.Damage = 10;
                        @event.HitInfo = hitTargetInfo;
            
                        CombatSystem.CombatSystem.Instance.AddCombatEvent(@event);
                    }
                }
            }
            
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                //.ScreenPointToRay(Vector3 position) position : 화면 기준 좌표
                Ray ray2 = cam.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray2, out RaycastHit hitInfo2, 1000, targetLayerMask))
                {
                    var combatSystem = CombatSystem.CombatSystem.Instance;

                    if (combatSystem.HasHitTarget(hitInfo2.collider))
                    {
                        HitInfo hitTargetInfo = new HitInfo();
                        hitTargetInfo.hitTarget = combatSystem.GetHitTarget(hitInfo2.collider);
                        hitTargetInfo.parameter = 2;
                        hitTargetInfo.receiver = hitTargetInfo.hitTarget.Owner;
                        hitTargetInfo.gameObject = hitInfo2.collider.gameObject;
                        hitTargetInfo.position = hitInfo2.point;

                        CombatEvent @event = new CombatEvent();
                        @event.Sender = null;
                        @event.Receiver = hitTargetInfo.receiver;
                        @event.Damage = 10;
                        @event.HitInfo = hitTargetInfo;

                        CombatSystem.CombatSystem.Instance.AddCombatEvent(@event);
                    }
                }
            }
        }
    }
}