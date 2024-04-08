
using UnityEngine;
using UnityEngine.AI;

public class BTStopOnPath : BTBaseNode
{
    private readonly Blackboard blackboard;
    private NavMeshAgent agent;
    private float waitTime;
    private PathNode[] patrolNodes;
    private int patrolNodeIndex;
    private float t;

    public BTStopOnPath(Blackboard _blackboard) : base("StopOnPath")
    {
        blackboard = _blackboard;
        patrolNodes = _blackboard.GetVariable<PathNode[]>(Strings.PatrolNodes);
        agent = _blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
    }

    protected override void OnEnter(bool _debug)
    {
        base.OnEnter(_debug);
        patrolNodeIndex = blackboard.GetVariable<int>(Strings.PatrolNodeIndex);
        waitTime = patrolNodes[patrolNodeIndex].WaitTime;
        t = .0f;
    }

    protected override TaskStatus Run()
    {
        t += Time.fixedDeltaTime;
        if (t >= waitTime)
        {
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}

