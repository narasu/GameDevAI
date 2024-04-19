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
        fireRateTimer = new Timer(fireRate);
    }


    // call this method from Update / FixedUpdate
    public bool Fire()
    {
        fireRateTimer.Run(true, out bool isTimerExpired);
        if (isTimerExpired)
        {
            return true;
        }
        return false;
    }
}