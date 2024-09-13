using System.Collections.Generic;
using UnityEngine;

public class PoolManager : MonoBehaviour
{ public static PoolManager instance;

    // Assault bullet
	[SerializeField] private GameObject _assaultBullet;
	[SerializeField] private int MAX_ASSAULT;
	[SerializeField] private bool _canGrowAssault;

    // Sniper bullet
    [SerializeField] private GameObject _sniperBullet;
    [SerializeField] private int MAX_SNIPER;
	[SerializeField] private bool _canGrowSniper;

    // Zombies



    // Lists
    private List<GameObject> _assaultPool = new();
    private List<GameObject> _sniperPool = new();



    private void Awake()
    {
        if (instance == null) instance = this;
    }
    private void Start()
    { 
        if (MAX_ASSAULT > 0)
            InitializePool(_assaultPool, _assaultBullet, MAX_ASSAULT);

        if (MAX_SNIPER > 0)
            InitializePool(_sniperPool, _sniperBullet, MAX_SNIPER);
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

	public GameObject PoolAssaultBullet()
	{
		return PoolObject(_assaultPool, _assaultBullet, _canGrowAssault);
	}
    public GameObject PoolSniperBullet()
    {
        return PoolObject(_sniperPool, _sniperBullet, _canGrowSniper);
    }


}
