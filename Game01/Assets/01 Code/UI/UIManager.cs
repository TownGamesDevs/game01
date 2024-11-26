using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] private MainScreens[] _mainScreens;
    [SerializeField] private GameObject _soundOn;
    [SerializeField] private GameObject _soundOff;
    private bool _displaySound = true;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        ShowMainMenu();
        AudioManager.instance.Play(AudioManager.Category.Music, "Background Track", "MenuMusic");
    }


    private void OnEnable() => AudioManager.instance.Play(AudioManager.Category.Music, "Background Track", "MenuMusic");


    private void ShowScreen(MainScreens.Screens selectedScreen)
    {
        // Makes sure there is only one MAIN screen activated at a time
        foreach (var screen in _mainScreens)
            screen._gameObject.SetActive(screen.name == selectedScreen);

        // Play sound effect for each screen activity
        AudioManager.instance.Play(AudioManager.Category.Other, "UI", "Selection");
    }
    public void ShowMainMenu() => ShowScreen(MainScreens.Screens.MainMenu);
    public void ShowLevels() => ShowScreen(MainScreens.Screens.LevelSelector);
    public void ShowLoadingScreen() => ShowScreen(MainScreens.Screens.LoadScreen);
    public void ShowOptions() => ShowScreen(MainScreens.Screens.Options);
    public void ShowStats() => ShowScreen(MainScreens.Screens.Stats);
    public void QuitGame() => Application.Quit();
    public void SetSound()
    {

        _soundOff.SetActive(_displaySound);
        _soundOn.SetActive(!_displaySound);

        _displaySound = !_displaySound;
        AudioManager.instance._canPlay = _displaySound;
    }
    public void HideLoadingScreen()
    {
        foreach (var screen in _mainScreens)
            if (screen.name == MainScreens.Screens.LoadScreen)
                screen._gameObject.SetActive(false);
    }






    [System.Serializable]
    public class MainScreens
    {
        public enum Screens
        {
            MainMenu,
            LevelSelector,
            LoadScreen,
            Options,
            Stats,

            // Add more screens as needed
        }

        public Screens name;
        public GameObject _gameObject;
    }
}
