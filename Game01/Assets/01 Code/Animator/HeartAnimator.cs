using UnityEngine;

public class HeartAnimator : MonoBehaviour
{
    [SerializeField] private GameObject _hearts;
    [SerializeField] private GameObject _heartShadow;

    [SerializeField] private float _speed;
    [SerializeField] private float _targetScale;

    private bool _canScale;
    private float _timer;
    private Vector2 _initialScale;
    private Vector2 _targetVector;
    private RectTransform _heart_transform;
    private RectTransform _heartShadow_transform;

    private void Start()
    {
        _heart_transform = _hearts.GetComponent<RectTransform>();
        _heartShadow_transform = _heartShadow.GetComponent<RectTransform>();
        _initialScale = _hearts.transform.localScale;
        _targetVector = new(_targetScale, _targetScale);
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
        if (_canScale) SetScale(_targetVector);
        else SetScale(_initialScale);
        _canScale = !_canScale;
    }


    private void SetScale(Vector2 scale)
    {
        _heart_transform.localScale = scale;
        _heartShadow_transform.localScale = scale;
    }
}
