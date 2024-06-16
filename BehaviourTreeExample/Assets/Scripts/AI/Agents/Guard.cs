using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;


public class Guard : MonoBehaviour, IWeaponUser, IBlindable
{
    public Transform ViewTransform;
    public List<PathNode> PathNodes;
    public PathNode RootNode;
    private PathNode[] patrolNodes;
    public float PatrolSpeed = 2.0f;
    public float ChaseSpeed = 4.0f;
    public float BlindTime = 5.0f;
    
    
    private int animIsWalking = Animator.StringToHash("IsWalking");
    private int animIsRunning = Animator.StringToHash("IsRunning");
    private int animHasWeapon = Animator.StringToHash("HasWeapon");
    private int animIsBlinded = Animator.StringToHash("IsBlinded");
    
    private NavMeshAgent agent;
    private Animator animator;
    private ViewCone viewCone;
    private Blackboard blackboard = new();
    private BTBaseNode tree;

    private void Awake()
    {

        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
        viewCone = GetComponentInChildren<ViewCone>();

        InitializePath();
        
        blackboard.SetVariable(Strings.UserTransform, transform);
        blackboard.SetVariable(Strings.Agent, agent);
        blackboard.SetVariable(Strings.Animator, animator);
        blackboard.SetVariable(Strings.PatrolNodes, patrolNodes);
        blackboard.SetVariable(Strings.ViewCone, viewCone);
        blackboard.SetVariable(Strings.ViewTransform, ViewTransform);
        blackboard.SetVariable(Strings.PatrolSpeed, PatrolSpeed);
        blackboard.SetVariable(Strings.ChaseSpeed, ChaseSpeed);
        
    }

    private void Start()
    {
        if (!ServiceLocator.TryLocate(Strings.WeaponCrates, out object weaponCrates)) 
        {
            Debug.LogError("Could not find WeaponCrates");
        }
        else
        {
            blackboard.SetVariable(Strings.WeaponCrates, weaponCrates as Transform[]);
        }

        BTMoveTo moveTo = new(blackboard);

        var detect = new BTSelector("Detect", 
            new BTSequence("Blinded", 
                new BTCheckBool(blackboard, Strings.IsBlinded), 
                new BTResetPath(blackboard),
                new BTAnimSetBool(animator, animIsBlinded, true),
                new BTSetComponentEnabled<ViewCone>(viewCone, false),
                new BTWait(BlindTime),
                new BTSetComponentEnabled<ViewCone>(viewCone, true),
                new BTAnimSetBool(animator, animIsBlinded, false),
                new BTSetBool(blackboard, Strings.IsBlinded, false)
                ),
            new BTCacheStatus(blackboard, Strings.DetectionResult, new BTDetect(blackboard, Strings.Player))
            );
        
        BTSequence path = new("Path",
            new BTSetSpeed(blackboard, PatrolSpeed),
            new BTSetDestinationOnPath(blackboard), 
            new BTAnimate(animator, animIsWalking, moveTo),
            new BTStopOnPath(blackboard)
        );
        
        BTParallel patrol = new("Patrol", Policy.RequireAll, Policy.RequireOne, true,
            new BTInvert(new BTGetStatus(blackboard, Strings.DetectionResult)),
            new BTInvert(new BTCheckBool(blackboard, Strings.IsBlinded)), 
            path
        );

        BTSequence attack = new ("Attack", false,

            new BTSetSpeed(blackboard, ChaseSpeed),

            new BTSelector("GetWeapon",
                new BTCheckBool(blackboard, Strings.HasWeapon),
                new BTSequence("GotoCrate", 
                    new BTFindNearest(blackboard, Strings.WeaponCrates, Strings.NearestCrate),
                    new BTSetDestinationOnTransform(blackboard, Strings.NearestCrate),
                    new BTAnimate(animator, animIsRunning, moveTo)
                )
            ),
            
            new BTParallel("", Policy.RequireAll, Policy.RequireOne, true,
                new BTSelector("Chase", 
                    new BTSetDestinationOnTransform(blackboard, Strings.Player),
                    new BTSetDestinationOnLastSeen(blackboard)
                ),
                new BTCondition("ShootOrMoveTo", 
                    new BTIsInRange(blackboard, Strings.Player, 8.0f),
                    new BTSequence("Shoot", 
                        new BTResetPath(blackboard),
                        new BTShoot(blackboard, Strings.Player, 1)
                        ),
                    
                    new BTAnimate(animator, animIsRunning, moveTo)
                ),
                new BTTimeout(1.0f, TaskStatus.Failed, new BTGetStatus(blackboard, Strings.DetectionResult))
            )
        );

        BTParallel onDetected = new("", Policy.RequireAll, Policy.RequireOne, true,
            new BTInvert(new BTCheckBool(blackboard, Strings.IsBlinded)), 
            attack
        );
        
        BTSelector detectionSelector = new("DetectionSelector",
            patrol,
            onDetected
        );

        tree = new BTParallel("Tree",Policy.RequireAll, Policy.RequireAll,
            detect,
            detectionSelector
        );

    }

    private void FixedUpdate()
    {
        tree?.Tick(false);
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

    public void PickUp()
    {
        blackboard.SetVariable(Strings.HasWeapon, true);
        animator.SetBool(animHasWeapon, true);
    }

    

    public void Blind()
    {
        blackboard.SetVariable(Strings.IsBlinded, true);
        blackboard.SetVariable(Strings.LastSeenPosition, transform.position);
        blackboard.SetVariable(Strings.DetectionResult, TaskStatus.Failed);
    }
}
