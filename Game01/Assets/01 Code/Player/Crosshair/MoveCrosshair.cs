using UnityEngine;

public class MoveCrosshair : MonoBehaviour
{
    [SerializeField] private GameObject _crosshair;

    private void Start() => Cursor.visible = false;
    
    private void Update() => Move();

    private void Move()
    {
        if (_crosshair != null)
        {
            // Get the mouse position in screen space and convert it to world space
            Vector3 mousePosition = Input.mousePosition;
            mousePosition.z = Camera.main.nearClipPlane; // Set the Z position for the camera's depth
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

            // Update the crosshair's position to match the mouse's world position
            _crosshair.transform.position = worldPosition;
        }
    }
}
