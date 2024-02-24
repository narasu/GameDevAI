using UnityEngine;
using UnityEngine.AI;

public class BTFollowPath : BTDecorator
{
    private Transform[] pathNodes;
    private Blackboard blackboard;
    private Transform moveTarget;
    private int currentNode = 0;

    public BTFollowPath(Blackboard _blackboard, BTBaseNode _parent) : base(_parent)
    {
        blackboard = _blackboard;
        pathNodes = _blackboard.GetVariable<GameObject>("PatrolNodes").GetComponentsInChildren<Transform>();
        moveTarget = _blackboard.GetVariable<Transform>("MoveTarget");
    }

    public override TaskStatus Run()
    {
        moveTarget.position = pathNodes[currentNode].position;
        
        TaskStatus parentStatus = base.Run();

        if (parentStatus == TaskStatus.Success)
        {
            currentNode++;
            if (currentNode == pathNodes.Length)
            {
                currentNode = 0;
            }
        }

        return parentStatus;
    }
}
