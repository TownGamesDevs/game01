using TMPro;
using UnityEngine;

public class ZombieHP : MonoBehaviour
{
    [SerializeField] private int _currentHP;
    [SerializeField] private TextMeshProUGUI[] _txt;

    private ZombieBlood _blood;
    private DamagePointsManager _damagePoints;
    const string BULLET = "Bullet";
    private int _zombieHP;
    private bool _isDead;
    private int _tmpDamage;
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

    public void SetZombieDamage(int damage)
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
        foreach (var text in _txt)
            text.text = hp.ToString();
    }

    private void ZombieDie()
    {
        if (_isDead) return;

        _isDead = true;
        WaveController.instance.UpdateZombiesOnScreen();
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Hurt");
        gameObject.SetActive(false); // Disable zombie
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BULLET) && collision.TryGetComponent<BulletDamage>(out BulletDamage bullet))
        {
            // Calculate damage to bullet and zombie
            int damageToBullet = bullet.GetDamage() - _zombieHP;
            int damageToZombie = _zombieHP - bullet.GetDamage();

            // Damage must be positive and bigger than 0
            _tmpDamage = Mathf.Max(1, Mathf.Abs(damageToBullet));
            ScoreManager.instance.PrintScore(_tmpDamage);
            _damagePoints.ShowDamageNumbers(damageToZombie);


            bullet.SetBulletDamage(damageToBullet);
            SetZombieDamage(damageToZombie);
        }
    }
}
