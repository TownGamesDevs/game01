using System;
using UnityEngine;

public class CrosshairFlip : MonoBehaviour
{



    [SerializeField] private GameObject _crosshair;
    [SerializeField] private GameObject _player;

    private void Update()
    {
        CheckPos();
    }

    private void CheckPos()
    {
        if (_crosshair != null && _player != null)
            if (_crosshair.transform.position.x > _player.transform.position.x)
            {
                FlipManager.instance.SetDirection(true);
            }
            else
            {
                FlipManager.instance.SetDirection(false);
            }
    }
}
