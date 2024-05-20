using System;
using UnityEngine;

public class BTFindNearest : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string nearestTransformString;
    private readonly Transform user;
    private readonly Transform[] transformArray;


    public BTFindNearest(Blackboard _blackboard, string _transformArrayString, string _nearestTransformString) : base("SetDestinationOnCrate")
    {
        blackboard = _blackboard;
        user = blackboard.GetVariable<Transform>(Strings.UserTransform);
        transformArray = blackboard.GetVariable<Transform[]>(_transformArrayString);
        nearestTransformString = _nearestTransformString;
    }


    protected override TaskStatus Run()
    {
        float shortestDistance = float.MaxValue;
        int indexOfNearest = -1;

        for (int i=0; i<transformArray.Length; i++) {
            float dist = Vector3.Distance(user.position, transformArray[i].position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                indexOfNearest = i;
            }
        }

        if (shortestDistance < float.MaxValue)
        {
            blackboard.SetVariable(nearestTransformString, transformArray[indexOfNearest]);
            return TaskStatus.Success;
        }
        
        Debug.Log("No objects found");
        return TaskStatus.Failed;
    }
}
