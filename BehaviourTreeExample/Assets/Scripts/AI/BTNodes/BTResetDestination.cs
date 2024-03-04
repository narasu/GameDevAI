using UnityEngine.AI;

public class BTResetDestination : BTBaseNode
{
    private readonly Blackboard blackboard;
    private NavMeshAgent agent;

    public BTResetDestination(Blackboard _blackboard) : base("ResetDestination")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
    }
    
    protected override TaskStatus Run()
    {
        agent.ResetPath();
        return TaskStatus.Success;
    }
}
