using TMPro;
using UnityEngine;

public class ZombieHP : MonoBehaviour
{
    [SerializeField] private int _currentHP;
    [SerializeField] private int _incrementBy;
    [SerializeField] private TextMeshProUGUI[] _txt;

    private ZombieBlood _blood;
    private DamagePointsManager _damagePoints;
    const string BULLET = "Bullet";
    private int _zombieHP;
    private bool _isDead;
    private void Start()
    {
        _blood = GetComponent<ZombieBlood>();
        _damagePoints = GetComponent<DamagePointsManager>();
    }
    private void OnEnable()
    {
        _zombieHP = _currentHP;
        _isDead = false;
        PrintZombieHP(_zombieHP);
    }

    public int GetHP() => _zombieHP;

    public void SetZombieHP(int damage)
    {
        if (_isDead) return;

        if (damage < _zombieHP)
            _zombieHP = damage;
        else
            _zombieHP--;

        if (_zombieHP <= 0)
        {
            ZombieDie();
            return;
        }
        PrintZombieHP(_zombieHP);
        _blood.ShowBlood();
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "BulletHit");
    }

    public void PrintZombieHP(int hp)
    {
        foreach (TextMeshProUGUI text in _txt)
            text.text = hp.ToString();
    }

    private void ZombieDie()
    {
        if (_isDead) return;

        TotalKilled.instance.IncrementTotalKilled();
        _currentHP += _incrementBy;
        _isDead = true;
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Hurt");
        gameObject.SetActive(false); // Disable zombie
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BULLET) && collision.TryGetComponent<BulletDamage>(out BulletDamage bullet))
        {
            // Calculate damage to bullet and zombie
            int bulletDamage = Mathf.Min(_zombieHP, bullet.GetDamage());
            int damageToBullet = bulletDamage - _zombieHP;
            int damageToZombie = _zombieHP - bulletDamage;

            // Display damage
            ScoreManager.instance.PrintScore(bulletDamage);
            _damagePoints.ShowDamageNumbers(bulletDamage);

            // Update bullet HP
            bullet.SetBulletHP(damageToBullet);

            // Update zombie HP
            SetZombieHP(damageToZombie);
        }
    }
}
