using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cover : MonoBehaviour
{
    [SerializeField] private LayerMask wallMask;
    public bool GetIsBlocking(Transform target)
    {
        if (Physics.Raycast(transform.position, (target.position - transform.position).normalized, out var hit, Vector3.Distance(transform.position, target.position), wallMask))
        {
            return true;
        }
        return false;
    }
}
