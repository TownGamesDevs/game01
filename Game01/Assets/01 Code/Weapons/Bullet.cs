using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;
    [SerializeField] private int _speed;
    [SerializeField] private float _destroyTime;
    [SerializeField] private float _bulletDamage;
    private float _timeCounter;
    private float _initialDamage;

    private void Awake()
    {
        if (instance == null) { instance = this; } 
    }

    private void Start()
    {
        // Counter
        _timeCounter = 0;

        // Stores original hp
        _initialDamage = _bulletDamage;
    }
    private void Update()
    {
        // Constantly moves bullet
        Move();

        // Check if bullet should be dead
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
            // Subtract enemy hp from bullet hp
            if (collision.gameObject.TryGetComponent<ZombieClass>(out ZombieClass current_zombie))
                SetBulletDamage(CalculateZombieDamage(collision.gameObject));
        }
    }

    public float GetBulletDamage()
    {
        return _bulletDamage;
    }

    public void SetBulletDamage(float hp)
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
        _bulletDamage = _initialDamage;

        // Disable gameobject so it can be recalled again from pool manager
        gameObject.SetActive(false);
    }

    private float CalculateZombieDamage(GameObject enemy)
    {
        // Get enemy HP
        if (enemy.TryGetComponent<ZombieClass>(out ZombieClass currentEnemy))
            return _bulletDamage - currentEnemy.GetHP();

        return -999;
    }
}
