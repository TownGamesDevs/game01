using UnityEngine;

public class PlayerSpeed : MonoBehaviour
{ public static PlayerSpeed instance;

    [SerializeField] private float _speed;
    [SerializeField] private float _speedWhileShooting;

    private float _actualSpeed;
    private Rigidbody2D _rb;
    private bool _canWalk, _canIdle;
    private Vector2 direction;
    private bool _isUp, _isDown, _isLeft, _isRight;

    private void Awake() => instance ??= this;

    private void Start()
    {
        _actualSpeed = _speed;
        _rb = GetComponent<Rigidbody2D>();
        _canWalk = true;
    }

    private void Update()
    {
        Move();

        // Set speed
        if ((Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0)) && !ReloadWeapon.instance.IsReloading())
            SpeedWhileShooting();
        else
            OriginalSpeed();
    }
    private void Move()
    {
        HandleInput();
        direction = Vector2.zero;

        // Horizontal movement priority
        if (_isRight && !_isLeft)
            direction.x = 1;
        
        if (_isLeft && !_isRight)
            direction.x = -1;
        

        // Vertical movement priority
        if (_isUp && !_isDown)
            direction.y = 1;
        else if (_isDown && !_isUp)
            direction.y = -1;


        // Move the player in the calculated direction
        if (direction != Vector2.zero)
        {
            _rb.linearVelocity = direction.normalized * _actualSpeed;
            SetWalkingAnim();
        }
        else
        {
            _rb.linearVelocity = Vector2.zero;
            SetIdleAnim();
        }
    }

    private void HandleInput()
    {
        // Track key pressed state for each direction
        _isUp = Input.GetKey(KeyCode.W);
        _isDown = Input.GetKey(KeyCode.S);
        _isLeft = Input.GetKey(KeyCode.A);
        _isRight = Input.GetKey(KeyCode.D);
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
    public void SpeedWhileShooting() => _actualSpeed = _speedWhileShooting;
    public void OriginalSpeed() => _actualSpeed = _speed;

    public void IncreaseSpeed(float speed )
    {
        _speed += speed;
        _speedWhileShooting += speed;
    }
}
