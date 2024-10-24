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

    private FrameManager _currentAnim;
    private float _timer, _delay = 0f;
    private int _frame;
    private bool _canLoop;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        _canLoop = true;
        SelectAnimation(FrameManager.Names.Walk);
    }
    private void OnDisable()
    {
        Wall.OnWallDestroyed -= SetWalkAnim;
    }
    private void OnEnable()
    {
        Wall.OnWallDestroyed += SetWalkAnim;
        _canLoop = true; // Ensure looping is allowed again
        _frame = 0; // Reset frame index
        SetWalkAnim(); // Default animation or last state
    }
    private void Update()
    {
        if (_canLoop)
            LoopAnimation();
    }
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
    public void SetWalkAnim() => SelectAnimation(FrameManager.Names.Walk);
}
