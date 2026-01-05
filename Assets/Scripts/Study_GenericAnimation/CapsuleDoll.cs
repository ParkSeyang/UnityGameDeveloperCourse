using System;
using System.Collections;
using Study_Camera.CombatSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class CapsuleDoll : MonoBehaviour, ICombatAgent
{
    [SerializeField] private Renderer myRenderer;

    private void Awake()
    {
        myRenderer = GetComponent<Renderer>();
    }

    private void Start()
    {
        var allHurtBox = GetComponentsInChildren<HurtBox>();
        foreach (var hurtBox in allHurtBox) hurtBox.Initialize(this);
    }

    public void TakeDamage(int damage)
    {
        StartCoroutine(DamageColorCoroutine(damage));
    }

    public void OnHitDetected(HitInfo hitInfo)
    {
        
    }
    
    
    
    private IEnumerator DamageColorCoroutine(int damage)
    {
        float sumTime = 0.0f;
        Material material = myRenderer.material;

        Color originalColor = material.color;
        Color targetColor = damage == 10 ? Color.orange : Color.red;
        material.color = targetColor;

        while (sumTime < 0.5f)
        {
            sumTime += Time.deltaTime;
            yield return null;
        }

        material.color = originalColor;
    }
}
