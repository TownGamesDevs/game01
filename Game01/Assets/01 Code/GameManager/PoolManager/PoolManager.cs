using System.Collections.Generic;
using UnityEngine;


public class PoolManager : MonoBehaviour
{
    public static PoolManager instance;

    // prefabs to spawn
    [SerializeField] private GameObject _assaultBullet, _sniperBullet, _zombieWalker, _zombieBrute, _damagePoints;

    // max spawns
    [SerializeField] private int _maxAssaultBullets, _maxSniperBullets, _maxWalker, _maxBrute, _maxDamagePoints;

    // can grow?
    [SerializeField] private bool _canGrowAssault, _canGrowSniper, _canGrowWalker, _canGrowBrute, _canGrowDamagePoints;

    // private lists where objects are stored
    private List<GameObject> _assaultPool = new();
    private List<GameObject> _sniperPool = new();
    private List<GameObject> _walkerPool = new();
    private List<GameObject> _brutePool = new();
    private List<GameObject> _damagePointsPool = new();


    private void Awake()
    { if (instance == null) instance = this; }
    private void Start()
    {
        if (_maxAssaultBullets > 0)
            InitializePool(_assaultPool, _assaultBullet, _maxAssaultBullets);

        if (_maxSniperBullets > 0)
            InitializePool(_sniperPool, _sniperBullet, _maxSniperBullets);

        if (_maxWalker > 0)
            InitializePool(_walkerPool, _zombieWalker, _maxWalker);

        if (_maxBrute > 0)
            InitializePool(_brutePool, _zombieBrute, _maxBrute);

        if (_maxDamagePoints > 0)
            InitializePool(_damagePointsPool, _damagePoints, _maxDamagePoints);
    }

    private void InitializePool(List<GameObject> list, GameObject pref, int maxSize)
    {
        for (int i = 0; i < maxSize; i++)
        {
            GameObject obj = Instantiate(pref);
            obj.SetActive(false);
            list.Add(obj);
        }
    }

    private GameObject PoolObject(List<GameObject> list, GameObject pref, bool canGrow)
    {
        for (int i = 0; i < list.Count; i++)
            if (!list[i].activeInHierarchy && !list[i].activeSelf)
            {
                list[i].SetActive(true);
                return list[i];
            }

        if (canGrow)
        {
            GameObject obj = Instantiate(pref);
            list.Add(obj);
            return obj;
        }

        return null;
    }


    // Used in other scripts to pool different objects
    public GameObject PoolAssaultBullet()
    {
        return PoolObject(_assaultPool, _assaultBullet, _canGrowAssault);
    }
    public GameObject PoolSniperBullet()
    {
        return PoolObject(_sniperPool, _sniperBullet, _canGrowSniper);
    }
    public GameObject PoolBruteZombie()
    {
        return PoolObject(_brutePool, _zombieBrute, _canGrowBrute);
    }
    public GameObject PoolWalkerZombie()
    {
        return PoolObject(_walkerPool, _zombieWalker, _canGrowWalker);
    }
    public GameObject PoolDamagePoint()
    {
        return PoolObject(_damagePointsPool, _damagePoints, _canGrowDamagePoints);
    }


    public List<GameObject> InitializeCustomPool(GameObject obj, int total)
    {
        if (obj == null) return null;
        List<GameObject> list = new();

        for (int i = 0; i < total; i++)
        {
            GameObject tmp = Instantiate(obj);
            tmp.SetActive(false);
            list.Add(tmp);
        }

        return list;
    }

    public GameObject PoolCustomGameObject(List<GameObject> list, GameObject pref, bool canGrow)
    {
        if (list.Count == 0) return null;

        for (int i = 0; i < list.Count; i++)
            if (!list[i].activeInHierarchy && !list[i].activeSelf)
            {
                list[i].SetActive(true);
                return list[i];
            }

        if (canGrow)
        {
            GameObject obj = Instantiate(pref);
            list.Add(obj);
            return obj;
        }

        return null;
    }
}
