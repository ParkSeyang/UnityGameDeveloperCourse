using UnityEngine;

namespace Study_Camera.Study_StatePattern.Standard
{
    public abstract class BaseState
    {
        public struct StateControllerParameter
        {
            public BossAlfa bossAlfa;
            public Transform firePoint;
            public ParticleSystem breathEffect; 
            public Collider ScratchCollider;
            public Collider BreathCollider;
            public Animator bossAnimator;
            public AnimEventReceiver AnimEventReceiver;
        }

        protected BossAlfa BossAlfa { get; private set; }
        protected Animator BossAnimator { get; private set; }

        public virtual void Initialize(StateControllerParameter parameter)
        {
            BossAlfa = parameter.bossAlfa;
            BossAnimator = parameter.bossAnimator;
        }
        
        public abstract void EnterState();
        public abstract void UpdateState(float timeDelta); // timeDelta는 안써도 되긴함.
        public abstract void ExitState();
    }
}