using System;
using System.Collections.Generic;
using UnityEngine;

namespace Study_Camera.Study_ObjectPool
{
    public class ObjectPool
    {
        private Stack<IPoolAbleObject> Pool { get; set; } = new Stack<IPoolAbleObject>();

        private int ExpandSize { get; set; }
        private Transform Parent { get; set; }

        private IPoolAbleObject PoolObject { get; set; }
        
        public void Initialize
            (IPoolAbleObject sample, int startSize, 
                int expandSize, Transform root = null)
        {
            PoolObject = sample;
            ExpandSize = expandSize;
            Parent = root;
            
            Expand(startSize);
        }

        private void Expand(int size)
        {
            for (int i = 0; i < size; i++)
            {
                // 새로 생성한 오브젝트의 IPoolAbleObject 컴포넌트가 instance에 할당됨
                IPoolAbleObject instance = GameObject
                        .Instantiate(PoolObject.gameObject, Parent)
                        .GetComponent<IPoolAbleObject>();
                
                Return(instance);
            }
        }

        // 반환받고 꼭 켜서(gameobject.SetActive(true)) 쓰세요.
        public IPoolAbleObject GetObject()
        {
            if (Pool.Count == 0)
            {
                Expand(ExpandSize);
            }

            var reVal = Pool.Pop();
            reVal.ReturnToPoolMethod = Return;
            return reVal;
        }

        private void Return(IPoolAbleObject endedObject)
        {
            endedObject.gameObject.SetActive(false);
            Pool.Push(endedObject);
        }
    }
}