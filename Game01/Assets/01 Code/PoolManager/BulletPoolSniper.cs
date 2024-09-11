using System.Collections.Generic;
using UnityEngine;


public class BulletPoolSniper : PoolManager
{ public static BulletPoolSniper instance;

    [SerializeField] private GameObject _sniperBullet;
    [SerializeField] private int _maxBullets;
    [SerializeField] private bool _canGrow;

    private List<GameObject> pool;



    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    {
        SetCanGrow(_canGrow);
        SetMaxSize(_maxBullets);
        pool = InitializePool(_sniperBullet);
    }

    public GameObject GetSniperBullet()
    {
        return PullObject(pool, _sniperBullet, _canGrow);
    }

}
