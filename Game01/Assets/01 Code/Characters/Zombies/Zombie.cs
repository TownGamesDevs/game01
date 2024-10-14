using TMPro;
using UnityEngine;

public class Zombie : ZombieClass
{
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackTime;
    [SerializeField] private TextMeshProUGUI _healthTxt;

    private float _timer;

    private new void Start()
    {
        base.Start();

        AttackForce = _attackForce;
        AttackTime = _attackTime;
        _timer = 0;

        UpdateHealthText();
    }

    private void Update()
    {
        if (_canMove)
        {
            ZombieMove();
        }

        CheckCanAttackWall();
    }

    public void CheckCanAttackWall()
    {
        if (_canAttackWall && !Wall.instance.GetIsDestroyed())
        {
            _timer += Time.deltaTime;

            if (_timer >= AttackTime)
            {
                AttackWall();
                _timer = 0;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            SetCanMove(false);
            SetCanAttackWall(true);
        }

        if (collision.CompareTag("Bullet"))
        {
            if (collision.TryGetComponent<Bullet>(out Bullet bullet))
            {
                float bulletDamage = bullet.GetBulletDamage();
                DamagePointsManager.instance.ShowDamage((int)bulletDamage, (int)GetHP(), transform.position);
                SetHP(_currentHP - bulletDamage);
                UpdateHealthText();
                AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "BulletHit");
            }
        }
    }

    private void UpdateHealthText() => _healthTxt.text = _currentHP.ToString();
}
