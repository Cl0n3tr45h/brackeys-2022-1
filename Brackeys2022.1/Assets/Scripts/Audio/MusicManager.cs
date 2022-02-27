using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioClip[] TracksObject;
    public static AudioClip[] Tracks;
    public static AudioSource track01, track02;
    private bool isPlayingtrack01;

    public static MusicManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Start()
    {
        Tracks = new AudioClip[TracksObject.Length];
        for (int i = 0; i < Tracks.Length; i++)
        {
            Tracks[i] = TracksObject[i];
        }
        track01 = gameObject.AddComponent<AudioSource>();
        track02 = gameObject.AddComponent<AudioSource>();
       // Destroy(gameObject.AddComponent<AudioSource>());
        isPlayingtrack01 = true;
        track01.clip = Tracks[0];
        //SwapTrack(defaultTrack);
    }

    public static void OnNewLevel(int _levelIndex)
    {
        track01.Stop();
        track01.clip = Tracks[_levelIndex % 2];
        track01.Play();
    }

    /*public void SwapTrack(AudioClip newClip)
    {
        if (isPlayingtrack01)
        {
            track02.clip = newClip;
            track02.Play();
            track01.Stop();
        }
        else
        {
            track01.clip = newClip;
            track01.Play();
            track02.Stop();
        }

        isPlayingtrack01 = !isPlayingtrack01;
    }

    public void ReturnToDefault()
    {
        SwapTrack(defaultTrack);
    }*/
}
