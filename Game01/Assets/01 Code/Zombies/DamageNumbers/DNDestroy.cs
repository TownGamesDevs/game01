using UnityEngine;

public class DNDestroy : MonoBehaviour
{
    [SerializeField] private float _destroyTime;
    [SerializeField] private bool _canDestroy;
    private float _timer;

    private void OnEnable()
    {
        gameObject.SetActive(true);
        _timer = 0;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer >= _destroyTime && _canDestroy)
            gameObject.SetActive(false);
    }



}
