using UnityEngine;

public class ZombieBlood : MonoBehaviour
{
    [SerializeField] private GameObject _bloodParticles;
    private ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = _bloodParticles.GetComponent<ParticleSystem>();
        _particleSystem.Stop();
    }

    public void ShowBlood() =>  _particleSystem.Play();
}
