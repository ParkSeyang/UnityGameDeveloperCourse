using UnityEngine;

namespace Study_Camera.Study_StatePattern.Standard
{
    public class BreathState : BaseState
    {
        private static readonly int BREATH = Animator.StringToHash("Breath");
        
        private bool isBreathing = false;
        private ParticleSystem breathEffect;
        private Collider BreathCollider;
        private Transform firePoint;
        private AnimEventReceiver receiver;

        public override void Initialize(StateControllerParameter parameter)
        {
            base.Initialize(parameter);
            breathEffect = parameter.breathEffect;
            BreathCollider = parameter.BreathCollider;
            firePoint = parameter.firePoint;
            receiver = parameter.AnimEventReceiver;
        }

        public override void EnterState()
        {
            breathEffect.gameObject.SetActive(true);
            breathEffect.Play();
            isBreathing = true;
            

            receiver.OnAnimationTriggerReceived += OnTriggeredEvent;
            
            BossAnimator.SetTrigger(BREATH);
        }

        public override void UpdateState(float timeDelta)
        {
            if (isBreathing)
            {
                breathEffect.transform.position = firePoint.position;
            }
        }

        public override void ExitState()
        {
            isBreathing = false;
            receiver.OnAnimationTriggerReceived -= OnTriggeredEvent;
        }

        private const string ATP_COLLIDER_ON = "Breath_Collider_On";
        private const string ATP_COLLIDER_OFF = "Breath_Collider_Off";
        private const string ATP_ANIM_END = "Breath_End";
        
        public void OnTriggeredEvent(string str)
        {
            switch (str)
            {
                case ATP_COLLIDER_ON:
                    BreathCollider.enabled = true;
                    break;
                case ATP_COLLIDER_OFF:
                    BreathCollider.enabled = false;
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