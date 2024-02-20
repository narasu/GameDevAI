using UnityEngine;
using UnityEngine.AI;

public class BTGetWeapon : BTBaseNode
{
    private Blackboard blackboard;
    private NavMeshAgent agent;
    private int currentNode = 0;
    
    public BTGetWeapon(Blackboard _blackboard)
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>("Agent");
        
    }

    public override void OnEnter()
    {
        //animator.SetBool...
    }

    public override TaskStatus Run()
    {
        // if player is sighted
        // return TaskStatus.Failed;
        
        // go to patrol point
        // increment counter
        // reset counter at max
        /*agent.SetDestination(patrolNodes[currentNode].position);
        if (Vector3.Distance(agent.transform.position, patrolNodes[currentNode].position) < 1.0f)
        {
            currentNode++;
            if (currentNode == patrolNodes.Length)
            {
                currentNode = 0;
            }

            
            return TaskStatus.Success;
        }*/
        return TaskStatus.Running;
    }
}
