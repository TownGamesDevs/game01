using UnityEngine;

public class ZombiePop : MonoBehaviour
{ public static ZombiePop instance;

    [SerializeField] private GameObject m_Prefab;
    const int MAX = 2;
    const float DISTANCE = 2f;

    private void Awake()
    { if (instance == null) instance = this; }


    public void PopZombie(Vector2 pos)
    {
        if (m_Prefab != null)
        { for (int i = 0; i < MAX; i++)
            { GameObject obj = PoolManager.instance.PoolWalkerZombie();

                if (obj != null)
                {
                    // Set the position for each new zombie
                    obj.transform.position = new Vector2(pos.x + (i * DISTANCE), pos.y);
                    WaveManager.instance.AddTotalZombies();
                }
            }
        }
        //else
            //ErrorManager.instance.PrintError("PopZombie has no prefab loaded -> POP()");
    }
}
