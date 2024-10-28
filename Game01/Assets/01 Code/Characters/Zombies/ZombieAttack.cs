using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private int _attackForce;
    [SerializeField] private float _attackSpeed;


    private ZombieMove _zombieMove;
    private BoxCollider2D _collider;
    private ZombieAnimator _animator;
    private float _timer;
    private bool _canAttack;


    private void Start()
    {
        _zombieMove = GetComponent<ZombieMove>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<ZombieAnimator>();
    }
    private void OnEnable()
    {
        _canAttack = false;
        _timer = 0;
    }

    private void Update()
    {
        if (_canAttack)
        {
            _timer += Time.deltaTime;

            if (_timer >= _attackSpeed)
            {
                _timer = 0;
                AttackWall();
                
                // Fixes bug where it won't detect wall
                _collider.enabled = false;
                _collider.enabled = true;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _canAttack = false;
            _zombieMove.SetCanMove(false);
        }
    }
    public void AttackWall()
    {
        Wall.instance.SetHP(_attackForce);
        _animator.SetAnimation(ZombieAnimations.Names.Attack);
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Other, "Wall");
    }

}
