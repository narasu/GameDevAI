using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class Rogue : MonoBehaviour
{

    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    private Blackboard blackboard = new();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        blackboard.SetVariable(Strings.Agent, agent);
        blackboard.SetVariable(Strings.Animator, animator);
    }

    private void Start()
    {
        
        Debug.Log(FindObjectOfType<Player>().transform);
        blackboard.SetVariable(Strings.Player, FindObjectOfType<Player>().transform);
        BTParallel followPlayer = new("FollowPlayer", Policy.RequireAll, Policy.RequireAll, 
            new BTSetDestinationOnTarget(blackboard, Strings.Player), 
            new BTMoveTo(blackboard)
        );

        tree = followPlayer;
    }

    private void FixedUpdate()
    {
        tree?.Tick();
    }
}
