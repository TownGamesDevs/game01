using UnityEngine;
public class MuzzleFlash : MonoBehaviour
{
    public static MuzzleFlash instance;

    [SerializeField] private Sprite[] _muzzleFlash;
    [SerializeField] private float _displayTime;

    private bool _isPlaying;
    private float _timer;
    private SpriteRenderer _sr;

    private void Awake() => instance = instance != null ? instance : this;
    private void Start()
    {
        _isPlaying = false;
        _timer = 0;
        if (TryGetComponent<SpriteRenderer>(out _sr)) _sr.enabled = false;
    }


    private void Update() => HideMuzzleFlash();
    public void PlayRandomMuzzle()
    {
        if (_muzzleFlash == null || _sr == null) return;
        int rnd = Random.Range(0, _muzzleFlash.Length);

        _sr.enabled = true;
        _sr.sprite = _muzzleFlash[rnd];
        _isPlaying = true;
    }

    private void HideMuzzleFlash()
    {
        if (_isPlaying)
        {
            _timer += Time.deltaTime;
            if (_timer >= _displayTime)
            {
                _sr.enabled = false;
                _isPlaying = false;
                _timer = 0f;
            }
        }
    }
}
