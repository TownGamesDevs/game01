using UnityEngine;

public class FlipBulletSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _bulletSpawn;
    private FlipManager _fm;
    private void Start()
    {
        _fm = GetComponent<FlipManager>();
    }

    private void Update() => UpdateBulletSpawnPosition();
    
    private void UpdateBulletSpawnPosition()
    {
        if (_bulletSpawn != null)
        {
            // Flip the local position of the bullet spawn on the X axis
            Vector3 localPos = _bulletSpawn.transform.localPosition;

            _bulletSpawn.transform.localPosition = new Vector3(
                _fm.GetDirection() ? Mathf.Abs(localPos.x) : -Mathf.Abs(localPos.x),
                localPos.y,
                localPos.z
            );
        }
    }
}
