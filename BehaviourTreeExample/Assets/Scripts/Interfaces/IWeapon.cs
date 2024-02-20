public interface IWeapon
{
    int Ammo { get; }
    float Damage { get; }
    void Fire(float _delta);
}