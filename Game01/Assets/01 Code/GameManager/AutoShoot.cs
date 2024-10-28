using System.Collections;
using TMPro;
using UnityEngine;
using static Weapon;

public class AutoShoot : MonoBehaviour
{
    // Public variables
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private BoxCollider2D _weaponRange;
    [SerializeField] private TextMeshProUGUI[] _ammoTxt;

    // Components
    private Weapon _weapon;
    private WeaponRange _range;
    private BoxCollider2D _bc;


    // Variables
    private float _fireRateTime;
    private float _timer;
    private int _maxAmmo;
    private int _ammo;
    private float _reloadTime;
    private BulletType _bullet;

    // Flags
    private bool _canShoot;
    private bool _inRange;
    private bool _isFireRate;
    private bool _hasAmmo;



    private void OnEnable() => WaveController.OnWaveCompleted += StopAutoShoot;
    private void OnDestroy() => WaveController.OnWaveCompleted -= StopAutoShoot;
    private void StopAutoShoot() => _canShoot = false;

    private void Start()
    {
        _weapon = GetComponent<Weapon>();
        _range = GetComponent<WeaponRange>();
        _bc = GetComponent<BoxCollider2D>();


        _timer = 0;
        _canShoot = true;
        _timer = _fireRateTime;

        if (_weapon != null)
        {
            _fireRateTime = _weapon.GetFireRate();
            _maxAmmo = _ammo = _weapon.GetMagSize();
            _reloadTime = _weapon.GetReloadTime();
            _bullet = _weapon.GetBulletType();
            PrintAmmo(_ammoTxt, _ammo.ToString());
        }
        else Debug.LogError("No weapon found in Soldier!");
    }

    void Update()
    {
        _timer += Time.deltaTime;
        _inRange = _range.IsInRange();
        _isFireRate = _timer >= _fireRateTime;
        _hasAmmo = _ammo > 0;



        if (_canShoot && _isFireRate)
        {
            if (_hasAmmo && _inRange)
            {
                _timer = 0;
                Shoot();
            }
            CanReload();
        }
    }

    private void Shoot()
    {
        GameObject bullet = null;
        if (_bullet == BulletType.Rifle)
            bullet = PoolManager.instance.Pool(PoolData.Type.AssaultBullet);

        else if (_bullet == BulletType.SniperRifle)
            bullet = PoolManager.instance.Pool(PoolData.Type.SniperBullet);


        if (bullet != null)
        {
            // Set position
            bullet.transform.position = _bulletSpawnPoint.position;

            // Update ammo
            _ammo--;
            PrintAmmo(_ammoTxt, _ammo.ToString());

            // Play relevant shooting sound
            if (_bullet == BulletType.Rifle)
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Rifle");

            else if (_bullet == BulletType.SniperRifle)
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Sniper Rifle");

            // Saves new score
            PlayerPrefs.SetInt("TotalShots", PlayerPrefs.GetInt("TotalShots") + 1);
            PlayerPrefs.Save();

            // Fixes bug where it won't detect zombies
            _bc.enabled = false;
            _bc.enabled = true;

            // Fixes bug where it won't detect zombies in range
            _range.SetInRange(false);
        }
    }

    public void CanReload()
    {
        if (_ammo <= 0)
            StartCoroutine(ReloadWeapon(_reloadTime));
    }


    IEnumerator ReloadWeapon(float time)
    {
        // Change text and play sound
        _canShoot = false;
        PrintAmmo(_ammoTxt, "Reloading...");
        AudioManager.instance.PlayOneShot(AudioManager.Category.Weapons, "Reload", "FullReload");

        // Wait some time before weapon is reloaded
        yield return new WaitForSeconds(time);
        Reload();
    }
    private void Reload()
    {
        _ammo = _maxAmmo;
        PrintAmmo(_ammoTxt, _ammo.ToString());
        _canShoot = true;

        // Fixes bug where it won't detect zombies
        _bc.enabled = false;
        _bc.enabled = true;
    }
    private void PrintAmmo(TextMeshProUGUI[] txt, string ammo)
    {
        for (int i = 0; i < txt.Length; i++)
            txt[i].text = ammo.ToString();
    }





}
