using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class BTFindOptimalCover : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly NavMeshAgent agent;
    private Cover[] coverPoints;

    public BTFindOptimalCover(Blackboard _blackboard) : base("FindOptimalCover")
    {
        blackboard = _blackboard;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        coverPoints = blackboard.GetVariable<Cover[]>(Strings.CoverPoints);
    }

    protected override TaskStatus Run()
    {
        Transform target = blackboard.GetVariable<Transform>(Strings.Target);
        if (target == null)
        {
            return TaskStatus.Failed;
        }

        IOrderedEnumerable<Cover> sortedArray = from cover in coverPoints
                orderby cover.GetDistance(agent.transform)
                select cover;

        Cover result = sortedArray.First(cover => cover.GetIsBlocking(target));
        if (result == null)
        {
            return TaskStatus.Failed;
        }

        blackboard.SetVariable(Strings.Destination, result.transform.position);
        return TaskStatus.Success;
    }
}
