using UnityEngine;

public class BulletMaxDistance : MonoBehaviour
{
    [SerializeField] private float _maxDistance;

    private void Update() =>  CheckDistance();
    
    public void CheckDistance()
    {
        if (_maxDistance - transform.position.x <= 0)
            gameObject.SetActive(false);
    }
}
