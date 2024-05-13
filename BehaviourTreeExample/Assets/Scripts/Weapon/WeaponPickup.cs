using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        other.GetComponent<IWeaponUser>()?.PickUp();
    }
}