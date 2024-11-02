using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{ public static CameraShake instance;

    [SerializeField] private Camera _camera; // How long the shake lasts
    [SerializeField] private float shakeDuration = 0.1f; // How long the shake lasts
    [SerializeField] private float shakeMagnitude = 0.2f; // Intensity of the shake

    private Vector3 originalPosition;

    private void Awake()
    {
        if (instance == null) instance = this;
        originalPosition = _camera.transform.localPosition;
    }

    // Call this method to trigger the camera shake
    public void TriggerShake()
    {
        StartCoroutine(Shake());
    }

    private IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            // Generate a random offset for the shake effect
            float offsetX = Random.Range(-1f, 1f) * shakeMagnitude;
            float offsetY = Random.Range(-1f, 1f) * shakeMagnitude;

            // Apply the offset to the camera's position
            _camera.transform.localPosition = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        // Reset the camera to its original position
        _camera.transform.localPosition = originalPosition;
    }
}