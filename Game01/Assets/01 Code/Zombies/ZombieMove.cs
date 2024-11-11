using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _canMove;

    private Rigidbody2D _rb;

    private void Start()
    {
        _canMove = true;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void OnEnable() => Wall.OnWallDestroyed += EnableMove;
    private void OnDisable() => Wall.OnWallDestroyed -= EnableMove;

    private void FixedUpdate() => Move();  // Use FixedUpdate for physics-based movement

    public void Move()
    {
        if (_canMove)
            _rb.linearVelocity = Vector2.left * _speed; // Moves to the left with specified speed
        else
            _rb.linearVelocity = Vector2.zero;  // Stops movement when _canMove is false
    }

    public void SetCanMove(bool state) => _canMove = state;
    public void EnableMove() => _canMove = true;
}
