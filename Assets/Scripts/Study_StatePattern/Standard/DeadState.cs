using UnityEngine;

namespace Study_Camera.Study_StatePattern.Standard
{
    public class DeadState : BaseState
    {
        private static readonly int DEAD = Animator.StringToHash("Dead");

        public override void EnterState()
        {
            BossAnimator.SetTrigger(DEAD);
        }

        public override void UpdateState(float timeDelta)
        {
            
        }

        public override void ExitState()
        {
            
        }
    }
}