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
    private float timer;
    private float secondsPerFrame;
    private int frame;
    private bool isValidAnim;

    protected virtual void Awake() => sr = GetComponent<SpriteRenderer>();

    private void Start()
    {
        SetAnimation(_defaultAnimation);
    }

    private void OnEnable()
    {
        ResetValues();
        isValidAnim = false;
        InitializeDefaultAnimation();
    }

    private void Update() => LoopAnimation();

    private void ResetValues()
    {
        frame = 0;
        timer = 0;
    }

    private void LoopAnimation()
    {
        if (!isValidAnim) return;

        timer += Time.deltaTime;

        if (timer >= secondsPerFrame && timer >= loopWaitTime)
        {
            sr.sprite = currentSprites[frame];
            frame++;

            if (frame >= currentSprites.Length)
                frame = 0;

            timer = 0;
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
