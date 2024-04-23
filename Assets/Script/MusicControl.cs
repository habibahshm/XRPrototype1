using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicControl : MonoBehaviour
{
    private AudioSource _audio;
    private bool isPlaying = true;
    void Start()
    {
        _audio = GetComponent<AudioSource>();
    }

    public void Toggle()
    {
        if(isPlaying)
            _audio.Pause();
        else
            _audio.Play();
        isPlaying = !isPlaying;
    }
}
