using System;
using System.Collections.Generic;
using UnityEngine;

namespace Study_Camera.CombatSystem
{
    // 전투시스템을 구성하는 단위를 생각을 해봅시다
    // 스탯이라는 필드가 필요함 (전투 시스템을 거쳐서 변화할 데이터들)
    // HitBox라고 불리는 공격의 판정을 할 수 있는 개체가 필요합니다.
    // HurtBox라고 불리는 충돌 검사 개체가 필요합니다.
    
    // HitBox => CombatSystem => Stat
    // HitBox : 감지/판정 하는 역할
    // HurtBox : 감지/판정을 할 수 있게 해주는 역할
    // Stat : 전투 정보를 반영하여 수정해야할 데이터
    // CombatSystem : 중재자, 매개자(?)
    
    public class CombatSystem : SingletonBase<CombatSystem>
    {
        public class Events
        {
            // 데미지를 입었을때
            public Action<CombatEvent> OnSomeoneTakeDamage;
            
            // 누군가가 회복했을때
            public Action<CombatEvent> OnSomeoneHeal;
        }

        public Events Subscribe { get; private set; } = new Events();

        private const int EVENT_PROCESS_PER_FRAME = 10;
        
        private Dictionary<Collider, IHitTargetPart> HitTargetDic { get; set; }
        private Queue<CombatEvent> CombatEventQueue { get; set; }

        protected override void Awake()
        {
            base.Awake();
            HitTargetDic = new Dictionary<Collider, IHitTargetPart>();
            CombatEventQueue = new Queue<CombatEvent>();
        }

        private void Update()
        {
            for (int i = 0; i < EVENT_PROCESS_PER_FRAME; i++)
            {
                if (CombatEventQueue.Count == 0) break;
                var combatEvent = CombatEventQueue.Dequeue();
                HandleCombatEvent(combatEvent);
            }
        }

        public void AddCombatEvent(CombatEvent combatEvent)
        {
            CombatEventQueue.Enqueue(combatEvent);
            //Debug.Log("Pass AddCombatEvent");
        }

        private void HandleCombatEvent(CombatEvent combatEvent)
        {
            //Debug.Log("Pass AddCombatEvent");
            combatEvent.Receiver.TakeDamage(combatEvent.Damage);
            Subscribe.OnSomeoneTakeDamage?.Invoke(combatEvent);
            
        }

        #region IHitTargetPart Management Methods

        public void AddHitTarget(Collider col, IHitTargetPart hitTarget)
        {
            HitTargetDic.TryAdd(col, hitTarget);
        }

        public void RemoveHitTarget(Collider col, IHitTargetPart hitTarget)
        {
            if (HitTargetDic.ContainsKey(col) == false) return;
            HitTargetDic.Remove(col);
        }

        public bool HasHitTarget(Collider collider)
        {
            return HitTargetDic.ContainsKey(collider);
        }

        // 먼저 HasHurtBox로 조회해 본 후 호출하세요. Null 처리 해놓지 않았습니다
        public IHitTargetPart GetHitTarget(Collider collider)
        {
            return HitTargetDic[collider];
        }
        
        // 아래처럼 해도 무방합니다. 다만 Null을 반환할 수 있을 경우에는 함수명에 표현해주세여
        // public HurtBox GetHurtBoxOrNull(Collider collider)
        // {
        //     if(HurtBoxDic.ContainsKey(collider)) return HurtBoxDic[collider];
        //     return null;
        // }
        
        #endregion
    }
    
    
    
}