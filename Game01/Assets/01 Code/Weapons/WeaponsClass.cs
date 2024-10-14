using UnityEngine;

public class WeaponsClass : MonoBehaviour
{
    public enum ReloadTime
    {
        Slow = 35,
        Medium = 26,
        Fast = 20,
    }

    public enum MagSize
    {
        Pistol_12 = 12,
        Rifle_30 = 30,
        Machine_gun_60 = 60,
        Shotgun_7 = 7,
        Sniper_rifle_10 = 10
    }

    public enum FireRate
    {
        // time it takes to fire each round (in milliseconds)
        pistol_90 = 90,
        rifle_15 = 15,
        machine_gun_10 = 10,
        shotgun_60 = 60,
        sniper_rifle_150 = 150
    }

    public enum Accuracy
    {
        Low,
        Medium,
        High
    }

    public enum BulletDamage
    {
        Low_1 = 1,
        Medium_4 = 4,
        High_10 = 10
    }



    // Constant
    const float SELL_PERCENTAGE = 0.85f;

    // Variables
    public float WeaponPrice { get; protected set; }
    public ReloadTime Reload_time { get; protected set; }
    public MagSize Mag_size { get; protected set; }
    public FireRate Fire_rate { get; protected set; }
    //public Accuracy Weapon_accuracy { get; protected set; }
    //public BulletDamage Bullet_damage { get; protected set; }


    protected float CalculateSellPrice(float price) => price > 0 ? Mathf.Round(price* SELL_PERCENTAGE) : 0;
    public virtual float GetFireRate() => (float)Fire_rate / 100;
    public virtual int GetReloadTime() => (int)Reload_time / 10;
    public int GetMagSize() => (int)Mag_size;


}
