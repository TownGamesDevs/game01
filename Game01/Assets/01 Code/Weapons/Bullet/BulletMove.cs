using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _canMove;

    private Rigidbody2D _rb;
    private void Start() => _rb = GetComponent<Rigidbody2D>();
    
    void FixedUpdate() => Move();

    private void Move()
    {
        if (_canMove)
            _rb.linearVelocity = Vector2.right * _speed;
    }
}
