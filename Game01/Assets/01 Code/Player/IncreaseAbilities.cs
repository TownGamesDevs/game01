using UnityEngine;

public class IncreaseAbilities : MonoBehaviour
{
    public static IncreaseAbilities instance;

    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _roundsPerSec;
    [SerializeField] private float _reloadTime;
    [SerializeField] private int _ammo;

    private void Awake() => instance ??= this;


    public void Increase()
    {
        // speed
        PlayerSpeed.instance.IncreaseSpeed(_moveSpeed);

        // rounds per sec
        Shooting.instance.IncreaseRPS(_roundsPerSec);

        // reload time
        ReloadWeapon.instance.DecreaseReloadTime(_reloadTime);

        // ammo
        ReloadWeapon.instance.IncreaseMaxAmmo(_ammo);

        // bullet damage(?)
    }

}
