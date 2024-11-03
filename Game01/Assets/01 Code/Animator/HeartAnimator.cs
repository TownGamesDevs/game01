using System;
using UnityEngine;

public class HeartAnimator : MonoBehaviour
{
    [SerializeField] private GameObject[] _hearts;
    [SerializeField] private float _speed;
    [SerializeField] private float _targetScale;

    private bool _state;
    private float _timer;
    private Vector3 _originalScale;

    private void Start()
    {
        if (_hearts.Length > 0)
            _originalScale = _hearts[0].transform.localScale;

    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _speed)
        {
            _timer = 0;
            ChangeScale();
        }
    }

    private void ChangeScale()
    {
        if (_state)
        {
            _state = !_state;
            for (int i =0;  i < _hearts.Length; i++)
            {
                RectTransform rectTransform = _hearts[i].GetComponent<RectTransform>();
                rectTransform.localScale = new Vector3(_targetScale, _targetScale, _targetScale);
            }
        }
        else
        {
            _state = !_state;
            for (int i = 0; i < _hearts.Length; i++)
            {
                RectTransform rectTransform = _hearts[i].GetComponent<RectTransform>();
                rectTransform.localScale = _originalScale;
            }
        }
    }
}
