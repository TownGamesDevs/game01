using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class WeaponRange : MonoBehaviour
{
    [SerializeField] private float rangeX = 2f;
    [SerializeField] private BoxCollider2D boxCollider;
    private bool _enemyInRange;

    private void Start() => SetRange();
    
    private void OnValidate() => SetRange();
    
    private void SetRange()
    {
        boxCollider.size = new Vector2(rangeX, boxCollider.size.y);
        boxCollider.offset = new Vector2(rangeX / 2f, boxCollider.offset.y);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            _enemyInRange = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            _enemyInRange = false;
    }

    public bool IsInRange() => _enemyInRange;
}
