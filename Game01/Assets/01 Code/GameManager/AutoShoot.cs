using System.Collections;
using TMPro;
using UnityEngine;

public class AutoShoot : MonoBehaviour
{
    // Variables
    [SerializeField] private Transform _bulletSpawnPoint;
    [SerializeField] private TextMeshProUGUI _ammoTxt;
    private float _timer;
    private float _fireRateTime;
    private WeaponsClass weapon;
    private int _maxAmmo;
    private int _ammo;
    private float _reloadTime;
    private bool _canAutoShoot;

    private void Awake()
    {
        // No autoshoot if wave was completed
        WaveManager.OnWaveCompleted += StopAutoShoot;
    }

    private void OnDestroy()
    {
        // Unsub from action
        WaveManager.OnWaveCompleted -= StopAutoShoot;
    }

    private void StopAutoShoot()
    {
        // Listens to action...
        _canAutoShoot = false;
    }


    private void Start()
    {
        // Allows autoshoot
        _canAutoShoot = true;

        // Gets the parent class
        weapon = GetComponentInChildren<WeaponsClass>();

        if (weapon != null)
        {

            _fireRateTime = weapon.GetFireRate();
            _timer = Random.Range(0f, 0.4f);    // Allows player to shoot with slight delay from each other
            _maxAmmo = _ammo = weapon.GetMagSize();
            _reloadTime = weapon.GetReloadTime();
            UpdateAmmoText(_ammoTxt, _ammo.ToString());
            //_canReload = false;
        }
        else Debug.LogError("No weapon found in Soldier!");
    }



    void Update()
    {
        if (weapon != null & _canAutoShoot)
        {
            // Update timer
            _timer += Time.deltaTime;

            // Can only shoot if enemy is in range and fire rate time has been reached
            if (_timer >= _fireRateTime & _ammo > 0)
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
            bullet = PoolManager.instance.PoolAssaultBullet();

        else if (gameObject.CompareTag("Sniper"))
            bullet = PoolManager.instance.PoolSniperBullet();


        // Set bullet position and update ammo
        if (bullet != null)
        {
            // Set bullet position
            bullet.transform.position = _bulletSpawnPoint.position;

            // Update ammo
            _ammo--;

            // Update ammo text on screen
            UpdateAmmoText(_ammoTxt, _ammo.ToString());

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


}
