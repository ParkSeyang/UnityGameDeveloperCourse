using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class AnimTriggerEvent
{
    public string parameter;
    protected AnimTriggerEventSender stateEventSender;
    
    public bool IsNull => string.IsNullOrEmpty(parameter);
    
    public virtual void Init(AnimTriggerEventSender sender)
    {
        stateEventSender = sender;
    }
    
    public void InvokeEvent()
    {
        stateEventSender.receiver.OnAnimationTriggerReceived?.Invoke(parameter);
    }
}

[Serializable]
public class UpdateTriggerEvent : AnimTriggerEvent
{
    [Range(0f, 1f)] public float timing = 0.0f;
    private bool isPassStartNormalizedTime;

    public override void Init(AnimTriggerEventSender sender)
    {
        isPassStartNormalizedTime = false;
        base.Init(sender);
    }

    public void Check(AnimatorStateInfo stateInfo)
    {
        //지나쳤다
        if (isPassStartNormalizedTime == false && timing < stateInfo.normalizedTime)
        {
            InvokeEvent();
            isPassStartNormalizedTime = true;
        }
    }
}