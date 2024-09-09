using UnityEngine;

public class HealthManager : MonoBehaviour
{
   [SerializeField] private GameObject _wall;

    public static HealthManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }



    public void ZombieDamage(GameObject bullet, GameObject zombie)
    {
        // Variables
        Bullet current_bullet;
        ZombieClass current_zombie;
        int tmp_bulletHP;
        int tmp_zombieHP;

        // Get relevant components
        current_zombie = zombie.GetComponent<ZombieClass>();
        current_bullet = bullet.GetComponent<Bullet>();

        // Update health points
        tmp_bulletHP = current_bullet.GetBulletDamage() - current_zombie.GetHP();
        tmp_zombieHP = current_zombie.GetHP() - current_bullet.GetBulletDamage();

        // Set health points
        current_bullet.SetBulletDamage(tmp_bulletHP);
        current_zombie.SetHP(tmp_zombieHP);

    }

    public void WallDamage(float attackForce)
    {
        Wall wall = Wall.instance;
        float wallHP;

        wallHP = wall.GetHP();
        wall.SetHP(wallHP - attackForce);
    }
}
