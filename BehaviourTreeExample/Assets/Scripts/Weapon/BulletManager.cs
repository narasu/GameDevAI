using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private ObjectPool<Projectile> projectilePool;
    private Action<WeaponFiredEvent> weaponFiredEventHandler;
    //private Action<BulletHitEvent> bulletHitEventHandler;

    private void Awake()
    {
        projectilePool = new ObjectPool<Projectile>();
        weaponFiredEventHandler = OnWeaponFired;
        //bulletHitEventHandler = _event => projectilePool.ReturnObjectToPool(_event.DestroyedBullet);
    }

    private void Update()
    {
        if (projectilePool.TryGetActiveObjects(out Projectile[] projectiles))
        {
            foreach (Projectile p in projectiles)
            {
                p.Update();
            }
        }
    }

    private void OnWeaponFired(WeaponFiredEvent _event)
    {
        Projectile projectile = projectilePool.RequestObject();
        projectile.Damage = _event.Damage;
    }

    private void OnEnable()
    {
        EventManager.Subscribe(typeof(WeaponFiredEvent), weaponFiredEventHandler);
        //EventManager.Subscribe(typeof(BulletHitEvent), bulletHitEventHandler);
    }


    private void OnDisable()
    {
        EventManager.Unsubscribe(typeof(WeaponFiredEvent), weaponFiredEventHandler);
        //EventManager.Unsubscribe(typeof(BulletHitEvent), bulletHitEventHandler);
    }
}