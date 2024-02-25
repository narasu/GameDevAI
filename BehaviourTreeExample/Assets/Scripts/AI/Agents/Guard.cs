using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private Transform MoveTarget;
    [SerializeField] private GameObject PatrolNodes;
    [SerializeField] private GameObject[] WeaponCrates;
    private BTBaseNode tree;
    private BTSequence patrol;
    
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
        blackboard.SetVariable("Agent", agent);
        blackboard.SetVariable("Animator", animator);
        blackboard.SetVariable("MoveTarget", MoveTarget);
        blackboard.SetVariable("PatrolNodes", PatrolNodes);
        //blackboard.SetVariable("WeaponCrates", WeaponCrates);
        Player player = FindObjectOfType<Player>();
        blackboard.SetVariable("Player", player);
        patrol = new BTSequence(new BTFollowPath(blackboard), new BTWait(1.0f));

        // getWeapon = new BTSequence(new BTFindWeapon(blackboard), new BTMoveTo(blackboard), new BTCheckWeapon(blackboard));
        // attack = new BTSequence(new BTGetTarget, new BTMoveTo, new BTShoot)
        tree = patrol;
    }

    private void FixedUpdate()
    {
        tree?.Tick();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // if (other.GetComponent<IPickup>() is { } pickup)
        // {
        //     EventManager.Invoke(new WeaponPickedUpEvent(pickup.PickUp()));
        // }
    }
}
