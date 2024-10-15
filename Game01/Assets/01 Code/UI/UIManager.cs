using UnityEngine;

public class UIManager : MonoBehaviour
{ public static UIManager instance;

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

    private void Start() => ShowMainMenu();
    
    
    private void ShowScreen(MainScreens.Screens selectedScreen)
    {
        // Makes sure there is only one MAIN screen activated at a time
        for (int i = 0; i < _mainScreens.Length; i++)
        {
            if (_mainScreens[i].name == selectedScreen)
                _mainScreens[i]._gameObject.SetActive(true);
            else
                _mainScreens[i]._gameObject.SetActive(false);
        }

        // Play sound effect for each screen activity
        AudioManager.instance.Play(AudioManager.Category.Other, "UI", "Selection");
    }
    public void ShowMainMenu() => ShowScreen(MainScreens.Screens.MainMenu);
    public void ShowLevels() => ShowScreen(MainScreens.Screens.LevelSelector);
    public void ShowLoadingScreen() => ShowScreen(MainScreens.Screens.LoadScreen);
    
    public void ShowOptions() => ShowScreen(MainScreens.Screens.Options);
    public void QuitGame() => Application.Quit();
    public void SetSound()
    {

        _soundOff.SetActive(_displaySound);
        _soundOn.SetActive(!_displaySound);

        _displaySound = !_displaySound;
        AudioManager.instance.CanPlaySound = _displaySound;
    }
    public void HideLoadingScreen()
    {
        for (int i =0; i < _mainScreens.Length; i++)
            if (_mainScreens[i].name == MainScreens.Screens.LoadScreen)
                _mainScreens[i]._gameObject.SetActive(false);
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

            // Add more screens as needed
        }

        public Screens name;
        public GameObject _gameObject;
    }
}
