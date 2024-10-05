using UnityEngine;

public class ZombieAnimator : MonoBehaviour
{
    public static ZombieAnimator instance;

    [SerializeField] private SpriteRenderer _spriteRenderer;

    // Set the different animation frames
    [SerializeField] private float _walkSpeed = .02f;
    [SerializeField] private float _attackSpeed = .02f;
    [SerializeField] private float _idleSpeed = .02f;
    [SerializeField] private float _attackDelay = 0.5f;


    [SerializeField] private Sprite[] _attackFrame, _deadFrame, _idleFrame, _walkFrame;
    [SerializeField] private bool _canPlayAnim, _canWalk, _canAttack, _isDead;


    const int NO_DELAY = 0;
    private float _counter;
    private int _frame;
    private float _delay;


    private void Awake()
    { if (instance == null) instance = this; }
    private void Start()
    {
        Wall.OnWallDestroyed += SetWalkAnim;
        SetWalkAnim();
    }
    private void Update()
    {
        if (_canPlayAnim)
        {
            if (_canWalk)
                ZombieAnim(_walkFrame, _walkSpeed, NO_DELAY);

            else if (_canAttack)
                ZombieAnim(_attackFrame, _attackSpeed, _attackDelay);

            else
                ZombieAnim(_idleFrame, _idleSpeed, NO_DELAY);
        }
    }


    private void ZombieAnim(Sprite[] sprite, float speed, float delay)
    {
        // Error check
        if (sprite == null || sprite.Length == 0)
        {
            ErrorManager.instance.PrintError("Sprite " + sprite + " is null or zero");
            return;
        }

        // Change frames after some time
        _counter += Time.deltaTime;
        if (_counter >= speed && _delay <= 0)
        {
            _counter = 0;

            _spriteRenderer.sprite = sprite[_frame];
            _frame++;

            if (_frame >= sprite.Length)
            {
                _frame = 0;
                _delay = delay;
            }
        }

        // Wait some time before playing first frame again
        if (_delay > 0)
            _delay -= Time.deltaTime;
    }

    public void SetWalkAnim()
    {
        if (!_canWalk)
        {
            _canWalk = true;
            _canAttack = false;
            ResetValues();
        }
    }

    public void SetAttackAnim()
    {
        if (!_canAttack)
        {
            _canWalk = false;
            _canAttack = true;
            ResetValues();
        }
    }

    private void ResetValues()
    {
        _frame = 0;
        _counter = 0;
        _delay = 0;
    }

}
