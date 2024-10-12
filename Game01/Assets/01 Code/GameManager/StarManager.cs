using UnityEngine;
using System.Collections;

public class StarManager : MonoBehaviour
{
    public static StarManager instance;

    [SerializeField] private GameObject[] _stars;
    [SerializeField] private float animationDuration = 0.5f;

    private int totalEnemies;  // Total enemies in the round
    private int killedEnemies;  // Number of enemies killed
    const float DELAY = 0f;

    private Vector3[] originalScales;  // To store the original scales of the stars

    private void Awake() => instance ??= this;

    public void Initialize()
    {
        // Initialize the original scales array and store the original scales of the stars
        originalScales = new Vector3[_stars.Length];
        for (int i = 0; i < _stars.Length; i++)
        {
            if (_stars[i] != null)
            {
                originalScales[i] = _stars[i].transform.localScale;  // Store the original scale
                _stars[i].SetActive(false);  // Optionally hide the stars at the start
            }
            else
            {
                Debug.LogError($"Star GameObject at index {i} is not assigned! Please assign a GameObject in the Inspector.");
            }
        }
    }

    // Call this function when the round ends
    public void DisplayStars()
    {
        Initialize();

        killedEnemies = UIZombiesLeft.instance.GetAllKilled();
        totalEnemies = UIZombiesLeft.instance.GetTotalZombies();

        // Calculate how many stars to display based on enemies killed
        int starCount = CalculateStars();

        // Start the coroutine to show the stars with animation
        StartCoroutine(AnimateStars(starCount));
    }

    // Calculate the number of stars based on killed enemies
    private int CalculateStars()
    {
        if (killedEnemies == totalEnemies)
            return 3;  // If all enemies are killed, show 3 stars
        else if (killedEnemies >= totalEnemies * 0.75f)
            return 2;  // Show 2 stars for 75% or more
        else if (killedEnemies >= totalEnemies * 0.5f)
            return 1;  // Show 1 star for 50% or more
        else
            return 0;  // Show no stars if less than 50%
    }

    // Coroutine to animate the stars appearing one by one
    IEnumerator AnimateStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            if (_stars[i] != null && originalScales[i] != null)  // Safeguard: Check if star and scale are not null
            {
                // Make the star visible
                _stars[i].SetActive(true);

                // Set the star's initial scale to zero for scaling animation
                _stars[i].transform.localScale = Vector3.zero;

                // Animate the star scaling from zero to its original scale
                float currentTime = 0f;
                while (currentTime < animationDuration)
                {
                    currentTime += Time.deltaTime;
                    float progress = currentTime / animationDuration;

                    // Interpolate from zero scale to the original scale
                    _stars[i].transform.localScale = Vector3.Lerp(Vector3.zero, originalScales[i], progress);

                    yield return null;
                }

                // Ensure the final scale is the original scale
                _stars[i].transform.localScale = originalScales[i];

                // Optional: Add a slight delay between each star animation
                yield return new WaitForSeconds(DELAY);  // Delay between stars (adjust as needed)
            }
            else
            {
                Debug.LogError($"Error: Star GameObject or original scale at index {i} is null.");
            }
        }
    }
}
