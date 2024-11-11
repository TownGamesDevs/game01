using UnityEngine;

public class FlipPlayer : MonoBehaviour
{
    [SerializeField] private GameObject _player;
    private SpriteRenderer _playerSR;
    private bool _isRight = true;
    private FlipManager _fm;


    private void Start()
    {
        _fm = GetComponent<FlipManager>();
        if (_player != null)
            _playerSR = _player.GetComponent<SpriteRenderer>();
    }

    private void Update() => Flip(_fm.GetDirection());
    
    private void Flip(bool faceRight)
    {
        if (_isRight != faceRight)
        {
            _isRight = faceRight;

            if (_playerSR != null)
                _playerSR.flipX = !faceRight;
        }
    }
}
