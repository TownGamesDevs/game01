using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public static HealthManager instance;
   [SerializeField] private GameObject _wall;


    private void Awake()
    {
        if (instance == null) instance = this;
    }


    //public void SetWallDamage(float attackForce)
    //{
    //    Wall wall = Wall.instance;
    //    float wallHP;

    //    wallHP = wall.GetHP();
    //    wall.SetHP(wallHP - attackForce);
    //}
}
