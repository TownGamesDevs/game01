using TMPro;
using UnityEngine;

public class ZombieHP : MonoBehaviour
{
    [SerializeField] private int _currentHP;
    private DamagePointsManager _damagePointsPref;
    [SerializeField] private TextMeshProUGUI[] _txt;

    const string BULLET = "Bullet";
    private int _zombieHP;
    private bool _isDead;

    private void Start()
    {
        _damagePointsPref = GetComponent<DamagePointsManager>();
    }
    private void OnEnable()
    {
        _zombieHP = _currentHP;
        PrintZombieHP(_zombieHP);
        _isDead = false;
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
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "Hurt");
        gameObject.SetActive(false); // Disable zombie
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(BULLET) && collision.TryGetComponent<BulletDamage>(out BulletDamage bullet))
        {
            _damagePointsPref.ShowDamage(bullet.GetDamage());

            // Calculate damage as absolute difference
            int damageToBullet = bullet.GetDamage() - _zombieHP;
            int damageToZombie = _zombieHP - bullet.GetDamage();

            bullet.SetBulletDamage(damageToBullet);
            SetZombieDamage(damageToZombie);
        }
    }
}
