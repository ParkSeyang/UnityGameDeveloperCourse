using UnityEngine;
using System;
using System.Dynamic;

public class RuntimeScriptableSingleton<T> : ScriptableObject where T: ScriptableObject
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = CreateInstance<T>();
            }

            return instance;
        }
    }

    void Start()
    {
        
    }
    
    void Update()
    {
        
    }
}
