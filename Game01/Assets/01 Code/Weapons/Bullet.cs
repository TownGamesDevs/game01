using UnityEngine;

public class Bullet : MonoBehaviour
{
	public static Bullet instance;
	[SerializeField] private int _speed;
	[SerializeField] private float _destroyTime;

	// Bullet damage limits
	[SerializeField] private int minBulletDamage = 5; // Minimum damage value
	[SerializeField] private int maxBulletDamage = 15; // Maximum damage value

	private int _bulletDamage;      // Current bullet damage
	private float _timeCounter;

	private void Awake() => instance ??= this;

	private void Start()
	{
		// Counter
		_timeCounter = 0;

		maxBulletDamage++;	// Adds one more so max can be reached in Random.Range below

        // Initialize bullet damage with a random value within the range
        _bulletDamage = Random.Range(minBulletDamage, maxBulletDamage);
	}

	private void Update()
	{
		Move();
		AutoDestroyBullet();
	}

	private void Move()
	{
		// Bullet moves in fixed Y axis so it doesn't move with player
		transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
	}

	private void AutoDestroyBullet()
	{
		_timeCounter += Time.deltaTime;
		if (_timeCounter >= _destroyTime)
			Die();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Checks if bullet has collided with an enemy
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Set bullet HP by subtracting enemy hp from bullet hp
			if (collision.gameObject.TryGetComponent<ZombieClass>(out ZombieClass current_zombie))
				SetBulletDamage(CalculateZombieDamage(current_zombie.gameObject));
		}
	}

	public int GetBulletDamage()
	{
		return _bulletDamage;
	}

	public void SetBulletDamage(int hp)
	{
		// Set bullet HP
		if (hp < _bulletDamage)
			_bulletDamage = hp;
		else
			_bulletDamage--;

		// Bullet died?
		if (_bulletDamage <= 0)
			Die();
	}

	public void Die()
	{
		// Restart variables
		_timeCounter = 0;

		// Reset bullet damage with a new random value
		_bulletDamage = Random.Range(minBulletDamage, maxBulletDamage);

		// Disable gameobject so it can be recalled again from pool manager
		gameObject.SetActive(false);
	}

	private int CalculateZombieDamage(GameObject enemy)
	{
		// Get enemy HP
		if (enemy.TryGetComponent<ZombieClass>(out ZombieClass currentEnemy))
			return (int)(_bulletDamage - currentEnemy.GetHP());

		return -999;
	}
}
