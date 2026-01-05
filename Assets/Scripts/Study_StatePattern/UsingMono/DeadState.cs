using UnityEngine;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public class DeadState : BaseState
    {
        private static readonly int DEAD = Animator.StringToHash("Dead");

        public override void EnterState()
        {
            gameObject.SetActive(false);
            BossAnimator.SetTrigger(DEAD);
        }

        public override void ExitState()
        {
            gameObject.SetActive(false);
        }
    }
}