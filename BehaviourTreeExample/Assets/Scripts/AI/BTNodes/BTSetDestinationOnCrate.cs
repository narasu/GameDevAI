using System;
using UnityEngine;
using UnityEngine.AI;

public class BTSetDestinationOnCrate : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly NavMeshAgent agent;
    private readonly WeaponPickup[] crates;


    public BTSetDestinationOnCrate(Blackboard _blackboard) : base("SetDestinationOnCrate")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        ServiceLocator.TryLocate(Strings.WeaponCrates, out object pickups);
        crates = pickups as WeaponPickup[];
    }


    protected override TaskStatus Run()
    {
        float shortestDistance = float.MaxValue;
        Vector3 nearestPosition = new();
        foreach (WeaponPickup c in crates)
        {
            float dist = Vector3.Distance(agent.transform.position, c.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                nearestPosition = c.transform.position;
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
}
