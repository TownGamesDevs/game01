using System.Collections;
using TMPro;
using UnityEngine;

public class LevelCompletedScore : MonoBehaviour
{
    public static LevelCompletedScore instance;

    [SerializeField] private TextMeshProUGUI _shadowTxt1;
    [SerializeField] private TextMeshProUGUI _shadowTxt;
    [SerializeField] private TextMeshProUGUI _scoreTxt;

    [SerializeField] private float duration = 1f;  // Time to reach the final score

    private int finalScore;  // The score to display after completing the level
    const string FORMAT = "Score • ";

    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        //ShowScore();
    }

    public void ShowScore()
    {
        finalScore = ScoreManager.instance.GetScore();  // Set the final score
        StartCoroutine(IncrementScore());  // Start the coroutine to increment the score
    }

    IEnumerator IncrementScore()
    {
        float currentTime = 0f;
        int currentScore = 0;

        AudioManager.instance.PlaySpecificSound(AudioManager.Category.Other, "UI", "Score");
        while (currentTime < duration)
        {
            // Calculate how much of the duration has passed
            currentTime += Time.deltaTime;

            // Interpolate the score value based on the current time
            currentScore = (int)Mathf.Lerp(0, finalScore, currentTime / duration);  // Linear interpolation

            // Update the score text in the UI
            _scoreTxt.text = FORMAT + currentScore.ToString();
            _shadowTxt.text = FORMAT + currentScore.ToString();
            _shadowTxt1.text = FORMAT + currentScore.ToString();

            yield return null;  // Wait for the next frame
        }

        // Ensure the final score is shown after the loop
        _scoreTxt.text = FORMAT + finalScore.ToString();
        _shadowTxt.text = FORMAT + finalScore.ToString();
        _shadowTxt1.text = FORMAT + finalScore.ToString();

        AudioManager.instance.StopSpecificSound(AudioManager.Category.Other, "UI", "Score");
    }
}
