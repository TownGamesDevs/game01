using TMPro;
using UnityEngine;

public class Zombie_Walker : ZombieClass
{
    [SerializeField] private Speed _speed;
    [SerializeField] private EnemyHP _hp;
    [SerializeField] private float _attackForce;
    [SerializeField] private float _attackTime;
    [SerializeField] TextMeshProUGUI _healthTxt;
    private float _timer;



    private void Awake()
    {
        // Initialize variables
        _timer = 0;
        SetCanAttackWall(false);
        SetCanMove(true);
        Zombie_Hp = _hp;
        Zombie_speed = _speed;
        AttackForce = _attackForce;
        AttackTime = _attackTime;

        // Update the HP text on screen to match current HP
        UpdateHealthText((int)Zombie_Hp, _healthTxt);
    }

    private void Update()
    {
        // Constantly moves the zombie
        ZombieMove();
        Attack();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if zombie has reached the wall
        if (collision.gameObject.name == "Wall")
        {
            SetCanMove(false);
            SetCanAttackWall(true);
        }

        // Check if zombie has been hit by a bullet
        else if (collision.gameObject.tag == "Bullet")
        {
            // Updates the zombie health txt on screen
            UpdateHealthText(GetHP(), _healthTxt);
        }
    }

    public void Attack()
    {
        if (GetCanAttack() & !Wall.instance.GetIsDestroyed())
        {
            _timer = _timer + Time.deltaTime;
            if (_timer >= _attackTime)
            {
                AttackWall();
                _timer = 0;
            }
        }

    }
}
