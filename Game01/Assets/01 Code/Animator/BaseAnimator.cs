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

    protected SpriteRenderer sr;
    private Sprite[] currentSprites;
    private float loopWaitTime;
    private float _timer;
    private float secondsPerFrame;
    private int _frame;
    private bool isValidAnim;

    protected virtual void Awake() => sr = GetComponent<SpriteRenderer>();

    private void Start() => SetAnimation(_defaultAnimation);
    

    private void OnEnable()
    {
        ResetValues();
        isValidAnim = false;
        InitializeDefaultAnimation();
    }

    private void Update() => LoopAnimation();

    private void ResetValues()
    {
        _frame = 0;
        _timer = 0;
    }

    private void LoopAnimation()
    {
        if (!isValidAnim) return;

        _timer += Time.deltaTime;

        if (_timer >= secondsPerFrame && _timer >= loopWaitTime)
        {
            sr.sprite = currentSprites[_frame];
            _frame++;

            if (_frame >= currentSprites.Length)
                _frame = 0;

            _timer = 0;
        }
    }

    public void SetAnimation(T name)
    {
        foreach (var anim in animations)
        {
            if (anim.name.Equals(name))
            {
                ResetValues();
                currentSprites = anim.sprites;
                secondsPerFrame = 1f / anim.FPS;
                loopWaitTime = anim.loopWaitTime;
                isValidAnim = true;
                return;
            }
        }

        isValidAnim = false;
        Debug.LogWarning($"{GetType().Name} -> Animation {name} not found.");
    }

    // This method should be overridden in the derived class to set the default animation
    protected abstract void InitializeDefaultAnimation();
}
