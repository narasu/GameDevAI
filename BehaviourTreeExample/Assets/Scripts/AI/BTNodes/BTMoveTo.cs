using UnityEngine;
using UnityEngine.AI;

public class BTMoveTo : BTBaseNode
{
    private Blackboard blackboard;
    private Transform moveTarget;
    private NavMeshAgent agent;

    public BTMoveTo(Blackboard _blackboard)
    {
        moveTarget = _blackboard.GetVariable<Transform>("MoveTarget");
        agent = _blackboard.GetVariable<NavMeshAgent>("Agent");
    }


    protected override TaskStatus Run()
    {
        Vector3 destination = moveTarget.position;
        agent.SetDestination(destination);
        
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            return TaskStatus.Failed;
        }
        
        if (Vector3.Distance(agent.transform.position, destination) <= agent.stoppingDistance)
        {
            return TaskStatus.Success;
        }
        
        return TaskStatus.Running;
    }

}
