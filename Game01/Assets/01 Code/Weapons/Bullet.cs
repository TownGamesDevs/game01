using UnityEngine;

public class Bullet : MonoBehaviour
{
	public static Bullet instance;
	[SerializeField] private int _speed;
	[SerializeField] private int _destroyTime;
	[SerializeField] private int _bulletDamage;
	private float _yPos;
	private float _counter;
	private int tempDamage;

	private void Awake()
	{
		if (instance == null) { instance = this; }
		tempDamage = _bulletDamage;
	}
	private void Start()
	{
		// Y axis is stored to prevent bullet of moving with soldier
		_yPos = transform.position.y;

		// Counter
		_counter = 0;

	}
	private void Update()
	{
		// Constantly moves bullet
		Move();

		// Check if bullet should be dead
		DestroyBullet();

	}

	private void Move()
	{
		// Bullet moves in fixed Y axis so it doesn't move with player
		transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, _yPos);
	}
	private void DestroyBullet()
	{
		_counter += Time.deltaTime;
		if (_counter >= _destroyTime)
		{
			_counter = 0;
			gameObject.SetActive(false);
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Checks if bullet has collided with an enemy
		if (collision.gameObject.CompareTag("Enemy"))
		{
			// Subtract enemy hp from bullet hp
			if (collision.gameObject.TryGetComponent<ZombieClass>(out ZombieClass current_zombie))
			{
                SetHP(CalculateZombieDamage(collision.gameObject));
            }


			//// Get enemy HP
			//if (collision.gameObject.TryGetComponent<ZombieClass>(out ZombieClass current_zombie))
			//{
			//    int tmpDamage;
			//    tmpDamage = _damage - current_zombie.GetHP();

			//    if (tmpDamage <= 0)
			//    {
			//        // destroy bullet
			//        _counter = 0;
			//        gameObject.SetActive(false);
			//    }
			//}
			//else
			//    print("Couldn't get component ZombieClass (Bullet.cs -> OnTriggerEnter2D");


		}
	}

	public int GetHP()
	{
		return _bulletDamage;
	}

	public void SetHP(int hp)
	{
		// Updates the actual HP variable
		if (hp < _bulletDamage)
			_bulletDamage = hp;
		else
			_bulletDamage--;

		// Checks if zombie died
		if (_bulletDamage <= 0)
			Die();
	}

	public void Die()
	{
		_counter = 0;
		_bulletDamage = tempDamage;
		gameObject.SetActive(false);
	}

	private int CalculateZombieDamage(GameObject enemy)
	{
        // Get enemy HP
        if (enemy.TryGetComponent<ZombieClass>(out ZombieClass currentEnemy))
            return _bulletDamage - currentEnemy.GetHP();

        return -1;
    }
}
