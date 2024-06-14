using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCloud : MonoBehaviour
{
    [SerializeField] private float lifetime;

    private Timer timer;

    private void Awake()
    {
        timer = new Timer(lifetime);
    }

    private void FixedUpdate()
    {
        timer.Run(true, out bool expired);
        if (expired)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<IBlindable>()?.Blind();
    }
}
