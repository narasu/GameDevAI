using System;
using UnityEngine;

/// <summary>
/// This node listens to detection events and updates the blackboard with the detected target.
/// If no target is detected, the node will return TaskStatus.Running.
/// If a target is detected, the node will return TaskStatus.Success.
/// </summary>

public class BTDetect : BTBaseNode
{
    private Blackboard blackboard;
    private bool hasTarget;
    private Action<TargetFoundEvent> targetFoundEventHandler;
    private Action<TargetLostEvent> targetLostEventHandler;
    public BTDetect(Blackboard _blackboard)
    {
        blackboard = _blackboard;
        
        targetFoundEventHandler = OnTargetFound;
        targetLostEventHandler = OnTargetLost;
        EventManager.Subscribe(typeof(TargetFoundEvent), targetFoundEventHandler);
        EventManager.Subscribe(typeof(TargetLostEvent), targetLostEventHandler);
    }

    protected override TaskStatus Run()
    {
        return hasTarget ? TaskStatus.Success : TaskStatus.Running;
    }
    
    public override void OnTerminate()
    {
        EventManager.Unsubscribe(typeof(TargetFoundEvent), targetFoundEventHandler);
        EventManager.Unsubscribe(typeof(TargetLostEvent), targetLostEventHandler);
    }

    private void OnTargetFound(TargetFoundEvent _event)
    {
        hasTarget = true;
        Debug.Log(_event.Target);
        blackboard.SetVariable(Strings.Target, _event.Target);
    }

    private void OnTargetLost(TargetLostEvent _event)
    {
        blackboard.SetVariable<Transform>(Strings.Target, null);
        hasTarget = false;
    }
}
