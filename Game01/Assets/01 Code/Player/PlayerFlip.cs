using UnityEngine;

public class PlayerFlip : MonoBehaviour
{ public static PlayerFlip instance;
    [SerializeField] private GameObject _bulletSpawn;
    private SpriteRenderer _sr;
    private bool _canFlipRight = true;
    private bool _canFlipLeft = true;

    private void Awake() => instance = instance != null ? instance : this;

    private void Start() => _sr = GetComponent<SpriteRenderer>();


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            FlipToRight();
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            FlipToLeft();
        }
    }

    private void FlipToRight()
    {
        if (_canFlipRight)
        {
            // Set flags
            _canFlipRight = false;
            _canFlipLeft = true;

            if (_sr != null)
                _sr.flipX = false;

            FlipBulletSpawn();
        }
    }

    private void FlipToLeft()
    {
        if (_canFlipLeft)
        {
            // Set flags
            _canFlipRight = true;
            _canFlipLeft = false;

            if (_sr != null)
                _sr.flipX = true;

            FlipBulletSpawn();
        }
    }

    public bool GetDir() => _canFlipRight;

    private void FlipBulletSpawn()
    {
        if (_bulletSpawn != null)
        {
            Vector3 localPos = _bulletSpawn.transform.localPosition;
            _bulletSpawn.transform.localPosition = new Vector3(-localPos.x, localPos.y, localPos.z);
        }
    }
}
