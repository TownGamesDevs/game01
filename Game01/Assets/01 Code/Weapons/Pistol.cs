using UnityEngine;

public class Pistol : WeaponsClass
{
    [SerializeField] private float _price;
    [SerializeField] private ReloadTime _reloadTime;
    [SerializeField] private MagSize _magSize;
    [SerializeField] private int _fireRate;
    //[SerializeField] private Accuracy _accuracy;
    //[SerializeField] private BulletDamage _bulletDamage;

    private void Awake()
    {
        WeaponPrice = _price;
        Reload_time = _reloadTime;
        Mag_size = _magSize;
        Fire_rate = _fireRate;
        //Weapon_accuracy = _accuracy;
        //Bullet_damage = _bulletDamage;
    }
}
