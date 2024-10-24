using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCompletedScore : MonoBehaviour
{
    public static LevelCompletedScore instance;

    [SerializeField] private TextMeshProUGUI[] _txt;
    [SerializeField] private bool _stopMusic;
    [SerializeField] private float _scoreAnimationTime = 1f;

    private StarManager _starManager;
    private int finalScore;
    const string FORMAT = "Score • ";

    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (TryGetComponent<StarManager>(out _starManager))
            _starManager.DisplayStars();

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
        int currentScore;

        // Start sound effect
        AudioManager.instance.Play(AudioManager.Category.Other, "UI", "Score");

        while (currentTime < _scoreAnimationTime)
        {
            // Calculate how much of the duration has passed
            currentTime += Time.deltaTime;

            // Interpolate the score value based on the current time
            currentScore = (int)Mathf.Lerp(0, finalScore, currentTime / _scoreAnimationTime);  // Linear interpolation

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

    public void RestarLevel()
    {
        AudioManager.instance.StopAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


    public void NextLevel()
    {
        // ToFix - show load screen, and animate it... then disable it when scene is ready

        int index = SceneManager.GetActiveScene().buildIndex + 1;
        AudioManager.instance.StopAll();
        SceneManager.LoadScene(index);
    }

    public void MainMenu()
    {
        AudioManager.instance.StopAll();
        SceneManager.LoadScene(0);
        UIManager.instance.ShowMainMenu();
    }



}
