using System;
using UnityEngine;

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
    
    
    private void Awake()
    {
        Animator = GetComponent<Animator>();
        
        // ParticleSystem?
        // 각종 연출과 효과에 사용되는 컴포넌트.
        // 입자를 이용한 각종 이펙트들이 사용된다고 생각하면 됩니다.
        // 몇백, 몇천개의 작은 입자들을 사용해서 화염이나 번개 같은
        // 그래픽효과를 만드는데에 사용이 됩니다.
        
        // .Play() => 파티클을 재생시키는 함수
        // .isPlaying => 재생중이니? 물어볼수있는 프로퍼티

        ScratchCollider.enabled = false;
        BreathCollider.enabled = false;
    }
}
