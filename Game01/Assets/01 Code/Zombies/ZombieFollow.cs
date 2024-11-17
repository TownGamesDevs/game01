using UnityEngine;

public class ZombieFollow : MonoBehaviour
{
    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _xOffset, _yOffset;

    private Rigidbody2D _rb;
    private float _speed;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = Random.Range(_minSpeed, _maxSpeed);
    }
    private void FixedUpdate() => FollowPlayer();
    
    private void FollowPlayer()
    {
        Vector2 targetPosition = new(
            PlayerPosition.instance.GetPlayerPos().x + _xOffset,
            PlayerPosition.instance.GetPlayerPos().y + _yOffset
        );

        Vector2 newPosition = Vector2.MoveTowards(_rb.position, targetPosition, _speed * Time.fixedDeltaTime);
        _rb.MovePosition(newPosition);
    }
}

