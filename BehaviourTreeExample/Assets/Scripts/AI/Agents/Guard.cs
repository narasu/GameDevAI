﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Guard : MonoBehaviour
{
    [SerializeField] private Transform MoveTarget;
    [SerializeField] private GameObject PatrolNodes;
    [SerializeField] private GameObject[] WeaponCrates;
    private BTBaseNode tree;
    
    private NavMeshAgent agent;
    private Animator animator;
    private Blackboard blackboard = new();
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        blackboard.SetVariable(Strings.Agent, agent);
        blackboard.SetVariable(Strings.Animator, animator);
        blackboard.SetVariable(Strings.PatrolNodes, PatrolNodes);
        //blackboard.SetVariable("WeaponCrates", WeaponCrates);

        var moveTo = new BTMoveTo(blackboard);

        var detect = new BTSequence(false,
            new BTCacheStatus(blackboard, Strings.DetectionResult, new BTDetect(blackboard)),
            new BTSetDestinationOnTarget(blackboard));
        
        var patrol = new BTParallel(Policy.RequireAll, Policy.RequireOne,
            new BTInvert(new BTGetStatus(blackboard, Strings.DetectionResult, true)),
            new BTSequence(false,
                new BTPath(blackboard, moveTo),
                new BTWait(2.0f))
        );

        
        var chase = new BTSequence(false,
            moveTo,
            new BTGetStatus(blackboard, Strings.DetectionResult, false));

        
        var detectionSelector = new BTSelector(
            patrol,
            chase
        );

        tree = new BTParallel(Policy.RequireAll, Policy.RequireAll,
            detect,
            detectionSelector);

        //BTCondition btDetected = new(detect, attack, patrol);
        //tree = new BTParallel(Policy.RequireAll, Policy.RequireAll, detect, btDetected);



        /*
         * parallel {
         *
         *   detect   -   if target is lost, should there be a delay before this returns failed?
         *
         *   condition : detect {
         *
         *     on fail: patrol  -  should this return failed on detection? should it even be aware of that?
         *
         *     on success: sequence {
         *       get weapon  -  can store a bool so the guard won't have to search for a weapon every detection event
         *       chase  -  returns success if close enough
         *       shoot  -  returns failed if too far
         *     }
         *
         *   }
         *
         * }
         */
    }

    private void FixedUpdate()
    {
        tree?.Tick();
    }

    private void OnDestroy()
    {
        tree?.OnTerminate();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // if (other.GetComponent<IPickup>() is { } pickup)
        // {
        //     EventManager.Invoke(new WeaponPickedUpEvent(pickup.PickUp()));
        // }
    }
}