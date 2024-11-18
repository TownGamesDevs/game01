using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    [SerializeField] private int _attackForce;
    [SerializeField] private float _attackSpeed;


    private ZombieFollow _follow;
    private ZombieAnimator _anim;
    private float _timer;
    private bool _canAttack;


    private void Start()
    {
        _follow = GetComponent<ZombieFollow>();
        _anim = GetComponent<ZombieAnimator>();
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
            _canAttack = true;
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _canAttack = false;
            _timer = _attackSpeed;
            _anim.SetAnimation(ZombieAnimations.Names.Walk);
        }
    }

    public void Attack()
    {
        // Attack animation
        _anim.SetAnimation(ZombieAnimations.Names.Attack);

        // Attack sound
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Die");

        // Update player HP
        PlayerHealth.instance.SetPlayerHealth(_attackForce);
    }

}
