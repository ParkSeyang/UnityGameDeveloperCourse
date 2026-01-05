using UnityEngine;

namespace Study_Camera.Study_StatePattern.Standard
{
    public class ScratchState : BaseState
    {
        private static readonly int ATTACK = Animator.StringToHash("Attack");
        
        private Collider ScratchCollider;
        private AnimEventReceiver receiver;
        
        public override void Initialize(StateControllerParameter parameter)
        {
            base.Initialize(parameter);
            ScratchCollider = parameter.ScratchCollider;
            receiver = parameter.AnimEventReceiver;
        }

        public override void EnterState()
        {
            ScratchCollider.enabled = true;
            BossAnimator.SetTrigger(ATTACK);

            receiver.OnAnimationTriggerReceived += OnTriggeredEvent;
        }

        public override void UpdateState(float timeDelta)
        {
            return;
        }

        public override void ExitState()
        {
            ScratchCollider.enabled = false;
            receiver.OnAnimationTriggerReceived -= OnTriggeredEvent;
        }
        
        private const string ATP_COLLIDER_ON = "Attack_Collider_On";
        private const string ATP_COLLIDER_OFF = "Attack_Collider_Off";
        private const string ATP_ANIM_END = "Attack_End";
        
        public void OnTriggeredEvent(string str)
        {
            switch (str)
            {
                case ATP_COLLIDER_ON:
                    ScratchCollider.enabled = true;
                    break;
                case ATP_COLLIDER_OFF:
                    ScratchCollider.enabled = false;
                    break;
                case ATP_ANIM_END:
                    BossAlfa.ChangeState<IdleState>();
                    break;
                default:
                    break;
            }
        }
    }
}