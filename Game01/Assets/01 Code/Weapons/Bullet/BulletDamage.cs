using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int _bulletDamage;
    private int _actuallDamage;
    const string ENEMY = "Enemy";

    private void OnEnable()
    {
        _actuallDamage = _bulletDamage;
    }
    public int GetDamage() => _actuallDamage;

    public void ApplyDamage(int damage)
    {
        _actuallDamage = damage;
        if (_actuallDamage <= 0)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(ENEMY) && collision.TryGetComponent<ZombieHP>(out ZombieHP zombieHP))
        {
            // Calculate damage as absolute difference
            int damageToBullet = _actuallDamage - zombieHP.GetHP();
            int damageToZombie = zombieHP.GetHP() - _actuallDamage;

            ApplyDamage(damageToBullet);
            zombieHP.ApplyDamage(damageToZombie);
        }
    }
}
