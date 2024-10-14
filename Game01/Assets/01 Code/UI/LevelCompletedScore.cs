using System.Collections;
using TMPro;
using UnityEngine;

public class LevelCompletedScore : MonoBehaviour
{
    public static LevelCompletedScore instance;

    [SerializeField] private TextMeshProUGUI[] _txt;
    [SerializeField] private bool _stopMusic;
    [SerializeField] private float duration = 1f;


    private int finalScore;
    const string FORMAT = "Score • ";

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        StarManager.instance.DisplayStars();

        if (_stopMusic)
            AudioManager.instance.Stop(AudioManager.Category.Music, "Background Track", "WaveMusic");
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

        // Start sound effect
        AudioManager.instance.Play(AudioManager.Category.Other, "UI", "Score");

        while (currentTime < duration)
        {
            // Calculate how much of the duration has passed
            currentTime += Time.deltaTime;

            // Interpolate the score value based on the current time
            currentScore = (int)Mathf.Lerp(0, finalScore, currentTime / duration);  // Linear interpolation

            // Update the score text in the UI

            for (int i = 0; i < _txt.Length; i++)
                _txt[i].text = FORMAT + currentScore.ToString();


            yield return null;  // Wait for the next frame
        }

        // Ensure the final score is shown after the loop
        for (int i = 0; i < _txt.Length; i++)
            _txt[i].text = FORMAT + finalScore.ToString();

        // Stop sound effect
        AudioManager.instance.Stop(AudioManager.Category.Other, "UI", "Score");
    }

}
