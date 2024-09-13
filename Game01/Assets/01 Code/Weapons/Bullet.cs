using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;
    [SerializeField] private int _speed;
    [SerializeField] private float _destroyTime;
    [SerializeField] private int _bulletDamage;
    private float _yPos;
    private float _timeCounter;
    private int _tempDamage;

    private void Awake()
    {
        if (instance == null) { instance = this; }
        
    }
    private void Start()
    {
        // Y axis is stored to prevent bullet of moving with soldier
        _yPos = transform.position.y;

        // Counter
        _timeCounter = 0;

        // Stores original hp
        _tempDamage = _bulletDamage;
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
        _bulletDamage = _tempDamage;

        // Disable gameobject so it can be recalled again from pool manager
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
