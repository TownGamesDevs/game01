using System.Collections;
using UnityEngine;
using static Weapon;

public class Shooting : MonoBehaviour
{
    // Public variables
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private float _initialRoundsPerSec = 10f;  // Starting rounds per second
    [SerializeField] private float _minRoundsPerSec = 5f;       // Minimum rounds per second for faster shooting
    [SerializeField] private float _increaseRate = 0.1f;        // Rate at which fire rate increases

    // Components
    private Weapon _weapon;
    private ReloadWeapon _reload;

    // Variables
    private float _timer;
    private float _currentRoundsPerSec;
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
        _currentRoundsPerSec = _initialRoundsPerSec;
        _timer = 1 / _currentRoundsPerSec;

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
        if (Input.GetMouseButton(0) && _timer >= (1 / _currentRoundsPerSec) && _canShoot)
        {
            _timer = 0;
            Shoot();

            // Increase fire rate by reducing _currentRoundsPerSec, clamping it to the minimum allowed
            _currentRoundsPerSec = Mathf.Max(_minRoundsPerSec, _currentRoundsPerSec - _increaseRate);
        }

        // Reset fire rate if fire button is released
        if (Input.GetMouseButtonUp(0) || !_canShoot)
        {
            _currentRoundsPerSec = _initialRoundsPerSec;
        }
    }

    private void Shoot()
    {
        GameObject bullet = PoolBullet();
        if (bullet == null) return;  // Exit if no bullet

        bullet.transform.position = _bulletSpawnPoint.position;  // Set bullet position
        _reload.DecreaseAmmo();  // Update ammo
        PlayBulletSound();
        MuzzleFlash.instance.PlayRandomMuzzle();
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
