using System.Collections.Generic;
using UnityEngine;

namespace Study_Camera.CombatSystem
{
    public class BossScratchDetector : MonoBehaviour, IHitDetector
    {
        public ICombatAgent Owner { get; private set; }
        
        [field: SerializeField]  public LayerMask DetectionLayer { get; private set; }
        
        [field: SerializeField] private Collider Collider { get; set; }
        private HashSet<IHitTargetPart> hitList = new HashSet<IHitTargetPart>();

        private int enableCount = 0;
        
        public void Initialize(ICombatAgent owner)
        {
            Owner = owner;
        }

        public void EnableDetection()
        {
            Collider.enabled = true;
            enableCount++;
        }

        public void DisableDetection()
        {
            Collider.enabled = false;
            if(enableCount % 2 == 0) hitList.Clear();  
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
                
            Owner.OnHitDetected(hitInfo);
            
            hitList.Add(hitInfo.hitTarget);
        }
    }
}