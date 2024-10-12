using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    [SerializeField] private TextMeshProUGUI _shadowTxt;
    [SerializeField] private TextMeshProUGUI _scoreTxt;
    private int _score;

    private void Awake() => instance ??= this;
    private void Start() => _score = 0;


    public void UpdateScore(int score)
    {
        if (score <= 0) return;

        if ((score + _score) > _score)
        {
            _score += score;
            PrintScore("Score: ");
        }
    }

    public int GetScore() => _score;
    private void PrintScore(string txt)
    {
        _scoreTxt.text = txt + _score.ToString();
        _shadowTxt.text = txt + _score.ToString();
    }

}
