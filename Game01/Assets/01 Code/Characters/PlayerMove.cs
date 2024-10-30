using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _canMove;
    private Rigidbody2D _rb;
    private bool _canWalk, _canIdle;
    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _canWalk = true;
    }
    
    private void Update()
    {
        if (_canMove)
            Move();
    }

    private void Move()
    {
        if (Input.GetKey(KeyCode.W))
        {
            _rb.linearVelocity = Vector2.up * _speed;
            SetWalkingAnim();
        }
        else if (Input.GetKey(KeyCode.S))
        {
            _rb.linearVelocity = Vector2.down * _speed;
            SetWalkingAnim();
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            SetIdleAnim();
        }
    }

    private void SetWalkingAnim()
    {
        if (_canWalk)
        {
            _canIdle = true;
            _canWalk = false;
            SoldierAnimator.instance.SetAnimation(SoldierAnimations.Names.Moving);
        }
    }

    private void SetIdleAnim()
    {
        if (_canIdle)
        {
            _canWalk = true;
            _canIdle = false;
            SoldierAnimator.instance.SetAnimation(SoldierAnimations.Names.Idle);
        }
    }
}
