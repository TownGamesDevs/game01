using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private int _attackForce;
    [SerializeField] private float _attackSpeed;


    private ZombieMove _zombieMove;
    private BoxCollider2D _collider;
    private ZombieAnimator _animator;
    private float _timer;


    private void Start()
    {
        _zombieMove = GetComponent<ZombieMove>();
        _collider = GetComponent<BoxCollider2D>();
        _animator = GetComponent<ZombieAnimator>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            _zombieMove.SetCanMove(false);
            _timer += Time.deltaTime;

            // Fixes bug where it won't detect wall
            _collider.enabled = false;
            _collider.enabled = true;


            // Can attack?
            if (_timer >= _attackSpeed)
            {
                _timer = 0;
                AttackWall();
            }
        }
    }
    public void AttackWall()
    {
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Other, "Wall");
        Wall.instance.SetHP(_attackForce);
        _animator.SetAnimation(ZombieAnimations.Names.Attack);
    }

}
