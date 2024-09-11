using TMPro;
using UnityEngine;

public class ZombieClass : MonoBehaviour
{
    public static ZombieClass instance;
    public enum Speed
    {
        Slow = 2,
        Moderate = 6,
        Fast = 12
    }
    public enum EnemyHP
    {
        Weak = 5,
        Moderate = 15,
        Strong = 25
    }
    





    // Variables
    public EnemyHP Zombie_Hp { get; protected set; }
    public Speed Zombie_speed { get; protected set; }
    public float AttackForce { get; protected set; }
    public float AttackTime { get; protected set; }
    private bool _isMoving;
    private bool _canAttackWall;
    private int _currentHP;
    private float _currentAttackForce;
    private float _currentSpeed;




    private void Start()
    {

        if (instance == null) instance = this;

        // Subscribes to the event to let all zombies move if the wall gets destroyed
        Wall.OnWallDestroyed += AllZombiesCanMove;

        // Sets the actual variable for HP
        _currentHP = (int)Zombie_Hp;

        // Sets zombie speed
        _currentSpeed = (float)Zombie_speed;

        // Sets attack force
        _currentAttackForce = AttackForce;

    }
    private void OnDestroy()
    {
        Wall.OnWallDestroyed -= AllZombiesCanMove;
    }




    public void Die()
    {
        // Kills the zombie
        WaveManager.instance.ZombieDefeated();
        Destroy(gameObject);
    }

    public void ZombieMove()
    {
        // Zombie can move while it has NOT reached the wall
        if (_isMoving)
            gameObject.transform.position =
                new Vector2(transform.position.x - _currentSpeed * Time.deltaTime, transform.position.y);
    }

    public void AttackWall()
    {
        HealthManager.instance.WallDamage(_currentAttackForce);
        print("Zombie is attacking!");
    }


    public int GetHP()
    {
        return _currentHP;
    }
    public void SetHP(int hp)
    {
        // Updates the actual HP variable
        if (hp < _currentHP)    // fixes bug where health increases if bullet hits two enemies that are in the same place
            _currentHP = hp;
        else
            _currentHP--;

        // Checks if zombie died
        if (_currentHP <= 0)
            Die();
    }
    public void UpdateHealthText(int hp, TextMeshProUGUI txt)
    {
        // Updates the HP text of the zombie
        txt.text = hp.ToString();
    }



    public float GetAttackForce()
    {
        return _currentAttackForce;
    }    
    public void SetAttackForce(int force)
    {
        if (force >= 0) // Zombies can't have negative force xD
            _currentAttackForce = force;
    }


    public float GetAttackTime()
    {
        return AttackTime;
    }

    public void SetAttackTime(int time)
    {
        AttackTime = time;
    }

    public void SetCanMove(bool state)
    {
        if (state != _isMoving)
            _isMoving = state;
    }

    public bool GetCanMove()
    {
        return _isMoving;
    }

    public void SetCanAttackWall(bool state)
    {
        if (state != _canAttackWall)
            _canAttackWall = state;
    }

    public bool GetCanAttack()
    {
        return _canAttackWall;
    }

    private void AllZombiesCanMove()
    {
        SetCanMove(true);
    }

    public int CalculateBulletDamage(GameObject bullet)
    {
        // Get enemy HP
        if (bullet.TryGetComponent<Bullet>(out Bullet currentBullet))
            return _currentHP - currentBullet.GetHP();

        return -1;
    }
}



