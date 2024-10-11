using UnityEngine;


[System.Serializable]
public class Screens
{
    public enum MainScreens
    {
        MainMenu,
        LevelSelector,
        LoadScreen,
        // Add more screens as needed
    }

    public MainScreens name;
    public GameObject _gameObject;
}
public class UIManager : MonoBehaviour
{
    [SerializeField] private Screens[] _mainScreens;
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    private bool _displaySound = true;


    private void Start() => ShowMainMenu();
    private void ShowScreen(Screens.MainScreens selectedScreen)
    {
        // Makes sure there is only one MAIN screen activated at a time
        for (int i = 0; i < _mainScreens.Length; i++)
        {
            if (_mainScreens[i].name != selectedScreen)
                _mainScreens[i]._gameObject.SetActive(false);
            else
                _mainScreens[i]._gameObject.SetActive(true);
        }

        // Play sound effect for each screen activity
        AudioManager.instance.PlaySpecificSound(AudioManager.Category.Other, "UI", "Selection");
    }
    public void ShowMainMenu() => ShowScreen(Screens.MainScreens.MainMenu);
    public void ShowLevels() => ShowScreen(Screens.MainScreens.LevelSelector);
    public void ShowLoadScreen() => ShowScreen(Screens.MainScreens.LoadScreen);
    public void SetSound()
    {
        _soundOff.SetActive(_displaySound);
        _soundOn.SetActive(!_displaySound);
        _displaySound = !_displaySound;

        AudioManager.instance.CanPlaySound = _displaySound;
    }
    public void QuitGame() => Application.Quit();
}
