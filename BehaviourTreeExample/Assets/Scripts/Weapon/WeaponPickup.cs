using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour, IPickup
{
    [SerializeField] private WeaponData WeaponDataAsset;
    
    public WeaponData PickUp()
    {
        return WeaponDataAsset;
    }
}