using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Vector3 Velocity;
    public float SinkSpeed = 3.0f;
    private const int SINKING_LAYER = 8;

    private bool playedSplash;
    private bool IsSinking = false;

    public AudioClip SplashClip;
    public AudioSource AudioSource;
    public int Points;
    public ParticleSystem SmokePS;
    public GameObject ExplosionPrefab;

    public string[] validBoatNames = { "Rowboat", "Warship" };
    public string boatName;

    private void Awake()
    {
        if (validBoatNames.Contains<string>(boatName) == false)
        {
            boatName = validBoatNames[0];
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (playedSplash || IsSinking) {
            return;
        }

        playedSplash = true;
        if (this.AudioSource && this.SplashClip) {
            AudioUtils.PlayAudioClip(this.SplashClip, this.AudioSource);
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
    }

	public void Smoke() {
		var smoke = GameObject.Instantiate<ParticleSystem>(SmokePS);
		smoke.transform.position = transform.position;
        var em = smoke.emission;
        em.enabled = true;
	}

    public bool Sinking {
        get { return IsSinking; }
    }

    public void Sink()
    {
        IsSinking = true;

        GetComponent<Rigidbody>().drag = 6f;

        gameObject.layer = SINKING_LAYER;
        foreach (var subComponent in GetComponentsInChildren<Transform>())
        {
            subComponent.gameObject.layer = SINKING_LAYER;
        }

        ScoreManager sm = FindObjectOfType<ScoreManager>();
        if (sm != null)
        {
            ScoreManager scoreManager = FindObjectOfType<ScoreManager>();
            scoreManager.ScoreAtPoint(transform.position, Points);
            scoreManager.SinkBoatOfType(boatName);
        }

        int randomChange = UnityEngine.Random.Range(0, 10);
        if (randomChange == 0) {
            SoundManager soundManager = FindObjectOfType<SoundManager>();
            soundManager.PlayRandomClip();
        }
    }
}
