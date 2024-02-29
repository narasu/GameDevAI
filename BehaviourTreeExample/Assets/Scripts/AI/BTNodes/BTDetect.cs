using System;
using UnityEngine;

public class BTDetect : BTBaseNode
{
    private Blackboard blackboard;
    private bool hasTarget;
    private Transform target;
    private Action<TargetFoundEvent> targetFoundEventHandler;
    private Action<TargetLostEvent> targetLostEventHandler;
    public BTDetect(Blackboard _blackboard)
    {
        blackboard = _blackboard;
        
        targetFoundEventHandler = OnTargetFound;
        targetLostEventHandler = _ => hasTarget = false;
        EventManager.Subscribe(typeof(TargetFoundEvent), targetFoundEventHandler);
        EventManager.Subscribe(typeof(TargetLostEvent), targetLostEventHandler);
    }

    protected override TaskStatus Run()
    {
        return hasTarget ? TaskStatus.Success : TaskStatus.Failed;
    }
    
    public override void OnTerminate()
    {
        EventManager.Unsubscribe(typeof(TargetFoundEvent), targetFoundEventHandler);
        EventManager.Unsubscribe(typeof(TargetLostEvent), targetLostEventHandler);
    }

    private void OnTargetFound(TargetFoundEvent _event)
    {
        hasTarget = true;
        blackboard.SetVariable(Strings.Target, _event.Target);
    }

    private void OnTargetLost()
    {
        hasTarget = false;
        blackboard.SetVariable<>(Strings.Target, null);
    }
}
