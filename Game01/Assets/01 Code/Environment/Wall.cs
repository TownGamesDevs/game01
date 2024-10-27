using System;
using TMPro;
using UnityEngine;

public class Wall : MonoBehaviour
{ public static Wall instance;

    public static event Action OnWallDestroyed;

    [SerializeField] private int _wallHP;
    [SerializeField] private TextMeshProUGUI[] _txt;
    private bool _isDestroyed;

    private void Awake() => instance ??= this;

    void Start()
    {
        _isDestroyed = false;
        PrintWallHP(_wallHP);
    }

    public void SetHP(int damage)
    {
        // Exit if dead
        if (_isDestroyed) return;

        int tmp = _wallHP - damage;
        if (tmp > 0)
        {
            _wallHP = tmp;
            PrintWallHP(_wallHP);
            return;
        }
        DestroyWall();
    }

    private void DestroyWall()
    {
        if (!_isDestroyed)
        {
            _isDestroyed = true;
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
