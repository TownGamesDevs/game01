using UnityEngine;

public class DamagePointsManager : MonoBehaviour
{ public static DamagePointsManager instance;


    [SerializeField] private GameObject damagePointPrefab;  // Assign this in the Inspector

    private void Awake() => instance ??= this;
    
    public void ShowDamage(int damage, Vector3 position)
    {
        GameObject obj = PoolManager.instance.PoolDamagePoint();
        if (obj == null) return;

        DamagePoint dp = obj.GetComponent<DamagePoint>();
        obj.transform.position = new Vector2(position.x, position.y + 2.5f);
        dp.Initialize(damage);
    }
}
