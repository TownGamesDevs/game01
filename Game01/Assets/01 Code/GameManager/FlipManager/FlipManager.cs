using UnityEngine;

public class FlipManager : MonoBehaviour
{ public static FlipManager instance;

    private bool _isFacingRight = true;  // Track facing direction directly

    private void Awake() => instance ??= this;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
            _isFacingRight = false;
        if (Input.GetKeyDown(KeyCode.D))
            _isFacingRight = true;
    }

    public bool GetDirection() => _isFacingRight;
}
