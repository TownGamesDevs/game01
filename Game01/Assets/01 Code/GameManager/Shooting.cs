using System.Collections;
using UnityEngine;
using static Weapon;

public class Shooting : MonoBehaviour
{
    // Public variables
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _roundsPerSec;

    // Components
    private Weapon _weapon;
    private ReloadWeapon _reload;

    // Variables
    private float _timer;
    private BulletType _bullet;

    // Flags
    private bool _canShoot;

    private void OnEnable() => WaveController.OnWaveCompleted += StopShooting;
    private void OnDestroy() => WaveController.OnWaveCompleted -= StopShooting;
    private void StopShooting() => _canShoot = false;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        _reload = GetComponent<ReloadWeapon>();

        _timer = 0;
        _canShoot = true;
        _timer = 1 / _roundsPerSec;

        if (_weapon != null)
            _bullet = _weapon.GetBulletType();
        else
            Debug.LogError("No weapon found in Soldier!");
    }

    private void Update()
    {
        HandleShooting();
    }

    public void SetCanShoot(bool state) => _canShoot = state;

    private void HandleShooting()
    {
        _timer += Time.deltaTime;

        // Check if the fire button is held and player can shoot
        if (Input.GetMouseButton(0) && _timer >= (1 / _roundsPerSec) && _canShoot)
        {
            _timer = 0;
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolBullet();
        if (bullet == null) return;  // Exit if no bullet

        bullet.transform.position = _bulletSpawnPoint.position;  // Set bullet position



        // Set bullet direction
        bullet.GetComponent<BulletMove>().ChangeDir(FlipManager.instance.GetDirection());



        _reload.DecreaseAmmo();  // Update ammo
        PlayBulletSound();
        MuzzleFlash.instance.PlayRandomMuzzle();
        CameraShake.instance.TriggerShake(); // cam shake
    }

    private GameObject PoolBullet()
    {
        // Choose a bullet type for each soldier
        if (_bullet == BulletType.Rifle)
            return PoolManager.instance.Pool(PoolData.Type.AssaultBullet);
        else if (_bullet == BulletType.SniperRifle)
            return PoolManager.instance.Pool(PoolData.Type.SniperBullet);

        return null;
    }

    private void PlayBulletSound()
    {
        if (_bullet == BulletType.Rifle)
            AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Rifle");
        else if (_bullet == BulletType.SniperRifle)
            AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Sniper Rifle");
    }
}
