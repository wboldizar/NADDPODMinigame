using UnityEngine.Audio;
using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string clipName;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float clipVolume;

    public bool loop = false;

    [HideInInspector]
    public AudioSource clipSource;
}
