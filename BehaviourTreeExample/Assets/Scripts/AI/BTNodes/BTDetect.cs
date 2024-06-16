using System;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// This node listens to detection events and updates the blackboard with the detected target.
/// If no target is detected, the node will return TaskStatus.Running.
/// If a target is detected, the node will return TaskStatus.Success.
/// </summary>

public class BTDetect : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string targetString;
    private readonly NavMeshAgent agent;
    private ViewCone viewCone;
    
    private bool hasTarget;
    private Transform target;
    
    public BTDetect(Blackboard _blackboard, string _targetString) : base("Detect")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        targetString = _targetString;
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
        return hasTarget ? TaskStatus.Success : TaskStatus.Failed;
    }


    private void OnTargetFound(Transform _target)
    {
        target = _target;
        hasTarget = true;
        blackboard.SetVariable(targetString, target);
        target.GetComponent<IDetectable>()?.CallDetected(agent.transform);
    }


    private void OnTargetLost()
    {
        target?.GetComponent<IDetectable>()?.CallEscaped();
        target = null;
        hasTarget = false;
        blackboard.SetVariable<Transform>(targetString, null);
    }
}
