using UnityEngine;
using TMPro;
using System.Collections;

public class DamagePoint : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI[] _damageText;  // Reference to the damage text
	private CanvasGroup _canvasGroup;     // Reference to the CanvasGroup for fading

	// speed
	[SerializeField] private float _speed = 2f;  // Speed of the upward movement
	[SerializeField] private float _baseSpeed = 2f; // Base speed of the upward movement
	[SerializeField] private float _maxSpeed = 5f; // Base time it takes to fade out
	[SerializeField] private float _minSpeed = 0.5f; // Base time it takes to fade out

	[Space(10)] // Add space before duration settings

	// duration
	[SerializeField] private float _duration = 1f;  // Time to display the damage before fading
	[SerializeField] private float _baseDuration = 1f; // Base time to display the damage before fading

	[Space(10)] // Add space before duration settings

	// fade out
	[SerializeField] private float _baseFadeOutTime = 0.5f; // Base time it takes to fade out
	[SerializeField] private float _fadeOutTime = 0.5f;  // Time it takes to fade out
	[SerializeField] private float _minFadeOutTime = 0.2f; // Minimum fade-out time
	[SerializeField] private float _maxFadeOutTime = 1f; // Maximum fade-out time



	private void Awake()
	{
		// Find the CanvasGroup and TextMeshProUGUI in the prefab's children
		_canvasGroup = GetComponentInChildren<CanvasGroup>();
	}

	private void Update()
	{
		// Move the damage point upwards over time
		transform.position = new Vector3(transform.position.x, transform.position.y + _speed * Time.deltaTime, transform.position.z);
	}

	public void StartAnimation(int damage)
	{
		if (damage <= 0) return;

		// Set the damage value as text
		for (int i = 0; i < _damageText.Length; i++)
			_damageText[i].text = "+" + damage.ToString();
		

		// Calculate speed and fade out time based on damage
		_speed = _baseSpeed / damage; // Adjust speed (larger damage = slower speed)
		// Limit the speed to a minimum and maximum value
		_speed = Mathf.Clamp(_speed, _minSpeed, _maxSpeed); // Ensure speed is within the limits

		_duration = _baseDuration / damage; // Adjust duration (larger damage = shorter display time)


		// Calculate fade-out time based on damage, clamped to min and max
		_fadeOutTime = _baseFadeOutTime * damage * 0.1f; // Adjust fade-out time (larger damage = longer fade-out)
		_fadeOutTime = Mathf.Clamp(_fadeOutTime, _minFadeOutTime, _maxFadeOutTime); // Limit fade-out time

		// Reset the alpha for new instance
		_canvasGroup.alpha = 1f;

		// Start the fade and destroy process
		StartCoroutine(FadeAndDestroy());
	}

	private IEnumerator FadeAndDestroy()
	{
		// Wait for the duration before fading out
		yield return new WaitForSeconds(_duration);

		// Fading logic: gradually reduce the CanvasGroup alpha value
		float fadeTimer = 0f;
		while (fadeTimer < _fadeOutTime)
		{
			fadeTimer += Time.deltaTime;
			float newAlpha = Mathf.Lerp(1f, 0f, fadeTimer / _fadeOutTime);  // Linearly interpolate alpha value
			_canvasGroup.alpha = newAlpha;
			yield return null;
		}

		// Once fade is complete, hide damage point object
		gameObject.SetActive(false);
	}
}
