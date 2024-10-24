using System;
using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{ public static Wall instance;

    // Sets an event to allow ALL zombies to move if the wall gets destroyed
    public static event Action OnWallDestroyed;

    [SerializeField] private int _wallHP;
    [SerializeField] private TextMeshProUGUI txt;
    private float _currentHP;
    private bool _isDestroyed;

    private void Awake() => instance ??= this;

    void Start()
    {
        _isDestroyed = false;
        _currentHP = _wallHP;
        UpdateHealthText(txt, _currentHP);
    }

    public float GetHP() => _currentHP;

    public void SetHP(float hp)
    {
        AudioManager.instance.PlayRandomSound(AudioManager.Category.Other, "Wall");
        if (hp < _currentHP)
            _currentHP = hp;
        else
            _currentHP--;

        // Update wall HP text
        UpdateHealthText(txt, _currentHP);

        // Check if wall is dead
        if (_currentHP <= 0)
            Die();
    }

    private void Die()
    {
        if (!_isDestroyed)
        {
            // Event allows ALL zombies to move
            OnWallDestroyed?.Invoke();

            _isDestroyed = true;
            gameObject.SetActive(false);
        }
    }

    public bool GetIsDestroyed() => _isDestroyed;
    private void UpdateHealthText(TextMeshProUGUI txt, float hp) => txt.text = hp.ToString();
    
}
