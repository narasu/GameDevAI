using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] private LayerMask wallMask;

    public float GetDistance(Transform _target)
    {
        return Vector3.Distance(transform.position, _target.position);
    }

    public bool GetIsBlocking(Transform _target)
    {
        if (Physics.Raycast(transform.position, (_target.position - transform.position).normalized, out var hit, Vector3.Distance(transform.position, _target.position), wallMask))
        {
            return true;
        }
        return false;
    }
}
