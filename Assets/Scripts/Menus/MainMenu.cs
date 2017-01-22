using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    public int CreakMinTime = 5;
    public int CreakMaxTime = 10;

    public AudioClip CreakingBoatSound;
    public AudioSource AudioSource;

    // Use this for initialization
    public void Start()
    {
        StartCoroutine(PlayCreakSound());
    }

    private IEnumerator PlayCreakSound()
    {
        while (true)
        {
            float randomTime = Random.Range(this.CreakMinTime, this.CreakMaxTime);
            yield return new WaitForSeconds(randomTime);
            AudioUtils.PlayAudioClip(this.CreakingBoatSound, this.AudioSource, 0.5f);
        }
    }
}
