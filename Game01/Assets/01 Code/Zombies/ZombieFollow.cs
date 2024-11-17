using UnityEngine;

public class ZombieFollow : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;  // Speed of the zombie
    [SerializeField] private float _xOffset; // Offset on the X-axis
    [SerializeField] private float _yOffset = 1f; // Offset on the Y-axis

    private Rigidbody2D _rb;


    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        FollowPlayer();
    }

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

