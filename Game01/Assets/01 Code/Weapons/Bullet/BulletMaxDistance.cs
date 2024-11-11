using UnityEngine;

public class BulletMaxDistance : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _maxTime;
    private float _timer;

    private void Update() =>  CheckDistance();
    
    public void CheckDistance()
    {
        _timer += Time.deltaTime;
        if (_maxDistance - transform.position.x <= 0 || _timer >= _maxTime)
        {
            _timer = 0;
            gameObject.SetActive(false);
        }
    }
}
