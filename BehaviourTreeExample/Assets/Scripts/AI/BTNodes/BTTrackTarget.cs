using UnityEngine;
using UnityEngine.AI;

public class BTTrackTarget : BTBaseNode
{
    private Transform target;
    private NavMeshAgent agent;
    private Blackboard blackboard;

    public BTTrackTarget(Blackboard _blackboard)
    {
        agent = _blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        blackboard = _blackboard;
    }

    protected override TaskStatus Run()
    {
        target = blackboard.GetVariable<Transform>(Strings.Target);
        if (target != null)
        {
            blackboard.SetVariable(Strings.Destination, target.position);
            return TaskStatus.Success;
        }
        return TaskStatus.Failed;
    }
}
