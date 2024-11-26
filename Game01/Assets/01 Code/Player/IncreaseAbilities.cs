using UnityEngine;

public class IncreaseAbilities : MonoBehaviour
{
    public static IncreaseAbilities instance;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _roundsPerSec;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _ammo;
    [SerializeField] private int _health;

    private void Awake() => instance ??= this;


    public void IncreaseAll()
    {
        // speed
        PlayerSpeed.instance.IncreaseSpeed(_moveSpeed);

        // rounds per sec
        Shooting.instance.IncreaseRPS(_roundsPerSec);

        // reload time
        ReloadWeapon.instance.DecreaseReloadTime(_reloadTime);

        // ammo
        ReloadWeapon.instance.IncreaseMaxAmmo(_ammo);

        // hp
        PlayerHealth.instance.IncreaseHealth(_health);

        // bullet damage(?)
    }

}
