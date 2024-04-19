using UnityEngine;
using UnityEngine.AI;

public class BTThrowSmoke : BTBaseNode
{
    private readonly Blackboard blackboard;
    private readonly string transformString;
    private NavMeshAgent agent;
    private GameObject bombPrefab;
    private Vector3 launchVelocity, landingPosition;
    
    public BTThrowSmoke(Blackboard _blackboard, string _transformString) : base("ThrowSmoke")
    {
        blackboard = _blackboard;
        transformString = _transformString;
        agent = blackboard.GetVariable<NavMeshAgent>(Strings.Agent);
        bombPrefab = blackboard.GetVariable<GameObject>(Strings.BombPrefab);
    }

    protected override TaskStatus Run()
    {
        Transform target = blackboard.GetVariable<Transform>(transformString);
        if (target == null)
        {
            return TaskStatus.Failed;
        }
        float initialVelocity = CalculateInitialVelocity(target.position);
        float gravity = Physics.gravity.y;
        float timeOfFlight = 2 * initialVelocity / gravity;

        // Update the bomb's rigidbody with the calculated trajectory
        launchVelocity = CalculateLaunchVelocity(target.position, timeOfFlight);
        
        GameObject bomb = GameObject.Instantiate(bombPrefab, agent.transform.position, agent.transform.rotation);
        bomb.GetComponent<Rigidbody>().velocity = launchVelocity;
        blackboard.SetVariable<Transform>(Strings.Target, null);
        return TaskStatus.Success;
    }
    
    private float CalculateInitialVelocity(Vector3 _landingPosition)
    {
        Vector3 displacement = agent.transform.position - _landingPosition;
        displacement.y = 0;
        
        float timeOfFlight = displacement.magnitude / 2.0f;
        float initialVelocity = displacement.magnitude / timeOfFlight;

        return initialVelocity;
    }

    private Vector3 CalculateLaunchVelocity(Vector3 _landingPosition, float timeOfFlight)
    {
        Vector3 displacement = agent.transform.position - _landingPosition;
        displacement.y = 0;
        
        Vector3 velocity = displacement / timeOfFlight;
        velocity.y = (displacement.y + 0.5f * Physics.gravity.y * timeOfFlight * timeOfFlight) / timeOfFlight;
        
        return velocity;
    }
}