public struct WeaponPickedUpEvent
{
    public WeaponData WeaponDataAsset;

    public WeaponPickedUpEvent(WeaponData _weapon)
    {
        WeaponDataAsset = _weapon;
    }
}
