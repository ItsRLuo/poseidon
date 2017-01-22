using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public static class AudioUtils
{
    public static void PlayAudioClip(AudioClip audioClip, AudioSource audioSource)
    {
        audioSource.clip = audioClip;
        audioSource.volume = 1;
        audioSource.time = 0;
        audioSource.pitch = UnityEngine.Random.Range(0.75f, 1.25f);
        audioSource.Play();
    }
}
