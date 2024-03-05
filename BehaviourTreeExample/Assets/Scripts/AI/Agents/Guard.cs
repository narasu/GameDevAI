using System.Collections;
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
        blackboard.SetVariable("WeaponCrates", WeaponCrates);

        var moveTo = new BTMoveTo(blackboard);

        var detect = new BTSequence("DetectSequence", false,
            new BTCacheStatus(blackboard, Strings.DetectionResult, new BTDetect(blackboard)),
            new BTSetDestinationOnTarget(blackboard));
        
        var patrol = new BTParallel("Patrol", Policy.RequireAll, Policy.RequireOne,
            new BTInvert(new BTGetStatus(blackboard, Strings.DetectionResult)),
            new BTSequence("path sequence", false,
                new BTPath(blackboard, moveTo),
                new BTWait(5.0f))
        );
        
        var chase = new BTSelector("Chase Selector",
            new BTSequence("Chase", false,
                moveTo,
                new BTTimeout(2.0f, TaskStatus.Failed, new BTGetStatus(blackboard, Strings.DetectionResult)))
        );
        
        var attack = new BTShoot(blackboard);
        
        
        var detectionSelector = new BTSelector("DetectionSelector",
            patrol,
            chase
        );

        tree = new BTParallel("Tree",Policy.RequireAll, Policy.RequireAll,
            detect,
            detectionSelector);

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