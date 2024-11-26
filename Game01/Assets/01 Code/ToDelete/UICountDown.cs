using TMPro;
using UnityEngine;

public class UICountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txt;

    private float _timer;
    private int _counter;
    private bool _enableCountDown;
    private Blinking _pressAnyKeyUI;

    const int MAX_SEC = 3;
    const int SEC = 1;

    private void Start()
    {
        _timer = _counter = MAX_SEC;
        _enableCountDown = false;
        _pressAnyKeyUI = GetComponent<Blinking>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_enableCountDown)
        {
            _enableCountDown = true;

            _pressAnyKeyUI.EnablePressAnyKeyTxt();
            KeysUI.instance.DisableGameObject();
            _pressAnyKeyUI.enabled = false;   // disable blinking script
        }

        if (_enableCountDown)
            StartCountdown();
    }

    private void StartCountdown()
    {
        CountDown();

        if (_counter <= 0)
        {
            StartGame();
            gameObject.SetActive(false);
        }
    }

    private void StartGame()
    {
        GameTimer.instance.EnableGameTimer();
        WaveManager.instance.EnableZombieSpawn();
        WavesNew.instance.StartNextWave();
    }

    private void CountDown()
    {
        _timer -= Time.deltaTime;
        if (_timer % SEC == 0)
        {
            _counter--;
            _txt.text = _counter.ToString();
        }
    }
}
