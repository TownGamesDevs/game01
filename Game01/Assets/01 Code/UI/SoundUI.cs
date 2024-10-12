using UnityEngine;

public class SoundUI : MonoBehaviour
{


    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    private bool _displaySound = true;

    public void SetSound()
    {

        _soundOff.SetActive(_displaySound);
        _soundOn.SetActive(!_displaySound);

        _displaySound = !_displaySound;
        AudioManager.instance.CanPlaySound = _displaySound;
    }


}
