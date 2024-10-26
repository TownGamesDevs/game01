using TMPro;
using UnityEngine;

public class ZombieHP : MonoBehaviour
{
    [SerializeField] private int _currentHP;
    [SerializeField] private TextMeshProUGUI[] _txt;

    private int _actualHP;
    private bool _isDead;

    private void OnEnable()
    {
        _actualHP = _currentHP;
        PrintZombieHP(_actualHP);
        _isDead = false;
    }

    public int GetHP() => _actualHP;

    public void ApplyDamage(int damage)
    {
        if (_isDead) return;

        _actualHP = damage;
        PrintZombieHP(_actualHP);

        if (_actualHP <= 0)
            ZombieDie();
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
}
