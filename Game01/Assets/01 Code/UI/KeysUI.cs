using UnityEngine;

public class KeysUI : MonoBehaviour
{ public static KeysUI instance;

    private void Awake() => instance ??= this;
    public void DisableGameObject() => gameObject.SetActive(false);
}
