using UnityEngine;

public abstract class BaseAnimator<T> : MonoBehaviour where T : System.Enum
{
    [System.Serializable]
    public class AnimationData
    {
        public T name;
        public Sprite[] sprites;
        public int FPS;
        public float loopWaitTime;
    }

    [SerializeField] private T _defaultAnimation;
    [SerializeField] private AnimationData[] animations;

    protected SpriteRenderer _sr;
    private Sprite[] _currentSprites;
    private float _loopWaitTime;
    private float _timer;
    private float _secondsPerFrame;
    private int _frame;
    private bool isValidAnim;

    protected virtual void Awake()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr == null)
            Debug.LogWarning("SPRITE RENDERED IS NULL...");
    }

    private void Start() => SetAnimation(_defaultAnimation);


    private void OnEnable()
    {
        ResetValues();
        isValidAnim = false;
        InitializeDefaultAnimation();
    }
    private void ResetValues()
    {
        _frame = 0;
        _timer = 0;
    }
    private void Update() => Loop();

    private void Loop()
    {
        // exit early
        if (!isValidAnim) return;

        // update timer
        _timer += Time.deltaTime;

        // conditions
        bool secondsPerFramePassed = _timer >= _secondsPerFrame;
        bool loopTimePassed = _timer >= _loopWaitTime;

        if (secondsPerFramePassed && loopTimePassed)
        {
            _timer = 0;
            SetNextFrame();
        }
    }

    private void SetNextFrame()
    {
        if (_sr == null)
            _sr = GetComponent<SpriteRenderer>();

        // set current frame
        _sr.sprite = _currentSprites[_frame];

        // update frames
        _frame++;

        // reset animation?
        if (_frame >= _currentSprites.Length)
            _frame = 0;
    }

    public void SetAnimation(T name)
    {
        foreach (var anim in animations)
        {
            if (anim.name.Equals(name))
            {
                ResetValues();
                _currentSprites = anim.sprites;
                _secondsPerFrame = 1f / anim.FPS;
                _loopWaitTime = anim.loopWaitTime;
                isValidAnim = true;
                return;
            }
        }

        isValidAnim = false;
        Debug.LogWarning($"{GetType().Name} -> Animation {name} not found.");
    }

    protected abstract void InitializeDefaultAnimation();
}
