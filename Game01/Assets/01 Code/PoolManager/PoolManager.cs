using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PoolTypes
{
    public enum Type
    {
        Bullet,
        Walker,
        DamagePoints,
        BloodParticles,
        // Add more as needed
    }
    public Type _name;
    public GameObject _prefab;
    public int _totalObjects = 1;
    public bool _canGrow;
    [HideInInspector] public List<GameObject> _list = new();
}

public class PoolManager : MonoBehaviour
{ public static PoolManager instance;

    [SerializeField] private PoolTypes[] _poolTypes;


    private void Awake()
    {
        if (instance == null) instance = this;
        DontDestroyOnLoad(gameObject);
        PoolAllObjects();
    }
    private void PoolAllObjects()
    {
        for (int i = 0; i < _poolTypes.Length; i++)
        {
            // conditions
            bool isValidPrefab = _poolTypes[i]._prefab != null;
            bool isPositive = _poolTypes[i]._totalObjects > 0;

            if (isValidPrefab && isPositive)
                AddToList(_poolTypes[i]._list, _poolTypes[i]._prefab, _poolTypes[i]._totalObjects);
        }
    }
    private void AddToList(List<GameObject> list, GameObject pref, int maxSize)
    {
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


        // Grow list
        if (canGrow && list.Count > 0)
        {
            GameObject obj = Instantiate(pref);
            obj.SetActive(true);
            list.Add(obj);
            return obj;
        }

        return null;
    }


    public GameObject Pool(PoolTypes.Type objectType)
    {
        for (int i = 0; i < _poolTypes.Length; i++)
        {
            // Conditions
            bool nameFound = _poolTypes[i]._name == objectType;
            bool isPositive = _poolTypes[i]._totalObjects > 0;

            if (nameFound && isPositive)
                return PoolFromList(_poolTypes[i]._list, _poolTypes[i]._prefab, _poolTypes[i]._canGrow);
        }
        return null;
    }
}
