using UnityEngine;
public class MuzzleFlash : MonoBehaviour
{
    public static MuzzleFlash instance;

    [SerializeField] private Sprite[] _muzzleFlash;
    [SerializeField] private float _maxScreenTime;

    private bool _isShowing;
    private float _timer;
    private int _randomNum;
    private SpriteRenderer _spriteRenderer;

    private void Awake() => instance ??= this;
    private void Start() => InitializeVariables();
    private void InitializeVariables()
    {
        if (TryGetComponent<SpriteRenderer>(out _spriteRenderer))
            DisableSpriteRenderer();
    }

    private void Update()
    {
        if (_isShowing)
            HideMuzzleFlash();
    }
    private void HideMuzzleFlash()
    {
        _timer += Time.deltaTime;
        if (_timer >= _maxScreenTime)
            DisableSpriteRenderer();
    }
    private void SetDefaultValues()
    {
        _isShowing = false;
        _timer = 0f;
    }
    private void DisableSpriteRenderer()
    {
        _spriteRenderer.enabled = false;
        SetDefaultValues();
    }
    public void ShowRandomMuzzleFlash()
    {
        EnableSpriteRenderer();
        SetRandomSprite();
    }
    private void SetRandomSprite()
    {
        _randomNum = Random.Range(0, _muzzleFlash.Length);
        _spriteRenderer.sprite = _muzzleFlash[_randomNum];
    }

    private void EnableSpriteRenderer()
    {
        _spriteRenderer.enabled = true;
        _isShowing = true;
    }
}
