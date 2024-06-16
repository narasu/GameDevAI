using UnityEngine;
using UnityEngine.AI;

public class BTLookAt : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string targetString;
    private NavMeshAgent agent;

    public BTLookAt(Blackboard _blackboard, string _targetString) : base("LookAt")
    {
        blackboard = _blackboard;
        targetString = _targetString;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
    }

    protected override TaskStatus Run()
    {
        Transform target = blackboard.GetVariable<Transform>(targetString);
        if (!target)
        {
            return TaskStatus.Failed;
        }
        agent.transform.LookAt(target);
        return TaskStatus.Success;
    }
}
