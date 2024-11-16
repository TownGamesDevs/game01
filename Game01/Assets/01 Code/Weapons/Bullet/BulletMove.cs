using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public static BulletMove instance;

    [SerializeField] private float _speed;
    [SerializeField] private bool _canMove;

    private SpriteRenderer _sr;
    private Rigidbody2D _rb;
    private Vector2 _dir;

    private void Awake() => instance ??= this;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _sr = GetComponent<SpriteRenderer>();
    }
    

    void FixedUpdate() => Move();

    private void Move()
    {
        if (_canMove)
            _rb.linearVelocity = _dir * _speed;        
    }

    public void ChangeDir(bool isRight)
    {
        if (isRight)
        {
            _dir = Vector2.right;
            if (_sr != null)
                _sr.flipX = false;

        }
        else if (!isRight)
        {
            _dir = Vector2.left;
            if (_sr != null)
                _sr.flipX = true;
        }
    }
}
