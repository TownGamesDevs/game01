using UnityEngine;

public class DNFadeOut : MonoBehaviour
{

    [SerializeField] private float _baseFadeOutTime; // Base time it takes to fade out
    [SerializeField] private float _fadeOutTime;  // Time it takes to fade out
    [SerializeField] private float _minFadeOutTime; // Minimum fade-out time
    [SerializeField] private float _maxFadeOutTime; // Maximum fade-out time

    public float CalculateFadeOut(int damage)
    {
        // Calculate fade-out time based on damage, clamped to min and max
        float fadeOutTime = _baseFadeOutTime * damage * 0.1f; // (larger damage = longer fade-out)
        return Mathf.Clamp(fadeOutTime, _minFadeOutTime, _maxFadeOutTime); // Limit fade-out time
    }

}
