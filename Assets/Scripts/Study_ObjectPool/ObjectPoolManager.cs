using System;
using System.Collections.Generic;
using Study_Camera.CombatSystem;
using UnityEngine;


namespace Study_Camera.Study_ObjectPool
{
    public class ObjectPoolManager : SingletonBase<ObjectPoolManager>
    {
        private Dictionary<string, ObjectPool> PoolDictionary { get; set; }
            = new Dictionary<string, ObjectPool>();

        // ScriptableObject로 관리하는걸 추천
        [SerializeField] private FxObject[] fxObjectList;
        
        private void Start()
        {
            for (int i = 0; i < fxObjectList.Length; i++)
            {
                var obj = fxObjectList[i];
                var pool = new ObjectPool();
                pool.Initialize(obj, 5, 5, transform);
                PoolDictionary.TryAdd(obj.Key, pool);
            }
        }

        public void SpawnFxObject(string key, Vector3 position)
        {
            var pool = PoolDictionary[key];
            var fxObject = pool.GetObject();
            fxObject.gameObject.transform.position = position;
            fxObject.gameObject.SetActive(true);
        }
    }
}