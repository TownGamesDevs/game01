using UnityEngine;

public class BulletMaxDistance : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    private Vector2 _initialDistance;
    private float _totalDistance;
    private float _current_XPos;

    private void OnEnable() => RestartValues();
    private void Update() =>  CheckDistance();
    
    public void CheckDistance()
    {
        if (_maxDistance - transform.position.x <= 0)
            gameObject.SetActive(false);

        //_totalDistance = Vector2.Distance(_initialDistance, transform.position);

        //if (_totalDistance >= _maxDistance)
        //    gameObject.SetActive(false);
    }

    public void RestartValues()
    {
        _totalDistance = 0;
        _initialDistance = transform.position;
    }
}
