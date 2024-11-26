using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI[] _txt;
    private int _score;
    const string SCORE = "SCORE: ";

    private void Awake()
    {
        if (instance == null)
            instance = this;
        DontDestroyOnLoad(gameObject);
    }
    private void Start() => ResetScore();
    public int GetScore() => _score;

    public void SetScore(int score)
    {
        if (score <= 0) return;
        _score += score;

        PrintScore();
    }
    private void PrintScore()
    {
        for (int i = 0; i < _txt.Length; i++)
            _txt[i].text = SCORE + _score.ToString();
    }

    private void ResetScore()
    {
        _score = 0;
        PrintScore();
    }

    //private void SaveHighScore()
    //{
    //    // Save highscore
    //    if (_score > PlayerPrefs.GetInt("Highscore"))
    //        PlayerPrefs.SetInt("Highscore", _score);

    //}
}
