using TMPro;
using UnityEngine;

public class AssaultClass : SoldierClass
{
    [SerializeField] private BaseHP _health = BaseHP._100;
    [SerializeField] private Range _ranage = Range.Medium;
    [SerializeField] private Target _target = Target.Closest;
    
}
