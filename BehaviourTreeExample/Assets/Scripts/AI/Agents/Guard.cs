using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;

public class Guard : MonoBehaviour
{
    [SerializeField] private GameObject PatrolNodes;
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
        blackboard.SetVariable("PatrolNodes", PatrolNodes);
        patrol = new BTSequence(new BTWalkToNextNode(blackboard), new BTWait(blackboard));
        tree = new BTSelector(patrol);
    }

    private void FixedUpdate()
    {
        tree?.Run();
    }
}
