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

    private void Start()
    {
        ShowMainMenu();
    }
    private void ShowScreen(MainScreens.Screens selectedScreen)
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
    public void ShowMainMenu() => ShowScreen(MainScreens.Screens.MainMenu);
    public void ShowLevels() => ShowScreen(MainScreens.Screens.LevelSelector);
    public void ShowLoadScreen() => ShowScreen(MainScreens.Screens.LoadScreen);
    public void ShowOptions() => ShowScreen(MainScreens.Screens.Options);



    public void SetSound()
    {

        _soundOff.SetActive(_displaySound);
        _soundOn.SetActive(!_displaySound);

        _displaySound = !_displaySound;
        AudioManager.instance.CanPlaySound = _displaySound;
    }


    public void QuitGame() => Application.Quit();




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
