
public struct WeaponFiredEvent
{
    public float Damage { get; }

    public WeaponFiredEvent(float _damage)
    {
        Damage = _damage;
    }
}
