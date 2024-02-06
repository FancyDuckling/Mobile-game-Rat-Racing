using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Sound 
{
    public string name;
    public AudioClip clip;
    public AudioSource source;
    public float volume;
    public bool loop;
}
