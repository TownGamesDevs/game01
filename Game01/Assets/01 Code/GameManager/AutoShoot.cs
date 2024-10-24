using System.Collections;
using TMPro;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private BoxCollider2D _weaponRange;
    [SerializeField] private TextMeshProUGUI[] _ammoTxt;
    private float _timer;
    private float _fireRateTime;
    private WeaponsClass weapon;
    private int _maxAmmo;
    private int _ammo;
    private float _reloadTime;
    private bool _canShoot;
    private bool _enemyInRange = false; // Track if an enemy is within shooting range
    private BoxCollider2D _boxCollider;

    private void Awake() => WaveManager.OnWaveCompleted += StopAutoShoot;
    private void OnDestroy() => WaveManager.OnWaveCompleted -= StopAutoShoot;

    private void StopAutoShoot() => _canShoot = false;

    private void Start()
    {
        _canShoot = true;
        weapon = GetComponentInChildren<WeaponsClass>();
        _boxCollider = GetComponent<BoxCollider2D>();

        if (weapon != null)
        {
            _fireRateTime = weapon.GetFireRate();
            //_timer = Random.Range(0f, 0.4f);    // Allows player to shoot with slight delay from each other
            _timer = _fireRateTime;
            _maxAmmo = _ammo = weapon.GetMagSize();
            _reloadTime = weapon.GetReloadTime();
            PrintAmmo(_ammoTxt, _ammo.ToString());
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

        // Instantiate bullet according to soldier type
        if (gameObject.CompareTag("Assault"))
            bullet = PoolManager.instance.Pool(PoolData.Type.AssaultBullet);

        else if (gameObject.CompareTag("Sniper"))
            bullet = PoolManager.instance.Pool(PoolData.Type.SniperBullet);

        if (bullet != null)
        {
            // Set position
            bullet.transform.position = _bulletSpawnPoint.position;

            // Update ammo
            _ammo--;
            PrintAmmo(_ammoTxt, _ammo.ToString());

            // Play relevant shooting sound
            if (gameObject.CompareTag("Assault"))
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Rifle");
            else if (gameObject.CompareTag("Sniper"))
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, "Sniper Rifle");

            // Saves new score
            PlayerPrefs.SetInt("TotalShots", PlayerPrefs.GetInt("TotalShots") + 1);
            PlayerPrefs.Save();

            // Fixes bug where player won't detect zombies inside range
            _boxCollider.enabled = false;
            _boxCollider.enabled = true;
        }
    }

    public void CanReload()
    {
        if (_ammo <= 0) StartCoroutine(ReloadWeapon(_reloadTime));
    }

    IEnumerator ReloadWeapon(float time)
    {
        // Change text and play sound
        PrintAmmo(_ammoTxt, "Reloading...");
        AudioManager.instance.Play(AudioManager.Category.Weapons, "Reload", "FullReload");

        // Wait some time before weapon is reloaded
        yield return new WaitForSeconds(time);  
        Reload();
    }

    private void Reload()
    {
        _ammo = _maxAmmo;
        PrintAmmo(_ammoTxt, _ammo.ToString());
    }

    private void PrintAmmo(TextMeshProUGUI[] txt, string ammo)
    {
        for (int i = 0; i < txt.Length; i++)
            txt[i].text = ammo.ToString();
    }

    // Detect when an enemy enters the shooting range
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            _enemyInRange = true;
    }
    // Detect when an enemy exits the shooting range
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            _enemyInRange = false;
    }
}
