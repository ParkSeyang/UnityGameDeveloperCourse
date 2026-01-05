using System;

public interface IAnimTriggerEventReceiver
{
    Action<string> OnAnimationTriggerReceived { get; set; }
}