using System;
using UnityEngine;

namespace Study_Camera.Study_StatePattern.UsingMono
{
    public class IdleState : BaseState
    {
        private const float WAIT_TIME = 3.0f;
        private float timer = 0.0f;
        
        public override void EnterState()
        {
            timer = 0.0f;
            gameObject.SetActive(true);
        }

        private void Update()
        {
            timer += Time.deltaTime;
            if (timer >= WAIT_TIME)
            {
                // 상태 전이 코드를 넣는다
                int randNum = UnityEngine.Random.Range(0, 2);
                switch (randNum)
                {
                    case 0 :
                        BossAlfa.ChangeState<ScratchState>();
                        break;
                    case 1 :
                        BossAlfa.ChangeState<BreathState>();
                        break;
                    default:
                        BossAlfa.ChangeState<IdleState>();
                        break;
                }
            }

            if (Input.GetKeyDown(KeyCode.T))
            {
                BossAlfa.ChangeState<DeadState>();
            }
        }
        
        public override void ExitState()
        {
            gameObject.SetActive(false);
        }
    }
}