using UnityEngine;

public struct WeaponSpawnedEvent
{
    public Transform WeaponTransform { get; }

    public WeaponSpawnedEvent(Transform _weaponTransform)
    {
        WeaponTransform = _weaponTransform;
    }
}
