using System.Collections;
using TMPro;
using UnityEngine;
using static Weapon;

public class AutoShoot : MonoBehaviour
{
    // Public variables
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private BoxCollider2D _weaponRange;
    [SerializeField] private float _roundsPerSec;

    // Components
    private Weapon _weapon;
    private WeaponRange _range;
    private ReloadWeapon _reload;


    // Variables
    private float _timer;
    private BulletType _bullet;

    // Flags
    private bool _canShoot;


    private void OnEnable() => WaveController.OnWaveCompleted += StopAutoShoot;
    private void OnDestroy() => WaveController.OnWaveCompleted -= StopAutoShoot;
    private void StopAutoShoot() => _canShoot = false;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        _range = GetComponent<WeaponRange>();
        _reload = GetComponent<ReloadWeapon>();

        _timer = 0;
        _canShoot = true;
        _roundsPerSec /= 100;
        _timer = _roundsPerSec;

        if (_weapon != null)
            _bullet = _weapon.GetBulletType();
        else
            Debug.LogError("No weapon found in Soldier!");
    }
    public void SetCanShoot(bool state) => _canShoot = state;

    void Update()
    {
        _timer += Time.deltaTime;

        if (_canShoot && _timer >= _roundsPerSec && _range.IsInRange())
        {
            _timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolBullet();
        if (bullet == null) return; // Exit if no bullet

        bullet.transform.position = _bulletSpawnPoint.position; // Set bullet position
        _reload.DecreaseAmmo(); // Update ammo

        PlayReloadSound();
        SaveScore();

        // Fixes bug where it won't detect zombies in range
        _range.SetInRange(false);
    }


    private GameObject PoolBullet()
    {
        // Choose a bullet for each soldier
        if (_bullet == BulletType.Rifle)
            return PoolManager.instance.Pool(PoolData.Type.AssaultBullet);
        else if (_bullet == BulletType.SniperRifle)
            return PoolManager.instance.Pool(PoolData.Type.SniperBullet);

        return null;
    }

    private void PlayReloadSound()
    {
        if (_bullet == BulletType.Rifle)
            AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Rifle");
        else if (_bullet == BulletType.SniperRifle)
            AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Sniper Rifle");
    }

    private void SaveScore()
    {
        // Saves new score
        PlayerPrefs.SetInt("TotalShots", PlayerPrefs.GetInt("TotalShots") + 1);
        PlayerPrefs.Save();
    }

    

}
