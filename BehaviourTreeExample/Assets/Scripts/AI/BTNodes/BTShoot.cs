using UnityEngine;
using UnityEngine.AI;

public class BTShoot : BTBaseNode
{
    private Player player;
    private NavMeshAgent agent;

    public BTShoot(Blackboard _blackboard)
    {
        agent = _blackboard.GetVariable<NavMeshAgent>("Agent");
        player = _blackboard.GetVariable<Player>("Player");
    }

    protected override TaskStatus Run()
    {
        
        
        // if player is dead
        // return TaskStatus.Success
        
        // if player is too far away
        // return TaskStatus.Failed
        
        if (Vector3.Distance(agent.transform.position, player.transform.position) > 5.0f)
        {
            return TaskStatus.Failed;
        }
        
        
        //fire weapon
        
        return TaskStatus.Running;
    }
}
