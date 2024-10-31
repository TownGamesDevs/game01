using UnityEngine;

public class Weapon : MonoBehaviour
{
    public enum BulletType
    {
        Rifle,
        SniperRifle,
    }

    [SerializeField] private BulletType _bulletType;
    public BulletType GetBulletType() => _bulletType;
}
