using UnityEngine;

public class BulletMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _canMove;

    void Update() => Move();

    private void Move()
    {
        if (_canMove)
            transform.position = new Vector2(transform.position.x + _speed * Time.deltaTime, transform.position.y);
    }
}
