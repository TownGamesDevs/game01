using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolData
{
    public enum Type
    {
        AssaultBullet,
        SniperBullet,
        Brute,
        Walker,
        DamagePoints,
        Blood,
        // Add more as needed
    }
    public Type _name;
    public GameObject _prefab;
    public int _total = 1;
    public bool _canGrow;
    [HideInInspector] public List<GameObject> _list = new();
}

public class PoolManager : MonoBehaviour
{ public static PoolManager instance;

    [SerializeField] private PoolData[] _ObjectsToPool;


    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
        PoolAllObjects();
    }
    private void PoolAllObjects()
    {
        for (int i = 0; i < _ObjectsToPool.Length; i++)
        {
            // Conditions
            bool validPref = _ObjectsToPool[i]._prefab != null;
            bool nonZero = _ObjectsToPool[i]._total > 0;

            if (validPref && nonZero)
                AddToList(_ObjectsToPool[i]._list, _ObjectsToPool[i]._prefab, _ObjectsToPool[i]._total);
        }
    }
    private void AddToList(List<GameObject> list, GameObject pref, int maxSize)
    {
        if (pref == null)
        {
            Debug.LogError("Prefab is null in PoolManager.");
            return;
        }

        for (int i = 0; i < maxSize; i++)
        {
            GameObject obj = Instantiate(pref);
            obj.SetActive(false);
            list.Add(obj);
        }
    }
    private GameObject PoolFromList(List<GameObject> list, GameObject pref, bool canGrow)
    {
        for (int i = 0; i < list.Count; i++)
            if (!list[i].activeSelf)
            {
                list[i].SetActive(true);
                return list[i];
            }

        if (canGrow && list.Count > 0)
        {
            GameObject obj = Instantiate(pref);
            obj.SetActive(true);
            list.Add(obj);
            return obj;
        }

        return null;
    }
    public GameObject Pool(PoolData.Type objectType)
    {
        for (int i = 0; i < _ObjectsToPool.Length; i++)
        {
            // Conditions
            bool isName = _ObjectsToPool[i]._name == objectType;
            bool nonZero = _ObjectsToPool[i]._total > 0;

            if (isName && nonZero)
                return PoolFromList(_ObjectsToPool[i]._list, _ObjectsToPool[i]._prefab, _ObjectsToPool[i]._canGrow);
        }
        return null;
    }
}
