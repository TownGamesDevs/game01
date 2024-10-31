using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private int _minDamage, _maxDamage;
    private int _bulletDamage;

    private void OnEnable()
    {
        // Random damage
        _bulletDamage = Random.Range(_minDamage, _maxDamage + 1);
    }
    public int GetDamage() => _bulletDamage;

    public void SetBulletDamage(int damage)
    {
        _bulletDamage = damage;
        if (_bulletDamage <= 0)
            gameObject.SetActive(false);
    }
}
