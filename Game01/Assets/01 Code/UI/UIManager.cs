using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject levels;
    [SerializeField] private GameObject SoundOn;
    [SerializeField] private GameObject SoundOff;

    private bool sound = true;

    private void Start() => ShowMainMenu();

    public void ShowLevels()
    {
        mainMenu.SetActive(false);
        levels.SetActive(true);
    }
    public void ShowMainMenu()
    {
        mainMenu.SetActive(true);
        levels.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetSound()
    {
        SoundOn.SetActive(sound);
        SoundOff.SetActive(!sound);
        sound = !sound;
    }
}
