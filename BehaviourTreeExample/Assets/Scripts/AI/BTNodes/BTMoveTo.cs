using UnityEngine;
using UnityEngine.AI;

public class BTMoveTo : BTBaseNode
{
    private Blackboard blackboard;
    private NavMeshAgent agent;

    public BTMoveTo(Blackboard _blackboard)
    {
        blackboard = _blackboard;
        agent = _blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
    }

    protected override void OnEnter(bool _debug)
    {
        base.OnEnter(_debug);
        agent.SetDestination(blackboard.GetVariable<Vector3>(Strings.Destination));
    }


    protected override TaskStatus Run()
    {
        
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            return TaskStatus.Failed;
        }
        
        if (Vector3.Distance(agent.transform.position, agent.destination) <= agent.stoppingDistance)
        {
            return TaskStatus.Success;
        }
        
        return TaskStatus.Running;
    }

}
