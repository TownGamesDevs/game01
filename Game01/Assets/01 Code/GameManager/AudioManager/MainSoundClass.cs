using UnityEngine;

[System.Serializable]
public class MainSoundClass
{
    [HideInInspector] public AudioSource source;

    public string name;
    public AudioClip clip;

    [Range(0f, 1f)] public float volume = 1f;
    [Range(.1f, 3f)] public float pitch = 1f;
    public bool loop;
}

[System.Serializable]
public class Sound
{
    public string category;
    public MainSoundClass[] sounds;    // Array of sounds for each weapon
}