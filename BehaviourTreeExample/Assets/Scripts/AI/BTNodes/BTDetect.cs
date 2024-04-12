using System;
using UnityEngine;

/// <summary>
/// This node listens to detection events and updates the blackboard with the detected target.
/// If no target is detected, the node will return TaskStatus.Running.
/// If a target is detected, the node will return TaskStatus.Success.
/// </summary>

public class BTDetect : BTBaseNode
{
    private readonly Blackboard blackboard;
    private ViewCone viewCone;
    
    private bool hasTarget;
    private Transform target;


    public BTDetect(Blackboard _blackboard) : base("Detect")
    {
        blackboard = _blackboard;
        viewCone = _blackboard.GetVariable<ViewCone>(Strings.ViewCone);

        viewCone.OnTargetFound += OnTargetFound;
        viewCone.OnTargetLost += OnTargetLost;
    }


    public override void OnTerminate()
    {
        base.OnTerminate();
        viewCone.OnTargetFound -= OnTargetFound;
        viewCone.OnTargetLost -= OnTargetLost;
    }


    protected override TaskStatus Run()
    {
        if (hasTarget)
        {
            blackboard.SetVariable(Strings.LastSeenPosition, target.position);
        }
        
        return hasTarget ? TaskStatus.Success : TaskStatus.Running;
    }


    private void OnTargetFound(Transform _target)
    {
        target = _target;
        hasTarget = true;
        blackboard.SetVariable(Strings.Target, target);
    }


    private void OnTargetLost()
    {
        target = null;
        hasTarget = false;
        blackboard.SetVariable<Transform>(Strings.Target, null);
    }
}
