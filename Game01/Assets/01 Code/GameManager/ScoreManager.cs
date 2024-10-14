using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI[] _txt;
    private int _score;
    const string SCORE = "Score: ";

    private void Awake() => instance ??= this;
    private void Start()
    {
        _score = 0;
        UpdateScore(_score);
    }

    public int GetScore() => _score;
    public void UpdateScore(int score)
    {
        if (score <= 0) return;

        if ((score + _score) > _score)
        {
            _score += score;
            for (int i = 0; i < _txt.Length; i++)
                _txt[i].text = SCORE + _score.ToString();
        }
    }
}
