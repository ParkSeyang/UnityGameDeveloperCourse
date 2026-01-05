using System;
using UnityEngine;

namespace Study_Camera.Study_ObjectPool
{
    public interface IPoolAbleObject
    {
        Action<IPoolAbleObject> ReturnToPoolMethod { get; set; }

        string Key { get; set; }
        GameObject gameObject { get; }
    }
}