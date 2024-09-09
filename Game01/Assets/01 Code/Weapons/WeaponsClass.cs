using UnityEngine;

public class WeaponsClass : MonoBehaviour
{
	public enum LowMedHigh
	{
		Low,
		Medium,
		High
	}


	public enum ReloadTime
	{
		Slow = 4,
		Medium = 3,
		Fast = 2,
	}

	public enum MagSize
	{
		Pistol = 12,
		Rifle = 30,
		Machine_gun = 60,
		Shotgun = 7,
		Sniper_rifle = 10
	}

	public enum FireRate
	{
		// time it takes to fire each round (in milliseconds)
		pistol = 90,
		rifle = 22,
		machine_gun = 10,
		shotgun = 60,
		sniper_rifle = 150
	}

	public enum Accuracy
	{
		Low = LowMedHigh.Low,
		Medium = LowMedHigh.Medium,
		High = LowMedHigh.High
	}

	public enum BulletDamage
	{
		Low = 1,
		Medium = 4,
		High = 10
	}



	// Constant
	const float SELL_PERCENTAGE = 0.85f;

	// Variables
	public GameObject Bullet;
    public float WeaponPrice { get; protected set; }
	public ReloadTime Reload_time { get; protected set; }
	public MagSize Mag_size { get; protected set; }
	public FireRate Fire_rate { get; protected set; }
	public Accuracy Weapon_accuracy { get; protected set; }
	public BulletDamage Bullet_damage { get; protected set; }


	protected float CalculateSellPrice(float price)
	{
		if (price > 0) return Mathf.Round(price * SELL_PERCENTAGE);
		return 0;
	}

    public virtual float GetFireRate()
    {
        return (float)Fire_rate / 100;
    }

	public virtual int GetReloadTime()
	{
		return (int)Reload_time;
    }

	public GameObject GetBullet()
	{
		if (Bullet != null)
			return Bullet;
		return null;
	}

	public int GetBulletDamage()
	{
		return (int)Bullet_damage;
	}

	public int GetMagSize()
	{
		return (int)Mag_size;
	}

}
