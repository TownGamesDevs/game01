using UnityEngine;

public class BulletRotation : MonoBehaviour
{



    private float z_rotation = 45f;

    private void Start()
    {
        //ApplyRotation();
    }

    private void ApplyRotation()
    {
        transform.eulerAngles = new(0, 0, z_rotation);
    }



}
