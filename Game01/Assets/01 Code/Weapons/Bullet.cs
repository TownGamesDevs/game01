using System;
using System.Collections;
using UnityEngine;

public class Bullet : MonoBehaviour
{
	[SerializeField] private int _speed;
	[SerializeField] private int _destroyTime;
	[SerializeField] private int _damage;
	private HealthManager healthManager;
	private float _yPos;

	private void Awake()
	{
		// Sets the HealthManager instance
		if (healthManager == null)
			healthManager = new HealthManager();;
	}
	private void Start()
	{
		// Y axis is stored to prevent bullet of moving with the soldier
		_yPos = transform.position.y;

        // Bullet gets destroyed after X time
        StartCoroutine(DestroySelf(_destroyTime));
    }

	private void Update()
	{
		// Constantly moves the bullet
		Move();
	}

	private void Move()
	{
		// Bullet moves in fixed Y axis so it doesn't move with the player
		transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, _yPos);
	}


	private void OnTriggerEnter2D(Collider2D collision)
	{
        // Checks if bullet has collided with an enemy
        if (collision.gameObject.tag == "Enemy")
        {
            // Update bullet damage and zombie HP
            healthManager.ZombieDamage(this.gameObject, collision.gameObject);
        }
    }

	IEnumerator DestroySelf(int time)
	{
		// Bullet gets destroyed after X time
		yield return new WaitForSeconds(time);
		gameObject.SetActive(false);
		Destroy(gameObject);
	}

	public int GetBulletDamage()
	{
		return _damage;
	}

	public void SetBulletDamage(int hp)
	{
		_damage = hp;

		// Kills bullet if dead
		if (_damage <= 0)
			StartCoroutine(DestroySelf(0));
	}
}
