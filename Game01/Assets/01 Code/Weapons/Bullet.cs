using UnityEngine;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;
    [SerializeField] private int _speed;

    // Add this new variable to control the maximum distance
    [SerializeField] private float _maxDistance;

    // Bullet damage limits
    [SerializeField] private int minDamage = 5; // Minimum damage value
    [SerializeField] private int maxDamage = 15; // Maximum damage value

    private int _bulletDamage;      // Current bullet damage
    private Vector2 _startPosition; // Track the starting position of the bullet

    private void Awake() => instance ??= this;

    private void Start()
    {
        // Record the bullet's starting position
        _startPosition = transform.position;

        // Set maxDamage to be inclusive
        maxDamage++; // Adds one more so max can be reached in Random.Range below

        // Initialize bullet damage with a random value within the range
        _bulletDamage = Random.Range(minDamage, maxDamage);
    }

    private void Update()
    {
        Move();
        CheckDistanceTraveled(); // Check if the bullet has traveled the max distance
    }

    private void Move()
    {
        // Bullet moves along the X axis only
        transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
    }

    // New method to check if the bullet has traveled the specified max distance
    private void CheckDistanceTraveled()
    {
        float distanceTraveled = Vector2.Distance(_startPosition, transform.position);
        if (distanceTraveled >= _maxDistance)
        {
            Die(); // Destroy the bullet once it travels the specified distance
        }
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

    public int GetBulletDamage() => _bulletDamage;

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
        // Reset bullet damage with a new random value
        _bulletDamage = Random.Range(minDamage, maxDamage);

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
