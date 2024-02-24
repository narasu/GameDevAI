using UnityEngine;
using UnityEngine.AI;

public class BTChase : BTBaseNode
{
    private Player player;
    private NavMeshAgent agent;

    public BTChase(Blackboard _blackboard)
    {
        agent = _blackboard.GetVariable<NavMeshAgent>("Agent");
        player = _blackboard.GetVariable<Player>("Player");
    }
    
    public override TaskStatus Run()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) < 5.0f)
        {
            return TaskStatus.Success;
        }
        
        if (Vector3.Distance(agent.transform.position, player.transform.position) > 10.0f)
        {
            return TaskStatus.Failed;
        }
        
        agent.SetDestination(player.transform.position);
        return TaskStatus.Running;
    }
}
