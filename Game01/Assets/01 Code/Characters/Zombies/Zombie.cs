using TMPro;
using UnityEngine;
using static WeaponsClass;

public class Zombie : ZombieClass
{
	[SerializeField] private float _attackForce;
	[SerializeField] private float _attackTime;
	[SerializeField] private TextMeshProUGUI _healthTxt;
	private float _timer;


	private void Start()
	{
		// Initialize variables
		_timer = 0;
		SetCanAttackWall(false);
		SetCanMove(true);
		_currentHP = _hp;
		AttackForce = _attackForce;
		AttackTime = _attackTime;

		// Update text on screen
		UpdateHealthText(GetHP(), _healthTxt);
	}

	private void Update()
	{
		// Constantly moves the zombie
		ZombieMove();
		CheckCanAttackWall();
    }


	private void OnTriggerEnter2D(Collider2D collision)
	{
		// Check if zombie has reached the wall
		if (collision.gameObject.CompareTag("Wall"))
		{
			SetCanMove(false);
			SetCanAttackWall(true);
		}

		// Check if zombie has been hit by a bullet
		if (collision.gameObject.CompareTag("Bullet"))
		{
			float bulletDamage;

			// Get bullet damage
			collision.TryGetComponent<Bullet>(out Bullet currentBullet);
			bulletDamage = currentBullet.GetBulletDamage();

            // Display points
            DamagePointsManager.instance.ShowDamage((int)bulletDamage, (int)GetHP(), transform.position);

            // Update HP
            SetHP(_currentHP - bulletDamage);

			// Updates the zombie health txt on screen
			UpdateHealthText(GetHP(), _healthTxt);
			AudioManager.instance.PlayRandomSound(AudioManager.Category.Zombie, "BulletHit");
		}
	}

	public void CheckCanAttackWall()
	{
		// Zombie has reached wall and wall is not destroyed yet
		if (GetCanAttack() & !Wall.instance.GetIsDestroyed())
		{
			// Attack after some time
			_timer += Time.deltaTime;
			if (_timer >= _attackTime)
			{
				AttackWall();
				_timer = 0;     // Restar timer
			}
		}
	}

}
