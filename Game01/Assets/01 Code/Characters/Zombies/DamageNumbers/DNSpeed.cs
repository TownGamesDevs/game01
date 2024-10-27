using UnityEngine;

public class DNSpeed : MonoBehaviour
{
    [SerializeField] private float _speed;  // Speed of the upward movement
    [SerializeField] private float _baseSpeed; // Base speed of the upward movement
    [SerializeField] private float _maxSpeed; // Base time it takes to fade out
    [SerializeField] private float _minSpeed; // Base time it takes to fade out

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
