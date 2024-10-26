using System;
using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{
    public static Wall instance;

    // Allow ALL zombies to move if the wall gets destroyed
    public static event Action OnWallDestroyed;

    [SerializeField] private int _wallHP;
    [SerializeField] private TextMeshProUGUI[] _txt;
    private bool _isDead;

    private void Awake() => instance ??= this;

    void Start()
    {
        _isDead = false;
        PrintWallHP(_wallHP);
    }

    public void SetHP(int damage)
    {
        // Exit if dead
        if (_isDead) return;

        int tmp = _wallHP - damage;
        if (tmp > 0)
        {
            _wallHP = tmp;
            PrintWallHP(_wallHP);
            return;
        }
        Die();
    }

    private void Die()
    {
        // Can only die once
        if (!_isDead)
        {
            _isDead = true;
            OnWallDestroyed?.Invoke();
            gameObject.SetActive(false);
        }
    }

    private void PrintWallHP(float hp)
    {
        for (int i = 0; i < _txt.Length; i++)
            _txt[i].text = hp.ToString();
    }
}
