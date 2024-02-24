using System;
using UnityEngine;
using UnityEngine.AI;

public class BTGetWeapon : BTBaseNode
{
    private Blackboard blackboard;
    private NavMeshAgent agent;
    private GameObject[] weaponCrates;
    private bool weaponGrabbed;
    private Action<WeaponPickedUpEvent> pickupEventHandler;
    
    public BTGetWeapon(Blackboard _blackboard)
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>("Agent");
        weaponCrates = blackboard.GetVariable<GameObject[]>("WeaponCrates");
        pickupEventHandler = _ => weaponGrabbed = true;
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

    public override TaskStatus Run()
    {
        if (agent.pathStatus == NavMeshPathStatus.PathInvalid)
        {
            return TaskStatus.Failed;
        }

        if (weaponGrabbed)
        {
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
