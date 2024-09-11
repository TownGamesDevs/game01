using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Bullet : MonoBehaviour
{
    public static Bullet instance;
    [SerializeField] private int _speed;
    [SerializeField] private int _destroyTime;
    [SerializeField] private int _damage;
    private float _yPos;
    private float _counter;

    private void Awake()
    {
        if (instance == null) { instance = this; }
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

            // Get enemy HP
            if (collision.gameObject.TryGetComponent<ZombieClass>(out ZombieClass current_zombie))
            {
                int tmpDamage;
                tmpDamage = _damage - current_zombie.GetHP();

                if (tmpDamage <= 0)
                {
                    // destroy bullet
                    _counter = 0;
                    gameObject.SetActive(false);
                }
            }
            else
                print("Couldn't get component ZombieClass (Bullet.cs -> OnTriggerEnter2D");

        }
    }

    public int GetHP()
    {
        return _damage;
    }
}
