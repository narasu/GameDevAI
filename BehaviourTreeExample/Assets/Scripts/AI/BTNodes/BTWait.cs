using UnityEngine;
using UnityEngine.AI;

public class BTWait : BTBaseNode
{
    private NavMeshAgent agent;
    private Animator animator;
    private float waitLength = 1.0f;
    private float t = .0f;
    public BTWait(Blackboard _blackboard)
    {
        agent = _blackboard.GetVariable<NavMeshAgent>("Agent");
        animator = _blackboard.GetVariable<Animator>("Animator");
    }

    public override void OnEnter()
    {
        agent.isStopped = true;
    }

    public override TaskStatus Run()
    {
        t += Time.fixedDeltaTime;
        if (t >= waitLength)
        {
            t = .0f;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }

    public override void OnExit()
    {
        agent.isStopped = false;
    }
}
