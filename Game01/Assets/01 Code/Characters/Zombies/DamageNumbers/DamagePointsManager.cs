using UnityEngine;

public class DamagePointsManager : MonoBehaviour
{ public static DamagePointsManager instance;

    [SerializeField] private float _offset;
    [SerializeField] private Transform damagePointTransform;
    private DamageNumbers dp;
    private void Awake() => instance ??= this;
    public void ShowDamage(int damage)
    {
        // Pool Object
        GameObject obj = PoolManager.instance.Pool(PoolData.Type.DamagePoints);
        if (obj == null) return;

        dp = obj.GetComponent<DamageNumbers>();
        obj.transform.position = new Vector2(transform.position.x, damagePointTransform.transform.position.y + _offset);
        dp.ShowDamageNumber(damage);
    }
}
