using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager myInstance = null;

    public static SoundManager GetInstance()
    {
        return myInstance;
    }

    private AudioSource myAudioSource = null;

    private void Awake()
    {
        myInstance = this;
        myAudioSource = GetComponent<AudioSource>();
    }

    public void PlayBackgroundMusic()
    {
        myAudioSource.Play();
    }
}
