using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeBomb : MonoBehaviour
{
    public GameObject impactPrefab;

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log(other.collider);
        Instantiate(impactPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
