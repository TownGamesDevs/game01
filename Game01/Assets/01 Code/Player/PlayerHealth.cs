using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth instance;

    [SerializeField] private int _health;
    [SerializeField] private TextMeshProUGUI[] _txt;

    private void Awake() => instance ??= this;
    private void Start() => PrintHealth();


    public void TakeDamage(int damage)
    { 
        // Check health
        int hp = _health - damage;

        if (hp <= 0)
        {
            Die();
            return;
        }

        // Update and print
        _health = hp;
        PrintHealth();
    }

    private void Die()
    {
        GameTimer.instance.StopGameTimer();
        GameOver.instance.GameOverScreen();
    }

    private void PrintHealth()
    {
        for (int i = 0; i < _txt.Length; i++)
            _txt[i].text = _health.ToString();
    }

    public void IncreaseHealth(int hp)
    {
        _health += hp;
        PrintHealth();
    }
}
