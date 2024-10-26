using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum BulletType
    {
        Rifle,
        SniperRifle,
        // Add more as needed
    }

    [SerializeField] private BulletType _bulletType;
    [SerializeField] private int _reloadTime;
    [SerializeField] private int _magSize;
    [SerializeField] private float _fireRate;

    public int GetReloadTime() => _reloadTime;
    public int GetMagSize() => _magSize;
    public float GetFireRate() => 1 / _fireRate;
    public BulletType GetBulletType() => _bulletType;
}
