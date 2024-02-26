using UnityEngine;
using UnityEngine.AI;

public class BTFollowPath : BTDecorator
{
    private Transform[] pathNodes;
    private Blackboard blackboard;
    private Transform moveTarget;
    private int currentNode = 0;

    public BTFollowPath(Blackboard _blackboard) : base(new BTMoveTo(_blackboard))
    {
        blackboard = _blackboard;
        pathNodes = _blackboard.GetVariable<GameObject>("PatrolNodes").GetComponentsInChildren<Transform>();
        moveTarget = _blackboard.GetVariable<Transform>("MoveTarget");
    }

    protected override void OnEnter()
    {
        moveTarget.position = pathNodes[currentNode].position;
        base.OnEnter();
    }

    protected override TaskStatus Run()
    {
        TaskStatus childStatus = child.Tick();

        if (childStatus == TaskStatus.Success)
        {
            currentNode++;
            if (currentNode == pathNodes.Length)
            {
                currentNode = 0;
            }
        }

        return childStatus;
    }
}
