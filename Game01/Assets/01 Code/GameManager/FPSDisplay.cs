using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI fpsText; // Reference to a UI text element to display the FPS
    private float deltaTime;

    private void Update()
    {
        // Calculate the time between frames
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;

        // Calculate FPS
        float fps = 1.0f / deltaTime;

        // Update the UI text with FPS value
        fpsText.text = $"FPS: {fps:0.}";
    }
}
