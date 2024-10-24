using UnityEngine;

public class ZombieClass : MonoBehaviour
{
    public static ZombieClass instance;

    [SerializeField] protected float _hp;
    [SerializeField] protected float _speed;
    public float AttackForce { get; protected set; }
    public float AttackTime { get; protected set; }

    protected float _zombieHP;
    protected bool _canMove;
    protected bool _canAttackWall;
    protected bool _isDead;
    protected float _currentSpeed;
    protected ZombieAnimator _zombieAnimator;
    private EnemyFadeOut _fadeOut;


    private void Awake() => instance ??= this;
    protected void Start()
    {
        // Get components
        _zombieAnimator = GetComponent<ZombieAnimator>();
        _fadeOut = GetComponent<EnemyFadeOut>();

        Wall.OnWallDestroyed += MoveAllZombies;

        _zombieHP = _hp;
        _currentSpeed = _speed;
        _canMove = true;
        _canAttackWall = false;
    }
    private void OnEnable() => _isDead = false;
    private void OnDisable() => Wall.OnWallDestroyed -= MoveAllZombies;


    public bool GetIsDead() => _isDead;

    private void RestoreOriginalValues()
    {
        _zombieHP = _hp;
        _currentSpeed = _speed;
        _canMove = true;
        _canAttackWall = false;
        _fadeOut.FadeIn();
    }

    public void AttackWall()
    {
        // Play attack animation
        ZombieAnimator.instance.SelectAnimation(FrameManager.Names.Attack);

        // Set wall HP
        Wall.instance.SetHP(Wall.instance.GetHP() - AttackForce);
    }

    public float GetHP() => _zombieHP;
    public void SetHP(float hp)
    {
        _zombieHP = Mathf.Max(hp, 0); // Ensure HP doesn't go below 0

        if (_zombieHP <= 0)
            Die();
    }
    public virtual void Die()
    {
        if (_isDead) return;  // Ensure Die is not called multiple times

        _isDead = true;
        _canMove = false;

        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Hurt");

        // Start effect
        _fadeOut.StartFadeOut();

        // Restore values
        RestoreOriginalValues();
        //WaveManager.instance.ZombieKilled();
    }
    public void SetCanMove(bool state) => _canMove = state;
    public void SetCanAttackWall(bool state) => _canAttackWall = state;
    public bool GetCanAttackWall() => _canAttackWall;
    private void MoveAllZombies() => SetCanMove(true);
}
