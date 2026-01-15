using UnityEngine;
using System.Collections.Generic;

public static class LayerMaskExtensions
{
    /// <summary>
    /// LayerMask에 특정 레이어(int)가 포함되어 있는지 확인합니다.
    /// </summary>
    public static bool Contains(this LayerMask mask, int layer) => (mask.value & (1 << layer)) != 0;

    /// <summary>
    /// LayerMask에 특정 GameObject의 레이어가 포함되어 있는지 확인합니다.
    /// </summary>
    public static bool Contains(this LayerMask mask, GameObject gameObject) => mask.Contains(gameObject.layer);
    
    /// <summary>
    /// 기존 마스크에 새로운 레이어를 추가한 복사본을 반환합니다.
    /// </summary>
    public static LayerMask With(this LayerMask mask, int layer)
    {
        mask.value |= (1 << layer);
        return mask;
    }

    /// <summary>
    /// 기존 마스크에 여러 레이어(params)를 추가한 복사본을 반환합니다.
    /// </summary>
    public static LayerMask With(this LayerMask mask, params string[] layerNames)
    {
        foreach (var name in layerNames)
        {
            int layer = LayerMask.NameToLayer(name);
            if (layer != -1)
            {
                mask.value |= (1 << layer);
            }
        }
        return mask;
    }

    /// <summary>
    /// 기존 마스크에서 특정 레이어를 제거한 복사본을 반환합니다.
    /// </summary>
    public static LayerMask Without(this LayerMask mask, int layer)
    {
        mask.value &= ~(1 << layer);
        return mask;
    }

    /// <summary>
    /// 기존 마스크의 반전된 값을 반환합니다. (제외 처리 시 사용)
    /// </summary>
    public static LayerMask Inverted(this LayerMask mask)
    {
        mask.value = ~mask.value;
        return mask;
    }

    // --- 디버깅 및 유틸리티 (Debug & Utility) ---

    /// <summary>
    /// 현재 마스크에 포함된 레이어의 인덱스 목록을 반환합니다.
    /// (주의: 메모리 할당이 발생)
    /// </summary>
    public static List<int> GetLayerIndexes(this LayerMask mask)
    {
        var layers = new List<int>();
        for (int i = 0; i < 32; i++)
        {
            if ((mask.value & (1 << i)) != 0)
            {
                layers.Add(i);
            }
        }
        return layers;
    }
}