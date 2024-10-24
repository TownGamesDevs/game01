using UnityEngine;

public class DamagePointsManager : MonoBehaviour
{ public static DamagePointsManager instance;

    [SerializeField] private float _offset = 2.5f;

    [SerializeField] private GameObject damagePointPrefab;  // Assign this in the Inspector

    private void Awake() => instance ??= this;
    
    public void ShowDamage(int bulletDamage, int zombieHP, Vector3 position)
    {
        GameObject obj = PoolManager.instance.Pool(PoolData.Type.DamagePoints);
        if (obj == null) return;

        DamagePoint dp = obj.GetComponent<DamagePoint>();
        obj.transform.position = new Vector2(position.x, position.y + _offset);
        int damage = Mathf.Min(zombieHP, bulletDamage);
        dp.StartAnimation(damage);
        ScoreManager.instance.UpdateScore(damage);
    }
}
