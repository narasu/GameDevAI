using UnityEngine;
using UnityEngine.AI;
/// <summary>
/// This node sets the destination on the blackboard to the position of the target, if it exists.
/// </summary>
public class BTSetDestinationOnTarget : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string targetString;

    public BTSetDestinationOnTarget(Blackboard _blackboard, string _targetString) : base("SetDestinationOnTarget")
    {
        blackboard = _blackboard;
        targetString = _targetString;
    }
    
    protected override TaskStatus Run()
    {
        Transform target = blackboard.GetVariable<Transform>(targetString);
        
        if (target == null)
        {
            return TaskStatus.Failed;
        }

        blackboard.SetVariable(Strings.Destination, target.position);
        return TaskStatus.Success;
    }
}
