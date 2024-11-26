using TMPro;
using UnityEngine;

public class CountDown : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownTxt;

    private float _timer;
    private int _counter;
    private bool _enableCountDown;
    private Blinking _blink;

    const int MAX_SEC = 3;
    const int SEC = 1;

    private void Start()
    {
        _timer = SEC;
        _counter = MAX_SEC;
        _enableCountDown = false;
        _blink = GetComponent<Blinking>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_enableCountDown)
        {
            _enableCountDown = true;

            _blink.EnablePressAnyKeyTxt();
            KeysUI.instance.DisableGameObject();
            _blink.enabled = false;   // disable blinking script
        }

        if (_enableCountDown)
            StartCountdown();
    }

    private void StartCountdown()
    {
        Count();

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

    private void Count()
    {
        _timer -= Time.deltaTime;
        if (_timer <= 0)
        {
            _timer = SEC;
            _counter--;
            _countdownTxt.text = _counter.ToString();
        }
    }



}
