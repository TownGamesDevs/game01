using UnityEngine;

public class DNDuration : MonoBehaviour
{
    [SerializeField] private float _duration;  // Time to display the damage before fading
    [SerializeField] private float _baseDuration; // Base time to display the damage before fading
    public float CalculateDuration(int damage) => _baseDuration / damage;   // Adjust duration (larger damage = shorter display time)
}
