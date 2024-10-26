using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int _minDamage, _maxDamage;
    private int _bulletDamage;
    const string ENEMY = "Enemy";

    private void OnEnable()
    {
        _bulletDamage = Random.Range(_minDamage,_maxDamage);
    }
    public int GetDamage() => _bulletDamage;

    public void SetBulletDamage(int damage)
    {
        _bulletDamage = damage;
        if (_bulletDamage <= 0)
            gameObject.SetActive(false);
    }
}
