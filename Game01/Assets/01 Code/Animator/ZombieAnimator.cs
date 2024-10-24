using System.Collections;
using UnityEngine;

[System.Serializable]
public class FrameManager
{
    public enum Names
    {
        Hit,
        Attack,
        Idle,
        Dead,
        Walk,
        // Insert more as needed
    }

    [SerializeField] public Names _name;
    [SerializeField] public Sprite[] _sprites;
    [SerializeField] public float _fps, _duration, _resetDelay;
}

public class ZombieAnimator : MonoBehaviour
{
    public static ZombieAnimator instance;

    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private FrameManager[] _animations;
    private ZombieClass _zombieClassRef;

    private FrameManager _currentAnim;
    private FrameManager _previousAnim;
    private float _timer, _delay = 0f;
    private int _frame;
    private bool _canLoop;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        _zombieClassRef = GetComponent<ZombieClass>();
        _canLoop = true;
        Wall.OnWallDestroyed += SetWalkAnim;
        SelectAnimation(FrameManager.Names.Walk);
    }

    private void OnDestroy() => Wall.OnWallDestroyed -= SetWalkAnim;

    private void OnEnable()
    {
        _canLoop = true; // Ensure looping is allowed again
        _frame = 0; // Reset frame index
        SetWalkAnim(); // Default animation or last state
    }

    private void Update()
    {
        if (_canLoop)
            LoopAnimation();
    }

    // Loop animation frames
    public void LoopAnimation()
    {
        if (_currentAnim == null) return;

        float timePerFrame = 1f / _currentAnim._fps;
        _timer += Time.deltaTime;

        if (_timer >= timePerFrame && _delay <= 0)
        {
            _timer = 0;
            _spriteRenderer.sprite = _currentAnim._sprites[_frame];
            _frame++;

            // Restart frames if we reach the end of the array
            if (_frame >= _currentAnim._sprites.Length)
            {
                _frame = 0;
                _delay = _currentAnim._resetDelay;
            }
        }

        if (_delay > 0)
            _delay -= Time.deltaTime;
    }

    public void PlayHitAnimation()
    {
        if (!_canLoop) return; // Ensure hit animation doesn't play while looping

        StoreCurrentAnim(); // Store current animation
        SelectAnimation(FrameManager.Names.Hit); // Select hit animation

        if (!_zombieClassRef.GetIsDead())
        {
            StartCoroutine(PlayHitFrame());
        }
    }


    // Display the hit animation (single frame) for the specified duration
    private IEnumerator PlayHitFrame()
    {
        if (_currentAnim == null || _currentAnim._sprites.Length == 0) yield break;

        _spriteRenderer.sprite = _currentAnim._sprites[0]; // Show the hit frame
        yield return new WaitForSeconds(_currentAnim._duration); // Wait for the duration of the hit animation

        // Ensure to switch back to the previous animation afterward
        RestorePreviousAnim();
    }

    // Store the current animation state (before playing hit)
    private void StoreCurrentAnim() => _previousAnim = _currentAnim;
    

    // Restore the previously stored animation (e.g., walking)
    private void RestorePreviousAnim()
    {
        SelectAnimation(_previousAnim._name);
        _canLoop = true;
    }

    // Select an animation by name
    public void SelectAnimation(FrameManager.Names animName)
    {
        foreach (var anim in _animations)
        {
            if (anim._name == animName)
            {
                _currentAnim = anim;
                _frame = 0;
                return;
            }
        }
        _currentAnim = null;
    }

    // Example method to set walk animation
    public void SetWalkAnim() => SelectAnimation(FrameManager.Names.Walk);
}
