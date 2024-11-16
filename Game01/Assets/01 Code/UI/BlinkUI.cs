using UnityEngine;

public class BlinkUI : MonoBehaviour
{
    [SerializeField] private GameObject _UI;
    [SerializeField] private float _time;

    private float _timer;
    private bool _enable;

    private void Start() => _enable = true;
    
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _time)
        {
            _timer = 0;
            _enable = !_enable;
            _UI.SetActive(_enable);
        }
    }

    public void EnableTxt()
    {
        _UI.SetActive(true);
    }

}
