using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sfx;
    public Sound[] sounds;
    [HideInInspector] public Sound curMusic;
    [HideInInspector] public Sound curSfx;
   

    public static AudioManager instance;
    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
         PlayMusic("music");
    }
    public void PlayAudio(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.namee == name);
        sound.audioSource = sfx;
        sound.audioSource.clip = sound.clip;
        sound.audioSource.pitch = sound.pitch;
        sound.audioSource.volume = sound.volume;
        sound.audioSource.loop = sound.loop;
        sound.audioSource.Play();
        curSfx = sound;
    }
    public void PlayMusic(string name)
    {
        Sound sound = Array.Find(sounds, sound => sound.namee == name);
        sound.audioSource = music;
        sound.audioSource.clip = sound.clip;
        sound.audioSource.pitch = sound.pitch;
        sound.audioSource.volume = sound.volume;
        sound.audioSource.loop = sound.loop;
        sound.audioSource.Play();
        curMusic = sound;
    }
}
