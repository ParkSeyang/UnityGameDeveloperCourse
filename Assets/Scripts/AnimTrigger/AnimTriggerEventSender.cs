using UnityEngine;
using UnityEngine.Serialization;

public class AnimTriggerEventSender : StateMachineBehaviour
{
    public AnimTriggerEvent enterEvent;
    public UpdateTriggerEvent[] updateEvents;
    public AnimTriggerEvent exitEvent;
    public IAnimTriggerEventReceiver receiver { get; set; }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);

        if (receiver == null)
        {
            receiver = animator.GetComponent<IAnimTriggerEventReceiver>()
                       ?? animator.GetComponentInParent<IAnimTriggerEventReceiver>()
                       ?? animator.GetComponentInChildren<IAnimTriggerEventReceiver>();

            if (receiver == null)
            {
                Debug.LogError($"{animator.name}에 IStateEventReceiver가 없습니다.");
                return;
            }
        }
        
        enterEvent.Init(this);
        exitEvent.Init(this);
        
        for (int i = 0; i < updateEvents.Length; i++)
        {
            updateEvents[i].Init(this);
        }
        
        if(enterEvent.IsNull == false) enterEvent.InvokeEvent();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
     
        if (updateEvents == null || updateEvents.Length == 0) return;
        
        for (int i = 0; i < updateEvents.Length; i++)
        {
            updateEvents[i].Check(stateInfo);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        
        if(exitEvent.IsNull == false) exitEvent.InvokeEvent();
    }
}