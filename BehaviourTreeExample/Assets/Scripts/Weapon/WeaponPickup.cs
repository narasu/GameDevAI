using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickup
{
    private WeaponData weaponData;

    public void Create(WeaponData _weaponData)
    {
        weaponData = _weaponData;
    }

    public object PickUp()
    {
        Destroy(gameObject);
        return weaponData;
    }
}