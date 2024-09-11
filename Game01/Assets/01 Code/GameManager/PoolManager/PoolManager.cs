using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    private bool _canGrow;
    private int _maxPoolSize;

    protected List<GameObject> InitializePool(GameObject obj)
    {
        List<GameObject> pool = new List<GameObject>();
        for (int i = 0; i < _maxPoolSize; i++)
        {
            Instantiate(obj);
            obj.SetActive(false);
            pool.Add(obj);
        }
        return pool;
    }

    protected void SetCanGrow(bool state)
    { if (state != _canGrow) _canGrow = state; }
    protected void SetMaxSize(int size)
    { if (size > 0) _maxPoolSize = size; }

    protected GameObject PullObject(List<GameObject> list, GameObject pref, bool grow)
    {
        GameObject obj = null;

        for (int i = 0; i < list.Count; i++)
            if (!list[i].activeInHierarchy)
            {
                obj = list[i];
                obj.SetActive(true);
                return obj;
            }

        if (grow)
        {
            obj = Instantiate(pref);
            list.Add(obj);
        }
        return obj;
    }
}
