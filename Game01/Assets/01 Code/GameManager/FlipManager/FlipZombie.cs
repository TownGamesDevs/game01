using System;
using UnityEngine;

public class FlipZombie : MonoBehaviour
{
    private SpriteRenderer _sr;

    private void Start()
    {
        _sr = GetComponent<SpriteRenderer>();
        if (_sr == null) Debug.LogWarning("Sprite renderer not found");
    }
    private void Update() => Flip();
    

    private void Flip()
    {
        if (PlayerPosition.instance.GetPlayerPos().x < transform.position.x)
        {
            // is left to player
            if (_sr != null)
                _sr.flipX = true;
        }
        else
        {
            // is right to player
            if (_sr != null)
                _sr.flipX = false;
        }
    }
}
