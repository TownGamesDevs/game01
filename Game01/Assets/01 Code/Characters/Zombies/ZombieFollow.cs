using UnityEngine;

public class ZombieFollow : MonoBehaviour
{
    [SerializeField] private GameObject player;  // Reference to the player
    [SerializeField] private float speed = 2f;  // Speed of the zombie
    [SerializeField] private float xOffset = 1f; // Offset on the X-axis
    [SerializeField] private float yOffset = 1f; // Offset on the Y-axis

    private void Update()
    {
        if (player == null) return;

        // Calculate the target position with the offsets
        Vector3 targetPosition = new Vector3(
            player.transform.position.x + xOffset,
            player.transform.position.y + yOffset,
            transform.position.z // Keep the same Z position
        );

        // Calculate direction towards the target position
        Vector2 direction = (targetPosition - transform.position).normalized;

        // Move the zombie towards the target position
        transform.position += (Vector3)direction * speed * Time.deltaTime;
    }
}
