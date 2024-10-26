using UnityEngine;

public class DNSpeed : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;  // Speed of the upward movement
    [SerializeField] private float _baseSpeed = 2f; // Base speed of the upward movement
    [SerializeField] private float _maxSpeed = 5f; // Base time it takes to fade out
    [SerializeField] private float _minSpeed = 0.5f; // Base time it takes to fade out

    public float GetSpeed() => _speed;
    public float GetBaseSpeed() => _baseSpeed;

    public float CalculateSpeed(int damage)
    {
        // Calculate speed and fade out time based on damage
        _speed = _baseSpeed / damage; // Adjust speed (larger damage = slower speed)
        
        // Limit the speed to a minimum and maximum value
        return Mathf.Clamp(_speed, _minSpeed, _maxSpeed);
    }
}
