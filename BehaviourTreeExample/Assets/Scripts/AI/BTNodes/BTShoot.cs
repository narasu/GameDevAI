using UnityEngine;
using UnityEngine.AI;

public class BTShoot : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string targetString;
    private readonly int damage;
    private NavMeshAgent agent;

    public BTShoot(Blackboard _blackboard, string _targetString, int _damage) : base("Shoot")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        targetString = _targetString;
        damage = _damage;
    }

    protected override TaskStatus Run()
    {
        Transform target = blackboard.GetVariable<Transform>(targetString);
        target.GetComponent<Player>()?.TakeDamage(agent.gameObject, damage);
        
        return TaskStatus.Success;
    }

    public override void OnExit(TaskStatus _status)
    {
        base.OnExit(_status);
    }
}
