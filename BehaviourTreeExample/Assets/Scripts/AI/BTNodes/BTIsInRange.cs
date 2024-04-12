using UnityEngine;
using UnityEngine.AI;

public class BTIsInRange : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly float distance;
    private readonly NavMeshAgent agent;

    public BTIsInRange(Blackboard _blackboard, float _distance) : base("IsInRange")
    {
        blackboard = _blackboard;
        distance = _distance;

        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        
    }

    protected override TaskStatus Run()
    {
        Transform target = blackboard.GetVariable<Transform>(Strings.Target);
        if (target == null || Vector3.Distance(agent.transform.position, target.position) > distance) 
        {
            return TaskStatus.Failed;
        }

        return TaskStatus.Success;
    }
}
