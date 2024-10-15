using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponRange : MonoBehaviour
{
    // Public variable to adjust range in the inspector
    [SerializeField] private float rangeX = 2f;  // Default range value

    // Reference to the BoxCollider2D
    [SerializeField] private BoxCollider2D boxCollider;

    private void Update()
    {
        // Dynamically update the BoxCollider2D size and offset
        UpdateColliderRange();
    }

    private void UpdateColliderRange()
    {
        // Set the size of the collider (only modify the X axis, leave Y as it is)
        boxCollider.size = new Vector2(rangeX, boxCollider.size.y);

        // Set the X offset to half of the X size to move it forward in the correct direction
        boxCollider.offset = new Vector2(rangeX / 2f, boxCollider.offset.y);
    }
}
