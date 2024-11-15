using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    public static CountdownUI instance;

    [SerializeField] private TextMeshProUGUI[] _txt;
    [SerializeField] private int _countdownStart;

    private float _timer;
    private int _counter;
    private bool _canStartCountdown;
    private bool _hasStarted;

    private void Awake() => instance ??= this;
    private void Start()
    {
        _hasStarted = false;
        _canStartCountdown = false;
        _timer = 1;
        _counter = _countdownStart;
    }

    private void Update() => StartCountDown();

    public void StartCountdown()
    {
        if (!_hasStarted)
        {
            _hasStarted = true;
            _canStartCountdown = true;
        }
    }
    private void StartCountDown()
    {
        if (_canStartCountdown)
        {
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                _timer = 1;
                _counter--;
            }

            if (_counter <= 0)
                gameObject.SetActive(false);

            PrintText();
        }
    }

    private void PrintText()
    {
        for (int i = 0; i < _txt.Length; i++)
            _txt[i].text = _counter.ToString();
    }
}
