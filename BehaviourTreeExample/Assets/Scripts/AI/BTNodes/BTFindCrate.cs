using System;
using UnityEngine;
using UnityEngine.AI;

public class BTFindCrate : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly NavMeshAgent agent;
    private readonly GameObject[] objectArray;
    
    public BTFindCrate(Blackboard _blackboard, string _arrayString) : base("FindNearest")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        objectArray = blackboard.GetVariable<GameObject[]>(_arrayString);
    }

    protected override TaskStatus Run()
    {
        float shortestDistance = float.MaxValue;
        Vector3 nearestPosition = new();
        foreach (GameObject obj in objectArray)
        {
            float dist = Vector3.Distance(agent.transform.position, obj.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                nearestPosition = obj.transform.position;
            }
        }

        if (shortestDistance < float.MaxValue)
        {
            blackboard.SetVariable(Strings.Destination, nearestPosition);
            return TaskStatus.Success;
        }
        Debug.Log("No crates found");
        return TaskStatus.Failed;
    }

    // public override void OnExit()
    // {
    //     EventManager.Unsubscribe(typeof(WeaponPickedUpEvent), pickupEventHandler);
    //     weaponGrabbed = false;
    // }
}
