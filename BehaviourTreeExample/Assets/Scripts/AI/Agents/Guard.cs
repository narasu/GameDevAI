using System;
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
    private BTSequence patrol, attack;
    
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

        BTSequence detection = new(false, new BTDetect(blackboard));

        BTSequence follow = new BTSequence(false,
            new BTSelector(
                new BTTrackTarget(blackboard),
                new BTInvert(new BTWait(10.0f))
            )
        );

        BTMoveTo moveTo = new(blackboard);

        BTParallel p = new(Policy.RequireAll, Policy.RequireAll,
            new BTDetect(blackboard)
        );


        //BTCondition btDetected = new(detect, attack, patrol);
        //tree = new BTParallel(Policy.RequireAll, Policy.RequireAll, detect, btDetected);

        /*foreach (MethodInfo m in typeof(ExampleClass).GetMethods(BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.DeclaredOnly))
        {
            Debug.Log(m.Name);
        }

        ExampleClass c = new();

        c.GetType().GetMethod("SetSecret", BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.DeclaredOnly).Invoke(c, new[] { "I'M IN" });
        Debug.Log(c.GetType().GetMethod("GetSecret", BindingFlags.NonPublic|BindingFlags.Instance|BindingFlags.DeclaredOnly).Invoke(c, null));

        object instance = System.Activator.CreateInstance(typeof(ExampleClass));

        var a = instance.GetType().GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        foreach (var m in a)
        {
            Debug.Log(m.Name);
        }*/

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
        //tree?.Tick();
    }

    private void OnDestroy()
    {
        //tree?.OnTerminate();
    }

    private void OnTriggerEnter(Collider other)
    {
        
        // if (other.GetComponent<IPickup>() is { } pickup)
        // {
        //     EventManager.Invoke(new WeaponPickedUpEvent(pickup.PickUp()));
        // }
    }
}
