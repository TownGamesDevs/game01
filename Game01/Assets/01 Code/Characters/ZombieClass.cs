using System;
using TMPro;
using UnityEngine;

public class ZombieClass : MonoBehaviour
{ public static ZombieClass instance;

    public static event Action OnZombieDie;

    [SerializeField] protected float _hp;
    [SerializeField] private float _speed;
    [SerializeField] private float _currentSpeed;
    public float AttackForce { get; protected set; }
    public float AttackTime { get; protected set; }
    private bool _isMoving;
    private bool _canAttackWall;
    protected float _currentHP;
    private float _currentAttackForce;


    private void Awake()
    { if (instance == null) instance = this; }
    private void Start()
    {
        // Subscribes to the event to let all zombies move if the wall gets destroyed
        Wall.OnWallDestroyed += AllZombiesCanMove;

        // Sets the actual variable for HP
        SetHP(_hp);

        // Sets zombie speed
        _currentSpeed = _speed;

        // Sets attack force
        _currentAttackForce = AttackForce;
    }
    private void OnDestroy()
    { Wall.OnWallDestroyed -= AllZombiesCanMove; }




    public void ZombieMove()
    {
        // Zombie can move while it has NOT reached the wall
        if (_isMoving)
            gameObject.transform.position = new Vector2(transform.position.x - _currentSpeed * Time.deltaTime, transform.position.y);
    }




    public void Die()
    {
        // Reset values
        _currentHP = _hp;
        _currentSpeed = _speed;
        _currentAttackForce = AttackForce;
        SetCanMove(true);
        SetCanAttackWall(false);
        gameObject.SetActive(false);

        // Pops two walker zombies if brute zombie died
        if (gameObject.CompareTag("BruteZombie"))
            ZombiePop.instance.PopZombie(transform.position);
    }
    public void AttackWall()
    {
        Wall.instance.SetHP(Wall.instance.GetHP() - _currentAttackForce);

        //ErrorManager.instance.PrintWarning("Wall is being attacked");
    }
    public float GetHP()
    { return _currentHP; }
    public void SetHP(float hp)
    {
        // Updates the actual HP variable
        if (_currentHP > hp)    // fixes bug where health increases if bullet hits two enemies that are in the same place
            _currentHP = hp;
        else
            _currentHP--;

        // Zombie died
        if (_currentHP <= 0)
        {
            Die();
            WaveManager.instance.ZombieDefeated();
            UIZombiesLeft.instance.UpdateTotalZombies();
        }
    }
    public void UpdateHealthText(float hp, TextMeshProUGUI txt)
    {
        // Updates the HP text of the zombie
        txt.text = hp.ToString();
    }
    public void SetCanMove(bool state)
    {
        if (state != _isMoving)
            _isMoving = state;
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
    public float CalculateBulletDamage(GameObject bullet)
    {
        // Get enemy HP
        if (bullet.TryGetComponent<Bullet>(out Bullet currentBullet))
            return _currentHP - currentBullet.GetBulletDamage();

        return -1;
    }
}



