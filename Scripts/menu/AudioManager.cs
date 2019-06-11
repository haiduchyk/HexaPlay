using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour, IObserver
{
    public AudioClip lose, win, tap;
    public AudioSource MusicSource;
    Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    
    void Start()
    {
        MusicSource.clip = tap;
        clips.Add("tap", tap);
        clips.Add("win", win);
        clips.Add("lose", lose);
    }
    public void Play()
    {
        if (PlayerPrefs.GetString("Music") != "no") MusicSource.Play();
    }
    public void Upd(string key) => MusicSource.clip = clips[key];
    
    
}
