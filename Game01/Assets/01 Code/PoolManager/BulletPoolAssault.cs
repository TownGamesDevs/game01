using System.Collections.Generic;
using UnityEngine;


public class BulletPoolAssault : PoolManager
{ public static BulletPoolAssault instance;

    [SerializeField] private GameObject _assaultBullet;
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
        pool = InitializePool(_assaultBullet);
    }

    public GameObject GetAssaultBullet()
    {
        return PullObject(pool, _assaultBullet, _canGrow);
    }

}
