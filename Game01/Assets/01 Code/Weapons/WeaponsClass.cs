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
        sniper_rifle_180 = 180
    }


    // Variables
    public ReloadTime Reload_time { get; protected set; }
    public MagSize Mag_size { get; protected set; }
    public int Fire_rate { get; protected set; }


    public virtual float GetFireRate() => (float)Fire_rate / 100;
    public virtual int GetReloadTime() => (int)Reload_time / 10;
    public int GetMagSize() => (int)Mag_size;


}
