using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePoint : MonoBehaviour
{
    private TextMeshProUGUI damageText;  // Reference to the damage text
    private CanvasGroup canvasGroup;     // Reference to the CanvasGroup for fading

    // speed
    [SerializeField] private float speed = 2f;  // Speed of the upward movement
    [SerializeField] private float baseSpeed = 2f; // Base speed of the upward movement
    [SerializeField] private float maxSpeed = 5f; // Base time it takes to fade out
    [SerializeField] private float minSpeed = 0.5f; // Base time it takes to fade out

    // duration
    [SerializeField] private float duration = 1f;  // Time to display the damage before fading
    [SerializeField] private float baseDuration = 1f; // Base time to display the damage before fading

    // fadeout
    [SerializeField] private float baseFadeOutTime = 0.5f; // Base time it takes to fade out
    [SerializeField] private float fadeOutTime = 0.5f;  // Time it takes to fade out
    [SerializeField] private float minFadeOutTime = 0.2f; // Minimum fade-out time
    [SerializeField] private float maxFadeOutTime = 1f; // Maximum fade-out time



    private void Awake()
    {
        // Find the CanvasGroup and TextMeshProUGUI in the prefab's children
        canvasGroup = GetComponentInChildren<CanvasGroup>();
        damageText = GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        // Start the coroutine to handle fade and destroy
        StartCoroutine(FadeAndDestroy());
    }

    private void Update()
    {
        // Move the damage point upwards over time
        transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime, transform.position.z);
    }
    
    public void Initialize(int damage)
    {
        // Set the damage value as text
        damageText.text = damage.ToString();
        StartCoroutine(FadeAndDestroy());

        // Calculate speed and fade out time based on damage
        speed = baseSpeed / damage; // Adjust speed (larger damage = slower speed)
        // Limit the speed to a minimum and maximum value
        speed = Mathf.Clamp(speed, minSpeed, maxSpeed); // Ensure speed is within the limits



        duration = baseDuration / damage; // Adjust duration (larger damage = shorter display time)


        // Calculate fade-out time based on damage, clamped to min and max
        fadeOutTime = baseFadeOutTime * damage * 0.1f; // Adjust fade-out time (larger damage = longer fade-out)
        fadeOutTime = Mathf.Clamp(fadeOutTime, minFadeOutTime, maxFadeOutTime); // Limit fade-out time

        // Reset the alpha for new instance
        canvasGroup.alpha = 1f;

        // Start the fade and destroy process
        StartCoroutine(FadeAndDestroy());
    }

    private IEnumerator FadeAndDestroy()
    {
        // Wait for the duration before fading out
        yield return new WaitForSeconds(duration);

        // Fading logic: gradually reduce the CanvasGroup alpha value
        float fadeTimer = 0f;
        while (fadeTimer < fadeOutTime)
        {
            fadeTimer += Time.deltaTime;
            float newAlpha = Mathf.Lerp(1f, 0f, fadeTimer / fadeOutTime);  // Linearly interpolate alpha value
            canvasGroup.alpha = newAlpha;
            yield return null;
        }

        // Once fade is complete, destroy the damage point object
        gameObject.SetActive(false);
    }
}
