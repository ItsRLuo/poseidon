using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Vector3 Velocity;
    public float SinkSpeed = 3.0f;
    private const int SINKING_LAYER = 8;

    private bool playedSplash;
    private bool IsSinking;

    public AudioClip SplashClip;
    public AudioSource AudioSource;

    void OnCollisionEnter(Collision collision)
    {
        if (playedSplash)
        {
            return;
        }

        playedSplash = true;

        if (this.AudioSource && this.SplashClip)
        {
            AudioUtils.PlayAudioClip(this.SplashClip, this.AudioSource);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }

    internal void Sink()
    {
        GetComponent<Rigidbody>().drag = 6f;
        Debug.Log("Glug glug glug");

        gameObject.layer = SINKING_LAYER;
        foreach (var subComponent in GetComponentsInChildren<Transform>())
        {
            subComponent.gameObject.layer = SINKING_LAYER;
        }

        FindObjectOfType<ScoreManager>().ScoreAtPoint(transform.position, 10);
    }
}
