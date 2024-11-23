using System;
using TMPro;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{ public static PlayerHealth instance;

	[SerializeField] private int _health;
	[SerializeField] private TextMeshProUGUI[] _txt;

	private void Awake() => instance ??= this;
    private void Start() => PrintHealth();


	public void SetPlayerHealth(int damage)
	{
		int tmp = _health - damage;
		if (tmp <= 0)
		{
			Die();
		}

		_health = tmp;
		PrintHealth();
	}

    private void Die()
    {
		Timer.instance.StopTimeUpdate();
		GameOver.instance.GameOverScreen(true);
	}

    private void PrintHealth()
	{
		for (int i = 0; i < _txt.Length; i++)
			_txt[i].text = _health.ToString();
	}
}
