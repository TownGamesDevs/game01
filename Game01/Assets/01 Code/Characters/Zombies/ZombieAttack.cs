using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private int _attackForce;
    [SerializeField] private float _attackSpeed;


    private ZombieMove _zombieMove;
    private ZombieAnimator _animator;
    private float _timer;
    private bool _canAttack;


    private void Start()
    {
        _zombieMove = GetComponent<ZombieMove>();
        _animator = GetComponent<ZombieAnimator>();
    }
    private void OnEnable()
    {
        _canAttack = false;
        _timer = 0;
    }

    private void FixedUpdate()
    {
        if (_canAttack)
        {
            _timer += Time.deltaTime;

            if (_timer >= _attackSpeed)
            {
                _timer = 0;
                AttackWall();
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _canAttack = true;
            _zombieMove.SetCanMove(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _canAttack = false;
        _zombieMove.SetCanMove(true);
    }

    public void AttackWall()
    {
        _animator.SetAnimation(ZombieAnimations.Names.Attack);
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Other, "Wall");
        Wall.instance.SetHP(_attackForce);
    }

}
