using UnityEngine;
using UnityEngine.AI;

public class BTSetDestinationOnLastSeen : BTBaseNode
{
    private readonly Blackboard blackboard;
    public BTSetDestinationOnLastSeen(Blackboard _blackboard) : base("SetDestinationOnLastSeen")
    {
        blackboard = _blackboard;
        
    }
    protected override TaskStatus Run()
    {
        Vector3 lastSeenPosition = blackboard.GetVariable<Vector3>(Strings.LastSeenPosition);
        blackboard.SetVariable(Strings.Destination, lastSeenPosition);
        return TaskStatus.Success;
    }
}
