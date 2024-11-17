using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public static PlayerPosition instance;
    private void Awake() => instance ??= this;
    public Vector3 GetPlayerPos() => gameObject.transform.position;
}
