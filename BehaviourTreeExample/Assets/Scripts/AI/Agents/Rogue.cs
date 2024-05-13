using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System;

public class Rogue : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject BombPrefab;
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
        blackboard.SetVariable(Strings.BombPrefab, BombPrefab);
    }

    private void Start()
    {
        if (!ServiceLocator.TryLocate(Strings.CoverPoints, out object coverPoints))
        {
            Debug.LogError("Could not find CoverPoints");
        }
        else
        {
            blackboard.SetVariable(Strings.CoverPoints, coverPoints as Transform[]);
        }

        blackboard.SetVariable(Strings.Player, FindObjectOfType<Player>().transform);
        
        
        BTParallel followPlayer = new("FollowPlayer", Policy.RequireAll, Policy.RequireAll, 
            new BTSetDestinationOnTransform(blackboard, Strings.Player), 
            new BTMoveTo(blackboard)
        );

        BTSequence onDetected = new("OnDetected", new BTThrowSmoke(blackboard, Strings.Target));

        tree = new BTSelector("", new BTThrowSmoke(blackboard, Strings.Target), followPlayer);
    }

    private void FixedUpdate()
    {
        tree?.Tick();
    }

    private void OnEnable() 
    {
        player.OnDetected += OnPlayerDetected;
    }

    private void OnDisable() 
    {
        player.OnDetected -= OnPlayerDetected;
    }

    private void OnPlayerDetected(Transform _enemy)
    {
        blackboard.SetVariable(Strings.Target, _enemy);
    }

}
