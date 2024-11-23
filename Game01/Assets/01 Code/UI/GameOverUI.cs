using UnityEngine;
using TMPro;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] _scoreTxt;
    [SerializeField] private TextMeshProUGUI[] _timeTxt;

    private void OnEnable()
    {
        UpdateScoreTxt();
        UpdateTimeTxt();
    }

    private void UpdateScoreTxt()
    {
        for (int i=0; i < _scoreTxt.Length; i++)
        {
            _scoreTxt[i].text = "Score: " + ScoreManager.instance.GetScore().ToString();
        }
    }

    private void UpdateTimeTxt()
    {
        for (int i = 0; i < _timeTxt.Length; i++)
        {
            _timeTxt[i].text = "Time: " + Timer.instance.GetTotalTime();
        }
    }

}
