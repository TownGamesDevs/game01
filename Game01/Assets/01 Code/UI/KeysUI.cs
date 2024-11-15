using UnityEngine;
using UnityEngine.UI;

public class KeysUI : MonoBehaviour
{
    [SerializeField] private GameObject _keysUI; // The UI object to fade
    [SerializeField] private float fadeDuration = 2f; // Time in seconds for fade-out

    private CanvasGroup _canvasGroup; // CanvasGroup component for fading
    private bool _isFading = false; // Track if fading is in progress

    private void Start()
    {
        if (_keysUI != null)
        {
            // Add or fetch the CanvasGroup component
            _canvasGroup = _keysUI.GetComponent<CanvasGroup>();
            if (_canvasGroup == null)
                _canvasGroup = _keysUI.AddComponent<CanvasGroup>();

            _canvasGroup.alpha = 1f; // Ensure UI starts fully visible
            _keysUI.SetActive(true);
        }
    }

    private void Update()
    {
        if (!_isFading && Input.anyKeyDown) // Start fade-out when any key or mouse button is pressed
        {
            StartCoroutine(FadeOut());
        }
    }

    private System.Collections.IEnumerator FadeOut()
    {
        // Start countdown
        CountdownUI.instance.StartCountdown();

        _isFading = true;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            // Gradually decrease alpha value
            _canvasGroup.alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            elapsedTime += Time.deltaTime;
            yield return null; // Wait for the next frame
        }

        _canvasGroup.alpha = 0f; // Ensure alpha is set to 0
        _keysUI.SetActive(false); // Disable UI
        _isFading = false; // Mark fading as complete
    }
}
