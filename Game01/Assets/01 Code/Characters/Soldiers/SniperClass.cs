using UnityEngine;

public class SniperClass : SoldierClass
{
    [SerializeField] private BaseHP _health = BaseHP._75;
    //[SerializeField] private Weapon _weapon = Weapon.Sniper_rifle;
    [SerializeField] private Range _ranage = Range.Long;
    [SerializeField] private Target _target = Target.Strongest;

}
