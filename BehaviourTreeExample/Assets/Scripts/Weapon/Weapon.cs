using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private WeaponData WeaponDataAsset;
    private int ammo;
    private float damage;
    private float fireRate;

    private Timer fireRateTimer;

    private void Awake()
    {
        fireRate = WeaponDataAsset.FireRate;
        fireRateTimer = new Timer(1.0f);
    }

    public void Fire()
    {
        fireRateTimer.Run(true, out bool isTimerExpired);
        if (isTimerExpired)
        {
            ammo -= 1;
            //EventManager.Invoke(new WeaponFiredEvent(Damage));

            if (ammo <= 0)
            {
                //EventManager.Invoke(new WeaponOutOfAmmoEvent());
            }
        }
    }
}