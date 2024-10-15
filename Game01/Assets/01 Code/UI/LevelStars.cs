using UnityEngine;

public class LevelStars : MonoBehaviour
{
    public static LevelStars instance;

    [SerializeField] private GameObject[] _stars;  // Array of level prefabs with star GameObjects as children

    [SerializeField, Range(0, 3)] private int _showStars;  // Number of stars to display
    [SerializeField, Range(0, 5)] private int _level;      // Level index for Inspector

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

    private void Start() => LoadStars();  // Load stars when the script starts

    // This method updates the star display for all levels based on PlayerPrefs
    private void UpdateAllStars()
    {
        for (int i = 0; i < _stars.Length; i++)
        {
            int savedStars = PlayerPrefs.GetInt("StarsLevel" + i, 0);  // Default to 0 if not set

            // Keep the first child always active (0-3 stars)
            _stars[i].transform.GetChild(0).gameObject.SetActive(true);

            // Disable star displays for 1 to 3 based on the saved rating
            for (int j = 1; j < _stars[i].transform.childCount; j++)
            {
                // Enable only the stars according to the saved rating
                _stars[i].transform.GetChild(j).gameObject.SetActive(j <= savedStars);
            }
        }
    }

    // This method sets the star rating for a specific level
    public void SetStarsForLevel(int levelIndex, int stars)
    {
        if (stars < 0 || stars > 3)
        {
            //Debug.LogError("Star rating must be between 0 and 3.");
            return;
        }

        // Save the star rating in PlayerPrefs
        PlayerPrefs.SetInt("StarsLevel" + levelIndex, stars);
        PlayerPrefs.Save();

        // Update the star display for that level
        UpdateAllStars();  // This ensures the correct stars are shown
    }

    // Load stars and update the UI
    public void LoadStars() => UpdateAllStars();  // Load stars on start

    // Automatically update stars when values change in the Inspector
    private void OnValidate()
    {
        if (_level >= 0 && _level < _stars.Length)  // Ensure valid level index
        {
            SetStarsForLevel(_level, _showStars);  // Update stars based on Inspector values
        }
    }
}
