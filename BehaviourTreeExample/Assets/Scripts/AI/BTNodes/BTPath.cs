using UnityEngine;
using UnityEngine.AI;

public class BTPath : BTDecorator
{
    private Transform[] pathNodes;
    private Blackboard blackboard;
    private Transform moveTarget;
    private int currentNode = 0;

    public BTPath(Blackboard _blackboard) : base(new BTMoveTo(_blackboard))
    {
        blackboard = _blackboard;
        pathNodes = _blackboard.GetVariable<GameObject>(Strings.PatrolNodes).GetComponentsInChildren<Transform>();
    }

    protected override void OnEnter(bool _debug)
    {
        base.OnEnter(_debug);
        blackboard.SetVariable<Vector3>(Strings.Destination, pathNodes[currentNode].position);
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
