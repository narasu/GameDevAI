using UnityEngine;
using UnityEngine.AI;

public class BTWalkToNextNode : BTBaseNode
{
    private Blackboard blackboard;
    private Transform[] patrolNodes;
    private NavMeshAgent agent;
    private int currentNode = 0;
    
    public BTWalkToNextNode(Blackboard _blackboard)
    {
        blackboard = _blackboard;
        patrolNodes = blackboard.GetVariable<GameObject>("PatrolNodes").GetComponentsInChildren<Transform>();
        foreach (Transform p in patrolNodes)
        {
            Debug.Log(p);
        }
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
        agent.SetDestination(patrolNodes[currentNode].position);
        if (Vector3.Distance(agent.transform.position, patrolNodes[currentNode].position) < 1.0f)
        {
            currentNode++;
            if (currentNode == patrolNodes.Length)
            {
                currentNode = 0;
            }

            
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
}
