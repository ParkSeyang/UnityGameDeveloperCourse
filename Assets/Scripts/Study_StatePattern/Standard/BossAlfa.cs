using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Study_Camera.Study_StatePattern.Standard
{
    public class BossAlfa : MonoBehaviour
    {
        private static readonly int BREATH = Animator.StringToHash("Breath");
        private static readonly int ATTACK = Animator.StringToHash("Attack");
    
        [SerializeField] private Transform firePoint;
        [SerializeField] private ParticleSystem breathEffect; 
    
        [SerializeField] private Collider ScratchCollider;
        [SerializeField] private Collider BreathCollider;
        [SerializeField] private AnimEventReceiver AnimEventReciver;
    
        private Animator Animator { get; set; }
    
        private Dictionary<Type, BaseState> States { get; set; }
        private BaseState CurrentState { get; set; }
        private BaseState DefaultState { get; set; }
        
        private void Awake()
        {
            Animator = GetComponent<Animator>();
            ScratchCollider.enabled = false;
            BreathCollider.enabled = false;
            
            States = new Dictionary<Type, BaseState>();
            States.Add(typeof(IdleState), new IdleState());

            int randNum = Random.Range(0, 2);
            if (randNum == 0) States.Add(typeof(BreathState), new BreathState());
            else States.Add(typeof(ScratchState), new ScratchState());
            
            States.Add(typeof(DeadState), new DeadState());
            
            DefaultState = States[typeof(IdleState)];

            BaseState.StateControllerParameter param =  new BaseState.StateControllerParameter();
            param.bossAlfa = this;
            param.BreathCollider = BreathCollider;
            param.ScratchCollider = ScratchCollider;
            param.breathEffect = breathEffect;
            param.firePoint = firePoint;
            param.bossAnimator = Animator;
            param.AnimEventReceiver = AnimEventReciver;
            
            foreach (var state in States.Values)
            {
                state.Initialize(param);
            }

            ChangeState<IdleState>();
        }

        private void Update()
        {
            CurrentState.UpdateState(Time.deltaTime);
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
    }
}