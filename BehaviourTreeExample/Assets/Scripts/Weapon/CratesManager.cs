using System;
using System.Collections.Generic;
using UnityEngine;

public class CratesManager : MonoBehaviour
{
    private WeaponPickup[] allCrates;

    private void Awake()
    {
        allCrates = FindObjectsOfType<WeaponPickup>();
        ServiceLocator.Provide(Strings.WeaponCrates, allCrates);
    }
}
