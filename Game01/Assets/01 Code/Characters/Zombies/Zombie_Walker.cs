using TMPro;
using UnityEngine;

public class Zombie_Walker : ZombieClass
{
    //[SerializeField] private Speed _speed;
    //[SerializeField] private float _zombieHP;
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
        SetHP(GetHP());
        //Zombie_speed = _speed;
        AttackForce = _attackForce;
        AttackTime = _attackTime;

        // Update the HP text on screen to match current HP
        UpdateHealthText(GetHP(), _healthTxt);
    }
    private void Update()
    {
        // Constantly moves the zombie
        ZombieMove();
        AttackWall();
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
            SetHP(CalculateBulletDamage(collision.gameObject));

            // Updates the zombie health txt on screen
            UpdateHealthText(GetHP(), _healthTxt);
        }
    }
    public void AttackWall()
    {
        // Zombie has reached wall and wall is not destroyed yet
        if (GetCanAttack() & !Wall.instance.GetIsDestroyed())
        {
            // Attack after some time
            _timer = _timer + Time.deltaTime;
            if (_timer >= _attackTime)
            {
                base.AttackWall();
                _timer = 0;     // Restar timer
            }
        }
    }

}
