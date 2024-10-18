using UnityEngine;
using System.Collections;

public class UISlideAnimation : MonoBehaviour
{
	public RectTransform screen;         // The UI element (RectTransform) you want to animate
	public float startYPosition;         // Starting Y position for the slide-down animation
	public float targetYPosition;        // Final Y position for the slide-down animation
	public float animationDuration = 1.0f;   // Duration for the slide-down animation

	private float initialXPosition;      // Track the initial X position

	private void Update()
	{
        if (Input.GetKeyDown(KeyCode.Escape))
        {
			SlideUpAndDisable();
			ShowTutorial.instance.CanShowTutorial(true);
        }
        // same for reset stats
    }


	private void OnEnable()
	{
		// Store the current X position to keep it constant
		initialXPosition = screen.anchoredPosition.x;

		// Set the screen's starting Y position
		screen.anchoredPosition = new Vector2(initialXPosition, startYPosition);

		// Start the slide-down animation
		StartCoroutine(SlideDownAnimation());
	}

	private IEnumerator SlideDownAnimation()
	{
		float elapsedTime = 0f;

		while (elapsedTime < animationDuration)
		{
			elapsedTime += Time.deltaTime;

			// Smoothly interpolate the Y position from start to target
			float newYPosition = Mathf.Lerp(startYPosition, targetYPosition, elapsedTime / animationDuration);

			// Update only the Y position while keeping the X constant
			screen.anchoredPosition = new Vector2(initialXPosition, newYPosition);

			yield return null;  // Wait for the next frame
		}

		// Ensure it reaches the final Y position
		screen.anchoredPosition = new Vector2(initialXPosition, targetYPosition);
	}

	// Slide up animation that disables the GameObject at the end
	public void SlideUpAndDisable()
	{
		StartCoroutine(SlideUpAnimation());
	}

	public void SlideDownAndEnable()
	{
		gameObject.SetActive(true);
		StartCoroutine(SlideDownAnimation());
	}

	private IEnumerator SlideUpAnimation()
	{
		float elapsedTime = 0f;
		float currentYPosition = screen.anchoredPosition.y;  // Start from current Y position

		while (elapsedTime < animationDuration)
		{
			elapsedTime += Time.deltaTime;

			// Smoothly interpolate the Y position from current to startYPosition (reverse animation)
			float newYPosition = Mathf.Lerp(currentYPosition, startYPosition, elapsedTime / animationDuration);

			// Update only the Y position while keeping the X constant
			screen.anchoredPosition = new Vector2(initialXPosition, newYPosition);

			yield return null;  // Wait for the next frame
		}

		// Ensure it reaches the startYPosition
		screen.anchoredPosition = new Vector2(initialXPosition, startYPosition);

		// Disable the GameObject after the animation ends
		gameObject.SetActive(false);
	}
}