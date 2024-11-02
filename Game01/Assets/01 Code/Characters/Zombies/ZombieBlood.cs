using System.Collections;
using UnityEngine;

public class ZombieBlood : MonoBehaviour
{

    [SerializeField] private float _y_offset;
    [SerializeField] private float _x_offset;
    public void ShowBlood()
    {
        // Pool object
        GameObject blood = PoolManager.instance.Pool(PoolData.Type.Blood);
        if (blood == null) return;

        if (blood.TryGetComponent<ParticleSystem>(out ParticleSystem particle))
        {
            // Set position and activate particle system
            blood.transform.position = new Vector2(transform.position.x + _x_offset, transform.position.y + _y_offset);
            blood.SetActive(true);
            particle.Play();
        }
    }
}
