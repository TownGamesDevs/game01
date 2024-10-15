using System.Collections;
using TMPro;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private TextMeshProUGUI _ammoTxt;
    [SerializeField] private BoxCollider2D _weaponRange;
    private float _timer;
    private float _fireRateTime;
    private WeaponsClass weapon;
    private int _maxAmmo;
    private int _ammo;
    private float _reloadTime;
    private bool _canShoot;
    private bool _enemyInRange = false; // Track if an enemy is within shooting range

    private void Awake() => WaveManager.OnWaveCompleted += StopAutoShoot;
    private void OnDestroy() => WaveManager.OnWaveCompleted -= StopAutoShoot;

    private void StopAutoShoot() => _canShoot = false;

    private void Start()
    {
        _canShoot = true;
        weapon = GetComponentInChildren<WeaponsClass>();

        if (weapon != null)
        {
            _fireRateTime = weapon.GetFireRate();
            _timer = Random.Range(0f, 0.4f);    // Allows player to shoot with slight delay from each other
            _maxAmmo = _ammo = weapon.GetMagSize();
            _reloadTime = weapon.GetReloadTime();
            UpdateAmmoText(_ammoTxt, _ammo.ToString());
        }
        else Debug.LogError("No weapon found in Soldier!");
    }

    void Update()
    {
        if (weapon != null & _canShoot & _enemyInRange)
        {
            _timer += Time.deltaTime;

            // Only shoot if fire rate has been reached
            if (_timer >= _fireRateTime && _ammo > 0)
            {
                Shoot();
                _timer = 0;
                CanReload();
            }
        }
    }

    private void Shoot()
    {
        GameObject bullet = null;

        // Instantiate a bullet according to soldier type
        if (gameObject.CompareTag("Assault"))
            bullet = PoolManager.instance.Pool(PoolData.Type.AssaultBullet);

        else if (gameObject.CompareTag("Sniper"))
            bullet = PoolManager.instance.Pool(PoolData.Type.SniperBullet);

        if (bullet != null)
        {
            bullet.transform.position = _bulletSpawnPoint.position;
            _ammo--;
            UpdateAmmoText(_ammoTxt, _ammo.ToString());

            // Play sounds
            if (gameObject.CompareTag("Assault"))
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Rifle");
            else if (gameObject.CompareTag("Sniper"))
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Sniper Rifle");
        }
    }

    public void CanReload()
    {
        if (_ammo <= 0)
            StartCoroutine(ReloadWeapon(_reloadTime));
    }

    IEnumerator ReloadWeapon(float time)
    {
        UpdateAmmoText(_ammoTxt, "Reloading...");
        AudioManager.instance.Play(AudioManager.Category.Weapons, "Reload", "FullReload");

        yield return new WaitForSeconds(time);  // Wait some time before weapon is reloaded
        Reload();
    }

    private void Reload()
    {
        _ammo = _maxAmmo;
        UpdateAmmoText(_ammoTxt, _ammo.ToString());
    }

    private void UpdateAmmoText(TextMeshProUGUI txt, string ammo) => txt.text = ammo.ToString();

    // Detect when an enemy enters the shooting range
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _enemyInRange = true;
        }
    }
    // Detect when an enemy exits the shooting range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            _enemyInRange = false;
        }
    }
}
