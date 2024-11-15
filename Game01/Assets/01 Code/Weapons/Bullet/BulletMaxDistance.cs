using UnityEngine;

public class BulletMaxDistance : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _maxTime;
    private float _timer;

    private void Update() => CheckDistance();

    private void CheckDistance()
    {
        _timer += Time.deltaTime;
        if (Mathf.Abs(transform.position.x) >= _maxDistance || _timer >= _maxTime)
        {
            _timer = 0;
            gameObject.SetActive(false);
        }
    }
}
