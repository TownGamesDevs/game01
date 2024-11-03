using UnityEngine;

public class DNScale : MonoBehaviour
{
    [SerializeField] private float _speed = 2f;   // Speed of the scaling effect
    [SerializeField] private float _targetScale = 1.5f;  // Target scale multiplier

    private Vector3 _initialScale;
    private bool _scalingUp = true;  // Track if we are scaling up or down
    private float _progress = 0f;  // Progress of the scaling effect

    private void Start()
    {
        _initialScale = transform.localScale;  // Store the initial scale
    }

    private void Update()
    {
        ChangeScale();
    }

    private void ChangeScale()
    {
        // Target scale vector
        Vector3 targetScale = _initialScale * _targetScale;

        // Interpolate between initial and target scale
        if (_scalingUp)
        {
            // Scale up smoothly
            _progress += _speed * Time.deltaTime;
            transform.localScale = Vector3.Lerp(_initialScale, targetScale, _progress);

            // Check if we reached the target scale
            if (_progress >= 1f)
            {
                _progress = 0f;
                _scalingUp = false;
            }
        }
        else
        {
            // Scale down smoothly
            _progress += _speed * Time.deltaTime;
            transform.localScale = Vector3.Lerp(targetScale, _initialScale, _progress);

            // Check if we reached the initial scale
            if (_progress >= 1f)
            {
                _progress = 0f;
                _scalingUp = true;
            }
        }
    }
}
