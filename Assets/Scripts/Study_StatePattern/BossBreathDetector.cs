using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

namespace Study_Camera.CombatSystem
{
    public class BossBreathDetector : MonoBehaviour, IHitDetector
    {
        public ICombatAgent Owner { get; private set; }
        
        [field: SerializeField]  public LayerMask DetectionLayer { get; private set; }
        
        [field: SerializeField]  private Collider Collider { get; set; }

        private HashSet<IHitTargetPart> hitList = new HashSet<IHitTargetPart>();
        
        public void Initialize(ICombatAgent owner)
        {
            Owner = owner;
        }

        public void EnableDetection()
        {
            Collider.enabled = true;
        }

        public void DisableDetection()
        {
            Collider.enabled = false;
            hitList.Clear();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (CombatSystem.Instance.HasHitTarget(other) == false) return;
                
            HitInfo hitInfo = new HitInfo();
            hitInfo.hitTarget = CombatSystem.Instance.GetHitTarget(other);
            hitInfo.parameter = hitList.Contains(hitInfo.hitTarget) ? 2 : 1; 
            hitInfo.receiver = hitInfo.hitTarget.Owner;
            hitInfo.gameObject = other.gameObject;
            hitInfo.position = other.ClosestPoint(transform.position);
            // .ClosestPoint(Vector3 position)
            // 해당 함수는 position위치에서 Collider의 가장 가까운 표면 점을 반환해 주는 함수 
                
            Owner.OnHitDetected(hitInfo);
            
            hitList.Add(hitInfo.hitTarget);
        }
    }
}