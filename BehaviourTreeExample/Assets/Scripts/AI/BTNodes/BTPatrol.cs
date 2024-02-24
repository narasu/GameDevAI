using UnityEngine;
using UnityEngine.AI;

public class BTPatrol : BTBaseNode
{
    private Player player;
    private Transform[] patrolNodes;
    private NavMeshAgent agent;

    private BTSequence sequence;
    private int currentNode = 0;
    
    public BTPatrol(Blackboard _blackboard)
    {
        //sequence = new BTSequence(new BTWalkTo(_blackboard), new BTWait(1.0f));
        //patrolNodes = _blackboard.GetVariable<GameObject>("PatrolNodes").GetComponentsInChildren<Transform>();
        //agent = _blackboard.GetVariable<NavMeshAgent>("Agent");
    }

    public override TaskStatus Run()
    {
        if (Vector3.Distance(agent.transform.position, player.transform.position) < 10.0f)
        {
            return TaskStatus.Failed;
        }

        switch (sequence.Run())
        {
            case TaskStatus.Success:
                currentNode++;
                if (currentNode >= patrolNodes.Length)
                {
                    currentNode = 0;
                }
                break;
            case TaskStatus.Failed:
                return TaskStatus.Failed;
        }

        return TaskStatus.Running;
    }
}
