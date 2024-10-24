using TMPro;
using UnityEngine;

public class Zombie : ZombieClass
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackTime;
    [SerializeField] private TextMeshProUGUI _healthTxt;

    private ZombieAnimator _zombieAnimator;
    private bool _canWalk;
    private float _timer;

    private void Awake() => Wall.OnWallDestroyed += CanWalk;
    private new void Start()
    {
        _zombieAnimator = GetComponent<ZombieAnimator>();
        base.Start();

        AttackForce = _attackForce;
        AttackTime = _attackTime;
        _timer = 0;
        PrintZombieHP();
    }
    private void OnDestroy() => Wall.OnWallDestroyed -= CanWalk;

    private void Update()
    {
        if (_canMove)
            ZombieMove();
        
        CheckCanAttackWall();
    }

    public void CheckCanAttackWall()
    {
        if (GetCanAttackWall() && !_canWalk)
        {
            _timer += Time.deltaTime;

            if (_timer >= AttackTime)
            {
                AttackWall();
                _timer = 0;
            }
        }
    }

    public void CanWalk() => _canWalk = true;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            SetCanMove(false);
            SetCanAttackWall(true);
            _canWalk = false;
        }

        if (collision.CompareTag("Bullet"))
        {
            if (collision.TryGetComponent<Bullet>(out Bullet bullet))
            {
                // Play hit sound
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "BulletHit");

                // Play hit animation
                _zombieAnimator.PlayHitAnimation();

                // Calculate and set damage
                float bulletDamage = bullet.GetBulletDamage();
                DamagePointsManager.instance.ShowDamage((int)bulletDamage, (int)GetHP(), transform.position);
                SetHP(_currentHP - bulletDamage);

                // Print current HP
                PrintZombieHP();
            }
        }
    }

    private void PrintZombieHP() => _healthTxt.text = _currentHP.ToString();

}
