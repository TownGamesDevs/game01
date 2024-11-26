using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public enum Category
    {
        Zombie,
        Weapons,
        Music,
        Other
        // Add more sound categories as needed
    }

    // current sounds
    [SerializeField] private Sound[] _zombie;
    [SerializeField] private Sound[] _weapons;
    [SerializeField] private Sound[] _music;
    [SerializeField] private Sound[] _other;

    public bool _canPlay { get; set; } = true;
    const string NOT_FOUND = "No sounds found for subcategory: ";

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
        InitializeAllSounds();
    }

    private void InitializeAllSounds()
    {
        InitializeSound(_zombie);
        InitializeSound(_weapons);
        InitializeSound(_music);
        InitializeSound(_other);
        // Add more as needed
    }


    private void InitializeSound(Sound[] soundCategories)
    {
        // exit early
        if (soundCategories.Length <= 0) return;

        foreach (Sound soundCategory in soundCategories)
            foreach (MainSoundClass mainSound in soundCategory.sounds)
            {
                mainSound.source = gameObject.AddComponent<AudioSource>();
                mainSound.source.clip = mainSound.clip;
                mainSound.source.volume = mainSound.volume;
                mainSound.source.pitch = mainSound.pitch;
                mainSound.source.loop = mainSound.loop;
            }

    }
    private Sound[] GetSoundArrayByCategory(Category category)
    {
        switch (category)
        {
            case Category.Zombie:
                return _zombie;
            case Category.Weapons:
                return _weapons;
            case Category.Music:
                return _music;
            case Category.Other:
                return _other;
            default:
                return null;
        }
    }

    private Sound TryGetSubCategory(Category category, string subCategory)
    {
        Sound[] soundArray = GetSoundArrayByCategory(category);
        Sound subCategorySounds = System.Array.Find(soundArray, s => s.category == subCategory);

        if (subCategorySounds == null || subCategorySounds.sounds.Length == 0)
        {
            print(NOT_FOUND + subCategory);
            return null;
        }

        return subCategorySounds;
    }
    public void Play(Category category, string subCategory, string soundName)
    {
        // exit early
        if (!_canPlay) return;

        Sound subCat = TryGetSubCategory(category, subCategory);


        // Find the specific sound by name within the subcategory
        MainSoundClass specificSound = System.Array.Find(subCat.sounds, s => s.name == soundName);

        if (specificSound == null)
        {
            print("Sound " + soundName + " not found in subcategory: " + subCategory);
            return;
        }

        // Play the specific sound
        specificSound.source.Play();

    }
    public void PlayOneShot(Category category, string subCategory, string soundName)
    {
        if (_canPlay)
        {
            // Get the sound array for the given category
            Sound[] soundArray = GetSoundArrayByCategory(category);
            if (soundArray == null)
            {
                Debug.LogWarning("No sounds found for category: " + category);
                return;
            }

            // Find the subcategory within the sound array
            Sound subCategorySounds = System.Array.Find(soundArray, s => s.category == subCategory);
            if (subCategorySounds == null || subCategorySounds.sounds.Length == 0)
            {
                Debug.LogWarning("No sounds found for subcategory: " + subCategory);
                return;
            }

            // Find the specific sound by name within the subcategory
            MainSoundClass specificSound = System.Array.Find(subCategorySounds.sounds, s => s.name == soundName);
            if (specificSound == null)
            {
                Debug.LogWarning("Sound " + soundName + " not found in subcategory: " + subCategory);
                return;
            }

            // Play the specific sound using PlayOneShot
            specificSound.source.PlayOneShot(specificSound.clip);
        }
    }

    public void Stop(Category category, string subCategory, string soundName)
    {
        // Get the sound array for the given category
        Sound[] soundArray = GetSoundArrayByCategory(category);

        // Find the subcategory in the sound array
        Sound subCategorySounds = System.Array.Find(soundArray, s => s.category == subCategory);

        if (subCategorySounds == null || subCategorySounds.sounds.Length == 0)
        {
            print("No sounds found for subcategory: " + subCategory);
            return;
        }

        // Find the specific sound by name within the subcategory
        MainSoundClass specificSound = System.Array.Find(subCategorySounds.sounds, s => s.name == soundName);

        if (specificSound == null)
        {
            print("Sound " + soundName + " not found in subcategory: " + subCategory);
            return;
        }

        // Stop the specific sound
        specificSound.source.Stop();

    }
    public void PlayRandomSound(Category category, string subCategory)
    {
        if (_canPlay)
        {
            // Get the sound array for the given category
            Sound[] soundArray = GetSoundArrayByCategory(category);

            // Find the subcategory in the sound array
            Sound subCategorySounds = System.Array.Find(soundArray, s => s.category == subCategory);

            if (subCategorySounds == null || subCategorySounds.sounds.Length == 0)
            {
                print("No sounds found for subcategory: " + subCategory);
                return;
            }

            // Select a random sound from the subcategory array
            int randomIndex = UnityEngine.Random.Range(0, subCategorySounds.sounds.Length);
            MainSoundClass randomSound = subCategorySounds.sounds[randomIndex];

            // Play the randomly selected sound
            randomSound.source.Play();
        }
    }
    public void StopAll()
    {
        StopSound(_zombie);
        StopSound(_weapons);
        StopSound(_music);
        StopSound(_other);
    }
    public void StopSound(Sound[] soundCategories)
    {
        foreach (Sound soundCategory in soundCategories)
            foreach (MainSoundClass mainSound in soundCategory.sounds)
                mainSound.source.Stop();
    }

    // Methods of playing sounds
    //public void Play1(Category category, string soundName)
    //{
    //    if (CanPlaySound)
    //    {
    //        Sound[] soundArray = GetSoundArray(category);
    //        if (soundArray == null) return;

    //        foreach (Sound soundCategory in soundArray)
    //        {
    //            MainSoundClass mainSound = Array.Find(soundCategory.sounds, s => s.name == soundName);
    //            if (mainSound == null)
    //            {
    //                print("Sound " + soundName + " not found in category " + category.ToString());
    //                return;
    //            }

    //            mainSound.source.Play();
    //        }
    //    }
    //}


    //public void Stop1(Category category, string soundName)
    //{
    //    Sound[] soundArray = GetSoundArray(category);
    //    if (soundArray == null) return;

    //    foreach (Sound soundCategory in soundArray)
    //    {
    //        MainSoundClass mainSound = Array.Find(soundCategory.sounds, s => s.name == soundName);
    //        if (mainSound == null)
    //        {
    //            ErrorManager.instance.PrintError("Sound " + soundName + " not found in category " + category);
    //            return;
    //        }

    //        mainSound.source.Stop();
    //    }
    //}






    // Unique sounds
    public void MouseHover()
    {
        Play(Category.Other, "UI", "MouseHover");
    }
    public void MouseSelection()
    {
        Play(Category.Other, "UI", "Selection");
    }
}
