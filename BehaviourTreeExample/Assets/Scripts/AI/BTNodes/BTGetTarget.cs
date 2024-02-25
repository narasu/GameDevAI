using System;
using UnityEngine;

public class BTGetTarget : BTBaseNode
{
    private bool hasTarget;
    private Action<TargetFoundEvent> targetFoundEventHandler;
    protected override void OnEnter()
    {
        targetFoundEventHandler = _ => hasTarget = true;
        EventManager.Subscribe(typeof(TargetFoundEvent), targetFoundEventHandler);
    }

    public override TaskStatus Run()
    {
        if (hasTarget)
        {
            return TaskStatus.Success;
        }

        return TaskStatus.Running;
    }

    public override void OnExit(TaskStatus _status)
    {
        EventManager.Unsubscribe(typeof(TargetFoundEvent), targetFoundEventHandler);
        hasTarget = false;
    }
}
