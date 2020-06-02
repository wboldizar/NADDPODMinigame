using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class AudioControl : MonoBehaviour
{
    public Sound[] allSounds;

    public static AudioControl firstInstance;

    private void Awake()
    {
        if (firstInstance == null)
        {
            firstInstance = this;
        }

        DontDestroyOnLoad(gameObject);
        foreach(Sound s in allSounds)
        {
            s.clipSource = gameObject.AddComponent<AudioSource>();

            s.clipSource.clip = s.clip;
            s.clipSource.volume = s.clipVolume;
            s.clipSource.loop = s.loop;
        }
    }

    private void Start()
    {
        if(firstInstance == this)
        {
            Play("themeSong");
        }
        else
        {
            AudioControl titleAudio = GameObject.Find("AudioManager").GetComponent<AudioControl>();
            Sound theme = Array.Find(titleAudio.allSounds, sound => sound.clipName == "themeSong");
            theme.clipVolume = 0.1f;
            theme.clipSource.volume = 0.10f;
        }
    }

    public void Play(string clipName)
    {
        Sound searchSound = Array.Find(allSounds, sound => sound.clipName == clipName);

        if (searchSound != null)
        {
            searchSound.clipSource.Play();
        }
    }

    public void Stop(string clipName)
    {
        Sound searchSound = Array.Find(allSounds, sound => sound.clipName == clipName);

        if(searchSound != null)
        {
            searchSound.clipSource.Stop();
        }
    }
}
