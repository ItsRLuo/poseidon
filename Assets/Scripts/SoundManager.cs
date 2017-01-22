using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioClip[] soundClips;
    public AudioClip takeDamage;
    private AudioSource audioSource;

	void Start () {
        audioSource = gameObject.GetComponent<AudioSource>();
	}
	
	void Update () {
		
	}

    public void PlayRandomClip()
    {
        if (soundClips.Length < 1)  { return; }
        if (audioSource == null || audioSource.isPlaying == true) { return; }
        int randomClip = Random.Range(0, soundClips.Length);
        audioSource.clip = soundClips[randomClip];
        audioSource.Play();
    }

    public void PlayTakeDamage()
    {
        audioSource.Stop();
        audioSource.clip = takeDamage;
        audioSource.Play();
    }
}
