using UnityEngine;
using TMPro; // Include the TextMeshPro namespace

public class LevelStars : MonoBehaviour
{
	public static LevelStars instance;

	[SerializeField] private GameObject[] _stars;  // Array of level prefabs with star GameObjects as children
	[SerializeField, Range(0, 3)] private int[] _starRatings; // Array to hold star ratings for each level (0 to 3)
	[SerializeField] private TextMeshProUGUI[] _totalStarsText; // Single TextMeshPro component to display total stars

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
			DontDestroyOnLoad(gameObject);  // Persist the manager across scenes
		}
		else
		{
			Destroy(gameObject);
		}
	}

	private void Start()
	{
		LoadStars();  // Load stars when the script starts
		UpdateTotalStars(); // Update the total stars display
	}

	// Update the star display for all levels based on star ratings
	private void UpdateAllStars()
	{
		for (int i = 0; i < _stars.Length; i++)
		{
			// Ensure the first child (0-3) is always active
			_stars[i].transform.GetChild(0).gameObject.SetActive(true);

			// Get the star rating from the array
			int stars = _starRatings[i];

			// Disable star displays for 1 to 3 based on the star rating
			for (int j = 1; j < _stars[i].transform.childCount; j++)
			{
				// Enable only the stars according to the rating
				_stars[i].transform.GetChild(j).gameObject.SetActive(j <= stars);
			}
		}
	}

	// Set the star rating for a specific level
	public void SetStarsForLevel(int levelIndex, int stars)
	{
		if (stars < 0 || stars > 3)
		{
			Debug.LogError("Star rating must be between 0 and 3.");
			return;
		}

		// Assign the star rating in the array
		_starRatings[levelIndex] = stars;

		// Save the star rating in PlayerPrefs
		PlayerPrefs.SetInt("StarsLevel" + levelIndex, stars);
		PlayerPrefs.Save();

		// Update the star display for that level
		UpdateAllStars();  // Ensure the correct stars are shown
		UpdateTotalStars(); // Update total stars display
	}

	// Load stars from PlayerPrefs
	public void LoadStars()
	{
		for (int i = 0; i < _starRatings.Length; i++)
		{
			// Load star ratings from PlayerPrefs, defaulting to 0 if not set
			_starRatings[i] = PlayerPrefs.GetInt("StarsLevel" + i, 0);
		}

		UpdateAllStars();  // Update the star displays
		UpdateTotalStars(); // Update total stars display
	}

	// Calculate and update the total stars display
	private void UpdateTotalStars()
	{
		int totalStars = 0;
		foreach (int stars in _starRatings)
		{
			totalStars += stars;  // Sum up all the stars
		}

		// Update the TextMeshPro component with the total stars
		if (_totalStarsText != null)
		{
			for (int i = 0; i < _totalStarsText.Length; i++)
			{
				_totalStarsText[i].text = totalStars + "/18"; // Example format
			}
		}
	}

	// Automatically update stars when values change in the Inspector
	private void OnValidate()
	{
		UpdateAllStars();  // Reflect any changes in the Inspector in real-time
		UpdateTotalStars(); // Update total stars display when changes occur
	}
}
