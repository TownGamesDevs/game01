using UnityEngine;

public class Pistol : WeaponsClass
{
    [SerializeField] private float _price;
    [SerializeField] private ReloadTime _reloadTime;
    [SerializeField] private MagSize _magSize;
    [SerializeField] private int _fireRate;

    private void Awake()
    {
        Reload_time = _reloadTime;
        Mag_size = _magSize;
        Fire_rate = _fireRate;
    }
}
