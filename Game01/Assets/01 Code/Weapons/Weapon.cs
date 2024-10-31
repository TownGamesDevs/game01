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
    public BulletType GetBulletType() => _bulletType;
}
