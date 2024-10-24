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
    protected bool _isDead;
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
    private void OnEnable()
    {
        _isDead = false;
    }

    public bool GetIsDead() => _isDead;

    private void OnDestroy() => Wall.OnWallDestroyed -= MoveAllZombies;

    public void ZombieMove()
    {
        if (_canMove)
            transform.position += Vector3.left * _currentSpeed * Time.deltaTime;
    }

    public virtual void Die()
    {
        _isDead = true;
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Hurt");

        // Total zombies killed
        PlayerPrefs.SetInt("ZombiesKilled", PlayerPrefs.GetInt("ZombiesKilled") + 1);
        PlayerPrefs.Save();

        // Start effect
        //EnemyFadeOut.instance.StartFadeOut();

        // Restore values
        RestoreOriginalValues();
        gameObject.SetActive(false);
    }

    private void RestoreOriginalValues()
    {
        _currentHP = _hp;
        _currentSpeed = _speed;
        _canMove = true;
        _canAttackWall = false;
        EnemyFadeOut.instance.FadeIn();
    }

    public void AttackWall()
    {
        // Play attack animation
        ZombieAnimator.instance.SelectAnimation(FrameManager.Names.Attack);

        // Set wall HP
        Wall.instance.SetHP(Wall.instance.GetHP() - AttackForce);
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
    public bool GetCanAttackWall() => _canAttackWall;
    private void MoveAllZombies() => SetCanMove(true);
}
