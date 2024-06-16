﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;
using System;

public class Rogue : MonoBehaviour
{
    [SerializeField] private Player player;
    [SerializeField] private GameObject BombPrefab;
    [SerializeField] private Transform throwOrigin;
    private BTBaseNode tree;
    private NavMeshAgent agent;
    private Animator animator;
    private int animIsMoving = Animator.StringToHash("IsMoving");
    private int animThrowTrigger = Animator.StringToHash("ThrowTrigger");
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
        else if (coverPoints is Cover[])
        {
            blackboard.SetVariable(Strings.CoverPoints, coverPoints);
        }

        blackboard.SetVariable(Strings.Player, FindObjectOfType<Player>().transform);
        
        
        BTParallel followPlayer = new("FollowPlayer", Policy.RequireAll, Policy.RequireOne, true,
            new BTCheckBool(blackboard, Strings.IsDetected, TaskStatus.Failed, TaskStatus.Running),
            new BTSetDestinationOnTransform(blackboard, Strings.Player), 
            new BTAnimate(animator, animIsMoving, new BTMoveTo(blackboard))
        );

        
        BTParallel onDetected = new("OnDetected", Policy.RequireAll, Policy.RequireOne, true,
            new BTSequence("",
                new BTFindOptimalCover(blackboard),
                new BTAnimate(animator, animIsMoving, new BTMoveTo(blackboard)),
                new BTLookAt(blackboard, Strings.Target),
                new BTAnimTrigger(animator, animThrowTrigger),
                new BTWait(0.75f),
                new BTThrowSmoke(blackboard, throwOrigin, Strings.Target),
                new BTWait(0.25f)
                ),
            new BTCheckBool(blackboard, Strings.IsDetected, TaskStatus.Running, TaskStatus.Failed)
        );

        tree = new BTSelector("", followPlayer, onDetected);
    }

    private void FixedUpdate()
    {
        tree?.Tick(false);
    }

    private void OnEnable() 
    {
        player.OnDetected += OnPlayerDetected;
        player.OnEscaped += OnPlayerEscaped;
    }

    private void OnDisable() 
    {
        player.OnDetected -= OnPlayerDetected;
        player.OnEscaped -= OnPlayerEscaped;
    }

    private void OnPlayerDetected(Transform _enemy)
    {
        blackboard.SetVariable(Strings.IsDetected, true);
        blackboard.SetVariable(Strings.Target, _enemy);
    }

    private void OnPlayerEscaped()
    {
        blackboard.SetVariable(Strings.IsDetected, false);
        blackboard.SetVariable<Transform>(Strings.Target, null);
    }

}
