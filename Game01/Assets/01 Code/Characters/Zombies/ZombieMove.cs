using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private bool _canMove;

    private void Start() => _canMove = true;
    private void OnEnable() => Wall.OnWallDestroyed += EnableMove;
    private void OnDisable() => Wall.OnWallDestroyed -= EnableMove;
    void Update() => Move();


    public void Move()
    {
        if (_canMove)
            transform.position += _speed * Time.deltaTime * Vector3.left;
    }

    public void SetCanMove(bool state) => _canMove = state;
    public void EnableMove() => _canMove = true;
    


}
