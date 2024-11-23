using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    [SerializeField] private TextMeshProUGUI[] _timerTxt;


    private float _timer;
    private bool _canUpdateTime = false;
    private string _totalTime;

    private void Awake() => instance ??= this;
    private void Start() => ResetTimer();
    private void Update() => UpdateTime();

    public void StartUpdateTime() => _canUpdateTime = true;
    public void StopTimeUpdate() => _canUpdateTime = false;
    private void UpdateTime()
    {
        if (_canUpdateTime)
        {
            _timer += Time.deltaTime;

            int hours = Mathf.FloorToInt(_timer / 3600);
            int minutes = Mathf.FloorToInt((_timer % 3600) / 60);
            int seconds = Mathf.FloorToInt(_timer % 60);

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
        for (int i = 0; i < _timerTxt.Length; i++)
            _timerTxt[i].text = "00:00:00";
    }
    public string GetTotalTime() => _totalTime;



}
