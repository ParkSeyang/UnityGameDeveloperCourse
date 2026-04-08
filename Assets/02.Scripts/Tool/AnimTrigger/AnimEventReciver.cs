using System;
using UnityEngine;

public class AnimEventReceiver : MonoBehaviour, IAnimTriggerEventReceiver
{
    public Action<string> OnAnimationTriggerReceived { get; set; }
}
