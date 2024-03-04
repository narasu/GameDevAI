using System;
using UnityEngine;
using UnityEngine.AI;

public class BTFindWeapon : BTBaseNode
{
    private Blackboard blackboard;
    private NavMeshAgent agent;
    private Transform moveTarget;
    private GameObject[] weaponCrates;
    
    public BTFindWeapon(Blackboard _blackboard) : base("FindWeapon")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        weaponCrates = blackboard.GetVariable<GameObject[]>(Strings.WeaponCrates);
    }

    // public override void OnEnter()
    // {
    //     EventManager.Subscribe(typeof(WeaponPickedUpEvent), pickupEventHandler);
    //     
    //     float shortestDistance = Mathf.Infinity;
    //     Vector3 nearestPosition = new();
    //     foreach (GameObject crate in weaponCrates)
    //     {
    //         float dist = Vector3.Distance(agent.transform.position, crate.transform.position);
    //         if (dist < shortestDistance)
    //         {
    //             shortestDistance = dist;
    //             nearestPosition = crate.transform.position;
    //         }
    //     }
    //     agent.SetDestination(nearestPosition);
    //     
    //     //animator.SetBool...
    // }

    protected override TaskStatus Run()
    {
        float shortestDistance = Mathf.Infinity;
        Vector3 nearestPosition = new();
        foreach (GameObject crate in weaponCrates)
        {
            float dist = Vector3.Distance(agent.transform.position, crate.transform.position);
            if (dist < shortestDistance)
            {
                shortestDistance = dist;
                nearestPosition = crate.transform.position;
            }
        }

        if (shortestDistance < Mathf.Infinity)
        {
            moveTarget.position = nearestPosition;
            return TaskStatus.Success;
        }
        
        return TaskStatus.Running;
    }

    // public override void OnExit()
    // {
    //     EventManager.Unsubscribe(typeof(WeaponPickedUpEvent), pickupEventHandler);
    //     weaponGrabbed = false;
    // }
}
