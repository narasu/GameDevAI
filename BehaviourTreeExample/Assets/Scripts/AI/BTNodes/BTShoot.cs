using UnityEngine;
using UnityEngine.AI;

public class BTShoot : BTBaseNode
{
    private readonly Blackboard blackboard;

    public BTShoot(Blackboard _blackboard) : base("Shoot")
    {
        blackboard = _blackboard;
    }

    protected override TaskStatus Run()
    {
        Transform target = blackboard.GetVariable<Transform>(Strings.Target);
        //Debug.Log(target);

        Debug.Log("bang bang");
        
        return TaskStatus.Success;
    }

    public override void OnExit(TaskStatus _status)
    {
        base.OnExit(_status);
    }
}
