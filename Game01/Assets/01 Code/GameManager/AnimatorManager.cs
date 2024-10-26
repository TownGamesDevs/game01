using UnityEngine;

[System.Serializable]
public class FrameManager
{
    [SerializeField] public string _name;
    [SerializeField] public Sprite[] _sprites;
    [SerializeField] public float _FPS, _loopWaitTime;
}

public class AnimatorManager : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private Sprite[] _sprites;
    private float _fps;
    private float _timer;
    private float _loopWaitTime;
    private bool _isPlaying;
    private int _frame;
    private float timePerFrame;


    private void Start() => ResetValues();
    

    private void Update() => AnimationLoop();
    

    private void ResetValues()
    {
        _timer = 0;
        _frame = 0;
    }
    private void AnimationLoop()
    {
        if (_sprites == null) return;

        timePerFrame = 1f / _fps;
        _timer += Time.deltaTime;

        if (_timer >= timePerFrame && _timer >= _loopWaitTime)
        {
            _spriteRenderer.sprite = _sprites[_frame];
            _frame++;
            _timer = 0;

            if (_frame >= _sprites.Length)
                ResetValues();
        }
    }

    protected void SetAnimation(FrameManager animData)
    {
        if (animData != null)
        {
            _sprites = animData._sprites;
            _fps = animData._FPS;
            _loopWaitTime = animData._loopWaitTime;
        }
        else
            _sprites = null;
    }
}
