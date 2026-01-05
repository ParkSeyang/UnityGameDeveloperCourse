using System;
using UnityEngine;

namespace Study_Camera.Study_ObjectPool
{
    public class FxObject : MonoBehaviour, IPoolAbleObject
    {
        public Action<IPoolAbleObject> ReturnToPoolMethod { get; set; }
        [field:SerializeField] public string Key { get; set; }

        private ParticleSystem particleSystem;

        private void Awake() { particleSystem = GetComponent<ParticleSystem>(); }

        private void OnEnable() { particleSystem.Play(); }

        private void Update()
        {
            // 재생중일때는 패스
            if (particleSystem.isPlaying) return;
            
            // 풀로 되돌아가는 함수
            ReturnToPoolMethod.Invoke(this);
        }
    }
}