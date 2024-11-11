using System;
using UnityEngine;

public class FlipMuzzle : MonoBehaviour
{
    [SerializeField] private GameObject _muzzle;
    private SpriteRenderer _sr;
    private FlipManager _fm;
    private bool _isRight = true;

    private void Start()
    {
        _fm = GetComponent<FlipManager>();
        if (_muzzle != null)
            _sr = _muzzle.GetComponent<SpriteRenderer>();
    }


    private void Update() => FlipDirection(_fm.GetDirection());

    private void FlipDirection(bool faceRight)
    {
        if (_isRight != faceRight)
        {
            _isRight = faceRight;

            if (_muzzle != null)
                _sr.flipX = faceRight;

            FlipPosition();
        }
    }

    private void FlipPosition()
    {
        if (_muzzle != null)
        {
            // Flip the local position of the bullet spawn on the X axis
            Vector3 localPos = _muzzle.transform.localPosition;

            _muzzle.transform.localPosition = new Vector3(
                _fm.GetDirection() ? Mathf.Abs(localPos.x) : -Mathf.Abs(localPos.x),
                localPos.y,
                localPos.z
            );
        }
    }
}
