using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public static GameTimer instance;
    [SerializeField] private TextMeshProUGUI[] _timerTxt;

    private float _timer;
    private bool _canUpdateTime = false;
    private string _totalTime;
    private int hours;
    private int minutes;
    private int seconds;

    private void Awake() => instance ??= this;
    private void Start() => ResetTimer();
    private void Update() => UpdateTime();

    public void EnableGameTimer() => _canUpdateTime = true;
    public void StopGameTimer() => _canUpdateTime = false;
    private void UpdateTime()
    {
        if (_canUpdateTime)
        {
            _timer += Time.deltaTime;

            hours = Mathf.FloorToInt(_timer / 3600);
            minutes = Mathf.FloorToInt((_timer % 3600) / 60);
            seconds = Mathf.FloorToInt(_timer % 60);
            _totalTime = $"{hours:D2}:{minutes:D2}:{seconds:D2}";

            PrintTime(_totalTime);
        }
    }
    public void PrintTime(string txt)
    {
        for (int i = 0; i < _timerTxt.Length; i++)
            _timerTxt[i].text = txt;
    }
    public void ResetTimer()
    {
        _timer = 0;
        PrintTime("00:00:00");
    }
    public string GetTotalTime() => _totalTime;
}
