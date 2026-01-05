using System;
using System.Collections.Generic;
using Study_Camera.CombatSystem;
using UnityEngine;
using Random = System.Random;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public class BossAlfa : MonoBehaviour, ICombatAgent
    {
        private Dictionary<Type, BaseState> States { get; set; }
        private BaseState CurrentState { get; set; }
        private BaseState DefaultState { get; set; }
        
        private void Awake()
        {
            States = new Dictionary<Type, BaseState>();
            
            var allStates = GetComponentsInChildren<BaseState>();
            foreach (var state in allStates)
            {
                States.Add(state.GetType(), state);
                state.gameObject.SetActive(false);
            }

            DefaultState = States[typeof(IdleState)];
            ChangeState<IdleState>();
        }

        private void Start()
        {
            // .GetComponents 계열을 하이어라키상 활성화 되어있는 녀석들만 검색 가능합니다.
            // 비 활성화 개체까지 검색을 하려면 .GetComponents(includeInactive : true) 형태로
            // 활용해야 합니다
            
            var allDetector = GetComponentsInChildren<IHitDetector>(true);
            foreach (var detector in allDetector) detector.Initialize(this);
            
            var allHurtBox = GetComponentsInChildren<HurtBox>(true);
            foreach (var hurtBox in allHurtBox) hurtBox.Initialize(this);
        }

        public void ChangeState<T>() where T : BaseState
        {
            var prevState = CurrentState;
            prevState?.ExitState();

            CurrentState = DefaultState;
            if (States.ContainsKey(typeof(T))) CurrentState = States[typeof(T)];
            CurrentState.EnterState();
            Debug.Log($"{prevState?.GetType().Name} changed to {CurrentState.GetType().Name}");
        }

        
        public void TakeDamage(int damage)
        {
            
        }

        public void OnHitDetected(HitInfo hitInfo)
        {
            CombatEvent @event = new CombatEvent();
            @event.Sender = this;
            @event.Receiver = hitInfo.receiver;
            @event.Damage = hitInfo.parameter == 2 ? 100 : 10;
            @event.HitInfo = hitInfo;
            
            CombatSystem.CombatSystem.Instance.AddCombatEvent(@event);
        }
    }
}