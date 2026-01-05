using System;
using UnityEngine;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public abstract class BaseState : MonoBehaviour
    {
        protected BossAlfa BossAlfa { get; private set; }
        protected Animator BossAnimator { get; private set; }
        
        public abstract void EnterState();
        public abstract void ExitState();

        protected virtual void Awake()
        {
            BossAlfa = GetComponentInParent<BossAlfa>();
            BossAnimator = GetComponentInParent<Animator>();
        }
    }
}