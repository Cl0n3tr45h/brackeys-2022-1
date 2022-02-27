using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public SoundOptions[] sounds;

    private AudioSource audio;

    public static AudioManager instance;
    
    private float fadeTime = 2f;

    private void Awake()
    {
        //No double audiomanager switching scenes
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        
        foreach (SoundOptions s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
        
        //SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void Start()
    {
       
    }

    public void Play(string name)
    {
        SoundOptions s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
            return;
        s.source.Play();
    }
}
