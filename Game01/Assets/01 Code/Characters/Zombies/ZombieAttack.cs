using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private int _attackForce;
    [SerializeField] private float _attackSpeed;


    private ZombieFollow _move;
    private ZombieAnimator _animator;
    private float _timer;
    private bool _canAttack;


    private void Start()
    {
        _move = GetComponent<ZombieFollow>();
        _animator = GetComponent<ZombieAnimator>();
    }
    private void OnEnable()
    {
        _canAttack = false;
        _timer = _attackSpeed;
    }

    private void Update()
    {
        if (_canAttack)
        {
            _timer += Time.deltaTime;

            if (_timer >= _attackSpeed)
            {
                _timer = 0;
                Attack();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canAttack = true;
            _move.SetCanMove(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _canAttack = false;
        _timer = _attackSpeed;
        _move.SetCanMove(true);
        _animator.SetAnimation(ZombieAnimations.Names.Walk);
    }

    public void Attack()
    {
        _animator.SetAnimation(ZombieAnimations.Names.Attack);
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Die");
        PlayerHealth.instance.SetPlayerHealth(_attackForce);
    }

}
