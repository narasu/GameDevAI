using UnityEngine;
using UnityEngine.AI;

public class BTWait : BTBaseNode
{
    private float waitTime;
    private float t;
    public BTWait(float _waitTime)
    {
        waitTime = _waitTime;
    }

    public override TaskStatus Run()
    {
        t += Time.fixedDeltaTime;
        if (t >= waitTime)
        {
            t = .0f;
            return TaskStatus.Success;
        }
        return TaskStatus.Running;
    }
    
}
