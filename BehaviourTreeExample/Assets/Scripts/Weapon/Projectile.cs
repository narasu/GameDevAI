using System;
using UnityEngine;

public class Projectile : IPoolable
{
    public bool IsActive { get; set; }
    public float Damage { get; set; }


    public void Update()
    {
        //timeUntilHit.Run(Time.deltaTime, out bool hasHit);
        //if (hasHit) { Hit(); }
    }

    private void Hit()
    {
        //target.TakeDamage(Damage);
        Debug.Log("hit!");

        //EventManager.Invoke(new BulletDestroyedEvent(this));
    }

    public void OnEnableObject()
    {
        //gameObject.SetActive(true);
    }

    public void OnDisableObject()
    {
        //gameObject.SetActive(false);
    }
}