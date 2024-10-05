using UnityEngine;


public class SoldierAnimator : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Sprite[] _spriteArray;
    [SerializeField] private float _speed = .02f;
    [SerializeField] private bool _canPlayAnim = true;

    private float _counter;
    private int _frame;

    private void Start()
    {
        _frame = 0;
        _counter = 0;
    }


    private void Update()
    {
        if (_canPlayAnim)
        {
            _counter += Time.deltaTime;
            if (_counter >= _speed)
            { _counter = 0;

                _spriteRenderer.sprite = _spriteArray[_frame];
                _frame++;

                if (_frame >= _spriteArray.Length)
                    _frame = 0;
            }
        }
    }



}
