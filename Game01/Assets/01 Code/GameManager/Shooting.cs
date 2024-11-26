using UnityEngine;
using static Weapon;

public class Shooting : MonoBehaviour
{ public static Shooting instance;

	// Public variables
	[SerializeField] private Transform _bulletSpawnPoint;
	[SerializeField] private float _roundsPerSec;

	// Private variables
	private Weapon _weapon;
	private ReloadWeapon _reload;
	private float _timer;
	private bool _canShoot;

	// Constants
	const string ERR = "Weapon or ReloadWeapon not set";
	const string SOUND = "Rifle";


	private void Awake() => instance ??= this;

	private void OnEnable() => WaveManager.OnWaveCompleted += StopShooting;
	private void OnDestroy() => WaveManager.OnWaveCompleted -= StopShooting;
	private void StopShooting() => _canShoot = false;

	private void Start()
	{

		_weapon = GetComponent<Weapon>();
		_reload = GetComponent<ReloadWeapon>();
		if (_reload == null || _weapon == null)
			Debug.LogWarning(ERR);
        
		_timer = 0;
		_canShoot = true;
		_timer = 1 / _roundsPerSec;
	}

	private void Update() => CheckCanShoot();

	public void SetCanShoot(bool state) => _canShoot = state;
	public void IncreaseRPS(float speed) => _roundsPerSec += speed;
	

	private void CheckCanShoot()
	{
		// update timer
		_timer += Time.deltaTime;

		// conditions
		bool isKeyPressed = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
		bool timeHasPassed = _timer >= (1 / _roundsPerSec);

		// check conditions
		if (isKeyPressed && timeHasPassed && _canShoot)
		{
			_timer = 0;
			Shoot();
		}
	}

	private void Shoot()
	{
		// get bullet
		GameObject bullet = PoolManager.instance.Pool(PoolTypes.Type.Bullet);
		if (bullet == null) return;  // exit early

		// set position and sprite direction
		bullet.transform.position = _bulletSpawnPoint.position;
		bullet.GetComponent<BulletMove>().ChangeDir(FlipManager.instance.GetDirection());

		// update ammo
		_reload.DecreaseAmmo();  

		// play effects
		PlayEffects();
	}

	private void PlayEffects()
    {
		AudioManager.instance.PlayRandomSound(AudioManager.Category.Weapons, SOUND);
		MuzzleFlash.instance.ShowRandomMuzzleFlash();
		CameraShake.instance.TriggerShake();
	}
}
