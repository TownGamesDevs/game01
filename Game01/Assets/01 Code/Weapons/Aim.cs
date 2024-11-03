using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject projectilePrefab;  // The projectile prefab to spawn
    [SerializeField] private Transform firePoint;          // The point from where the projectile is fired
    [SerializeField] private float projectileSpeed = 10f;  // Speed of the projectile

    private Camera mainCamera;

    private void Start()
    {
        // Cache the main camera
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MouseAim();

        if (Input.GetMouseButtonDown(0)) // Left mouse click
        {
            Shoot();
        }
    }

    private void MouseAim()
    {
        // Get the mouse position in world space
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; // Set z to 0 since we're working in 2D

        // Calculate the direction from firePoint to mouse position
        Vector3 aimDirection = (mousePosition - firePoint.position).normalized;

        // Rotate the player or gun towards the mouse position (optional, if needed)
        transform.right = aimDirection; // This assumes the "right" of your sprite is the forward-facing direction
    }

    private void Shoot()
    {
        // Instantiate the projectile at the firePoint's position and rotation
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);

        // Calculate direction towards mouse
        Vector2 shootDirection = (mainCamera.ScreenToWorldPoint(Input.mousePosition) - firePoint.position).normalized;

        // Set projectile velocity towards the shoot direction
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = shootDirection * projectileSpeed;
        }
    }
}
