using System;
using TMPro;
using UnityEngine;

public class ZombieClass : MonoBehaviour
{
    public static ZombieClass instance;

    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    public float AttackForce { get; protected set; }
    public float AttackTime { get; protected set; }

    protected float _currentHP;
    protected bool _canMove;
    protected bool _canAttackWall;
    protected float _currentSpeed;

    private void Awake() => instance ??= this;

    protected void Start()
    {
        Wall.OnWallDestroyed += MoveAllZombies;

        _currentHP = _hp;
        _currentSpeed = _speed;
        _canMove = true;
        _canAttackWall = false;
    }

    private void OnDestroy() => Wall.OnWallDestroyed -= MoveAllZombies;

    public void ZombieMove()
    {
        if (_canMove)
        {
            transform.position += Vector3.left * _currentSpeed * Time.deltaTime;
        }
    }

    public virtual void Die()
    {
        _currentHP = _hp;
        _currentSpeed = _speed;
        _canMove = true;
        _canAttackWall = false;
        gameObject.SetActive(false);
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Hurt");
        PlayerPrefs.SetInt("ZombiesKilled", PlayerPrefs.GetInt("ZombiesKilled") + 1);
        PlayerPrefs.Save();
    }

    public void AttackWall()
    {
        Wall.instance.SetHP(Wall.instance.GetHP() - AttackForce);
        ZombieAnimator.instance.SetAttackAnim();
    }

    public float GetHP() => _currentHP;

    public void SetHP(float hp)
    {
        if (_currentHP > hp) _currentHP = hp;
        else _currentHP--;

        if (_currentHP <= 0)
        {
            Die();
            WaveManager.instance.ZombieKilled();
        }
    }

    public void SetCanMove(bool state) => _canMove = state;
    public void SetCanAttackWall(bool state) => _canAttackWall = state;
    public bool GetCanAttack() => _canAttackWall;
    private void MoveAllZombies() => SetCanMove(true);
}
