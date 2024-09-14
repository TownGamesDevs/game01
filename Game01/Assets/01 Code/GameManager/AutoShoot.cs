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
	private int _currentAmmo;
	private float _reloadTime;
	private bool _canReload;
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
			// Allows player to shoot first round before fire rate time has been reached
			_timer = _fireRateTime = weapon.GetFireRate();
			_maxAmmo = _currentAmmo = weapon.GetMagSize();
			_reloadTime = weapon.GetReloadTime();
			UpdateAmmoText(_ammoTxt, _currentAmmo.ToString());
			_canReload = false;
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
			if (_timer >= _fireRateTime & _currentAmmo > 0)
			{
				Shoot();

				// Resets variables after shot
				_canReload = true;
				_timer = 0;
			}
			CheckAmmo();
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
			bullet.transform.position = new Vector2(_bulletSpawnPoint.position.x, _bulletSpawnPoint.position.y);

			// Update ammo
			_currentAmmo--;

			// Update ammo text on screen
			UpdateAmmoText(_ammoTxt, _currentAmmo.ToString());
		}
	}




	public void CheckAmmo()
	{
		if (_currentAmmo <= 0 & _canReload)
		{
			_canReload = false; // Set to false so it reloads only once and not every frame
			UpdateAmmoText(_ammoTxt, "Reloading...");
			StartCoroutine(ReloadWeapon(_reloadTime));
		}
	}
	IEnumerator ReloadWeapon(float time)
	{
		// Wait time before weapon is reloaded

		yield return new WaitForSeconds(time);
		Reload();
	}
	private void Reload()
	{
		_currentAmmo = _maxAmmo;
		UpdateAmmoText(_ammoTxt, _currentAmmo.ToString());
	}

	private void UpdateAmmoText(TextMeshProUGUI txt, string ammo)
	{
		txt.text = ammo.ToString();
	}


}
