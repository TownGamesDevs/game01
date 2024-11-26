using UnityEngine;

public class Blinking : MonoBehaviour
{
    [SerializeField] private GameObject _countdownTxt;
    [SerializeField] private float _blinkTime = 0.5f;

    private float _timer;
    private bool _state;
        
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _blinkTime)
        {
            _timer = 0;
            _state = !_state;
            _countdownTxt.SetActive(_state);
        }
    }

    public void EnablePressAnyKeyTxt() => _countdownTxt.SetActive(true);
}
