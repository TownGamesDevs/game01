using UnityEngine;

public class DamagePointsManager : MonoBehaviour
{
    public static DamagePointsManager instance;

    [SerializeField] private Transform _spawnPos;
    [SerializeField] private float _y_offset;
    private DamageNumbers dp;
    private DNScale dnScaleScript;
    private GameObject _damageNum;
    private void Awake() => instance ??= this;
    public void ShowDamageNumbers(int damage)
    {
        // Pool Object
        _damageNum = PoolManager.instance.Pool(PoolTypes.Type.DamagePoints);
        if (_damageNum == null)
        {
            Debug.LogError("ShowDamagePoints -> Couldn't pool DamagePoints");
            return;
        }

        _damageNum.gameObject.SetActive(true);
        dp = _damageNum.GetComponent<DamageNumbers>();
        dnScaleScript = _damageNum.GetComponent<DNScale>();
        _damageNum.transform.position = new Vector2(transform.position.x, _spawnPos.transform.position.y + _y_offset);
        dp.SetDM(damage);
        //dnScaleScript.SetScaleFactor(damage);
    }
}
