using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public AudioSource audioSource;
    public AudioClip clip;
    public string namee;
    [Range(0,1)]
    public float volume;
    [Range(0, 1)]
    public float pitch;
    public bool loop;
}
