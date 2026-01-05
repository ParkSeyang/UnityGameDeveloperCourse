using UnityEngine;

namespace Study_Camera.CombatSystem
{
    public struct HitInfo
    {
        public ICombatAgent receiver;
        public Vector3 position;
        public IHitTargetPart hitTarget;
        public int parameter; // Enum으로 hit의 형태를 정의를 미리 해놨을 것 같음
        
        public GameObject gameObject; //혹 몰라서 넣는것. 사실 안넣고 ICombatAgent를 더 넓게 잡아도 됩니다.
    }
    
    public interface ICombatAgent
    {
        void TakeDamage(int damage);
        void OnHitDetected(HitInfo hitInfo);
    }
}