using System;
using TMPro;
using UnityEngine;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _txt;

    private int _countdownStart;
    private float _timer;
    private int _counter;
    private bool _canStartCountdown;
    private BlinkUI _blink;

    private void Start()
    {
        _countdownStart = 3;
        _canStartCountdown = false;
        _timer = 1;
        _counter = _countdownStart;
        _blink = GetComponent<BlinkUI>();
    }

    private void Update()
    {
        if (Input.anyKeyDown && !_canStartCountdown)
        {
            _canStartCountdown = true;
            KeysUI.instance.DisableGameObject();
            if (_blink != null)
            {
                _blink.EnableTxt();
                _blink.enabled = false;
            }
        }

        StartCountdown();
    }

    private void StartCountdown()
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
            {
                Timer.instance.StartUpdateTime();
                WaveController.instance.CanSpawnZombies();
                gameObject.SetActive(false);    // Die
            }

            _txt.text = _counter.ToString();
        }
    }
}
