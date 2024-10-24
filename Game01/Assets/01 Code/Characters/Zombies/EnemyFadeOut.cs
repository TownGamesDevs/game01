using System.Collections;
using UnityEngine;

public class EnemyFadeOut : MonoBehaviour
{ public static EnemyFadeOut instance;

    public float fadeDuration = 2.0f;  // Time taken to fade out the enemy (set this in the Inspector)

    private SpriteRenderer _spriteRenderer; // Reference to the SpriteRenderer component
    private bool _isFading = false;    // To prevent multiple fades

    private void Awake()
    {
        if (instance == null) instance = this;

        // Get the SpriteRenderer component
        _spriteRenderer = GetComponent<SpriteRenderer>();

        // Ensure there's a SpriteRenderer component attached
        if (_spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component missing on this GameObject!");
        }
    }

    // Call this when the enemy dies to start fading out
    public void StartFadeOut()
    {
        if (!_isFading)
        {
            _isFading = true; // Prevent triggering multiple fades
            StartCoroutine(FadeOutCoroutine());
        }
    }

    private IEnumerator FadeOutCoroutine()
    {
        float elapsedTime = 0f;

        // Store the initial alpha value (which should be 1 for fully visible)
        float initialAlpha = _spriteRenderer.color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            // Calculate the new alpha value based on elapsed time
            float newAlpha = Mathf.Lerp(initialAlpha, 0f, elapsedTime / fadeDuration);

            // Set the new alpha value
            SetAlpha(newAlpha);

            yield return null; // Wait for the next frame
        }

        // Ensure the alpha is set to 0 at the end
        SetAlpha(0f);
    }

    public void FadeIn() => SetAlpha(1f);
    

    // Helper function to set the alpha of the sprite
    private void SetAlpha(float alpha)
    {
        Color color = _spriteRenderer.color;
        color.a = alpha;
        _spriteRenderer.color = color;
    }
}
