using UnityEngine;

public class FlipManager : MonoBehaviour
{ public static FlipManager instance;

    private bool _isFacingRight = true;

    private void Awake() => instance ??= this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _isFacingRight = false;
        if (Input.GetKeyDown(KeyCode.D))
            _isFacingRight = true;
    }

    public void SetDirection(bool state) => _isFacingRight = state;
    public bool GetDirection() => _isFacingRight;
}
