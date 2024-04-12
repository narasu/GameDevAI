using UnityEngine;
using UnityEngine.AI;

public class BTShoot : BTBaseNode
{
    private readonly Blackboard blackboard;
    private Player player;
    private NavMeshAgent agent;

    public BTShoot(Blackboard _blackboard) : base("Shoot")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>("Agent");
        player = blackboard.GetVariable<Player>("Player");
    }

    protected override TaskStatus Run()
    {
        
        // if player is dead
        // return TaskStatus.Success
        
        // if player is too far away
        // return TaskStatus.Failed
        Transform target = blackboard.GetVariable<Transform>(Strings.Target);
        Debug.Log(target);
        if (target == null || Vector3.Distance(agent.transform.position, target.position) > 10.0f)
        {
            return TaskStatus.Failed;
        }

        agent.isStopped = true;
        Debug.Log("bang bang");
        //fire weapon
        
        return TaskStatus.Success;
    }

    public override void OnExit(TaskStatus _status)
    {
        base.OnExit(_status);
        agent.isStopped = false;
    }
}
