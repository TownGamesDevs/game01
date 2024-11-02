using UnityEngine;

public class ZombieFollow : MonoBehaviour
{
    [SerializeField] private Transform player;  // Reference to the player
    [SerializeField] private float speed = 2f;  // Speed of the zombie
    [SerializeField] private float xOffset; // Offset on the X-axis
    [SerializeField] private float yOffset = 1f; // Offset on the Y-axis

    private Rigidbody2D _rb;
    private bool _canMove;


    private void Start()
    {
        _canMove = true;
        _rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        if (_canMove && player != null)
            Move();
    }

    private void Move()
    {
        // Calculate the target position with the offsets
        Vector2 targetPosition = new Vector2(
            player.position.x + xOffset,
            player.position.y + yOffset
        );

        // Calculate direction towards the target position
        Vector2 newPosition = Vector2.MoveTowards(_rb.position, targetPosition, speed * Time.fixedDeltaTime);

        // Move the zombie towards the target position
        _rb.MovePosition(newPosition);
    }

    public void SetCanMove(bool state) => _canMove = state;
}

