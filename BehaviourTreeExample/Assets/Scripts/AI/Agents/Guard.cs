using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Guard : MonoBehaviour
{
    public Transform ViewTransform;
    public List<PathNode> PathNodes;
    public PathNode RootNode;
    private PathNode[] patrolNodes;
    public float PatrolSpeed = 2.0f;
    public float ChaseSpeed = 4.0f;
    private BTBaseNode tree;
    
    private int a_IsWalking = Animator.StringToHash("IsWalking");
    private int a_IsRunning = Animator.StringToHash("IsRunning");
    
    private NavMeshAgent agent;
    private Animator animator;
    private ViewCone viewCone;
    private Blackboard blackboard = new();
    
    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        viewCone = GetComponentInChildren<ViewCone>();

        InitializePath();
        
        blackboard.SetVariable(Strings.Agent, agent);
        blackboard.SetVariable(Strings.Animator, animator);
        blackboard.SetVariable(Strings.PatrolNodes, patrolNodes);
        blackboard.SetVariable(Strings.ViewCone, viewCone);
        blackboard.SetVariable(Strings.ViewTransform, ViewTransform);
        blackboard.SetVariable(Strings.PatrolSpeed, PatrolSpeed);
        blackboard.SetVariable(Strings.ChaseSpeed, ChaseSpeed);
        blackboard.SetVariable(Strings.WeaponCrates, FindObjectsOfType<WeaponPickup>());
        blackboard.SetVariable(Strings.AgentState, AgentState.PATROL);
    }

    private void Start()
    {
        var moveTo = new BTMoveTo(blackboard);

        var detect = new BTSequence("DetectSequence", false,
            new BTCacheStatus(blackboard, Strings.DetectionResult, new BTDetect(blackboard)),
            new BTSetDestinationOnTarget(blackboard));
        
        var path = new BTSequence("Path",
            new BTSetSpeed(blackboard, PatrolSpeed),
            new BTSetDestinationOnPath(blackboard), 
            new BTAnimate(animator, a_IsWalking, moveTo),
            new BTStopOnPath(blackboard));
        
        var patrol = new BTParallel("Patrol", Policy.RequireAll, Policy.RequireOne,
            new BTInvert(new BTGetStatus(blackboard, Strings.DetectionResult)),
            path
        );
        
        var chase = new BTSelector("Chase Selector",
            new BTSequence("Chase", false,
                new BTSetSpeed(blackboard, ChaseSpeed),
                new BTAnimate(animator, a_IsRunning, moveTo),
                new BTTimeout(2.0f, TaskStatus.Failed, new BTGetStatus(blackboard, Strings.DetectionResult)))
        );
        
        var attack = new BTShoot(blackboard);
        
        /*
         * find crate
         * moveTo (grab weapon)
         * chase (modified so agent stops some distance away from player)
         * shoot
         */
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
    private void InitializePath()
    {
        if (PathNodes.Count == 0)
        {
            return;
        }
        
        // convert path node positions to global
        patrolNodes = PathNodes.ToArray();
        for (int i = 0; i < patrolNodes.Length; i++)
        {
            patrolNodes[i].Position = transform.position + transform.rotation * patrolNodes[i].Position;
            
            Vector3 sumRotation = patrolNodes[i].Rotation.eulerAngles + transform.rotation.eulerAngles;
            patrolNodes[i].Rotation = Quaternion.Euler(sumRotation);
        }
        
        // add starting position as node
        RootNode.Position = transform.position;
        RootNode.Rotation = transform.rotation;
        IEnumerable<PathNode> result = patrolNodes.Prepend(RootNode);
        patrolNodes = result.ToArray();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // if (other.GetComponent<IPickup>() is { } pickup)
        // {
        //     EventManager.Invoke(new WeaponPickedUpEvent(pickup.PickUp()));
        // }
    }
}