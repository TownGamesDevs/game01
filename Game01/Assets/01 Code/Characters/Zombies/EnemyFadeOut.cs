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

        // Ensure there's a SpriteRenderer component attached
        if (!TryGetComponent<SpriteRenderer>(out _spriteRenderer))
            Debug.LogError("SpriteRenderer component missing on this GameObject!");
        
    }

    private void OnEnable()
    {
        SetAlpha(1f);
        _isFading = false;
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
        float initialAlpha = _spriteRenderer.color.a;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;

            float newAlpha = Mathf.Lerp(initialAlpha, 0f, elapsedTime / fadeDuration);
            SetAlpha(newAlpha);

            yield return null;
        }

        // Ensure the alpha is set to 0 at the end
        SetAlpha(0f);

        // Notify the WaveManager that this zombie is truly dead
        WaveManager.instance.ZombieKilled();

        // Disable the GameObject after fade out
        gameObject.SetActive(false);
    }


    public void FadeIn() => SetAlpha(1f);
    
    private void SetAlpha(float alpha)
    {
        Color color = _spriteRenderer.color;
        color.a = alpha;
        _spriteRenderer.color = color;
    }
}
