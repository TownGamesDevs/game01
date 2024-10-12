using UnityEngine;

public class PlaySoundOnStart : MonoBehaviour
{
    [SerializeField] private AudioManager.Category _category;
    [SerializeField] private string _subCat;
    [SerializeField] private string _name;

    private void Start() => PlaySound(_category);
    

    private void PlaySound(AudioManager.Category cat)
    {
        AudioManager.instance.Play(cat, _subCat, _name);
    }

}
