using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    [SerializeField] private GameObject[] _levels;  // Array of level prefabs (with children for unlocked/locked states)
    [SerializeField, Range(0, 5)] private int highestUnlockedLevel = 0;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);  // Ensure LevelManager persists across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start() => CheckAllLevels();

    // This method checks all levels and sets the appropriate lock/unlock state based on the highest unlocked level
    private void CheckAllLevels()
    {
        for (int i = 0; i < _levels.Length; i++)
        {
            if (i <= highestUnlockedLevel)
                UnlockLevel(i);
            else
                LockLevel(i);
        }
    }

    public void LockLevel(int index)
    {
        Transform levelTransform = _levels[index].transform;

        if (levelTransform.childCount >= 2)
            levelTransform.GetChild(1).gameObject.SetActive(true);  // Enable the locked child
    }

    public void UnlockLevel(int index)
    {
        if (_levels != null)
        {
            Transform levelTransform = _levels[index].transform;

            if (levelTransform.childCount >= 2)
                levelTransform.GetChild(1).gameObject.SetActive(false);  // Disable the locked child
        }
    }

    // Call this method when a level is completed to unlock the next one
    public void CompleteLevel(int index)
    {
        if (index < _levels.Length - 1 && index == highestUnlockedLevel)
        {
            highestUnlockedLevel++;
            UnlockLevel(highestUnlockedLevel);  // Unlock the next level
        }
    }

    // Optionally: Use PlayerPrefs to save the level progress across sessions
    public void SaveProgress()
    {
        PlayerPrefs.SetInt("HighestUnlockedLevel", highestUnlockedLevel);
    }

    public void LoadProgress()
    {
        highestUnlockedLevel = PlayerPrefs.GetInt("HighestUnlockedLevel", 0);  // Level 0 (first level) is unlocked by default
        CheckAllLevels();  // Update the visual state of all levels
    }

    // Automatically update levels when values change in the Inspector
    private void OnValidate()
    {
        CheckAllLevels();  // Reflect any changes in the Inspector in real-time
    }
}
