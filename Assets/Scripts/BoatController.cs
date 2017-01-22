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
    private bool IsExploding = false;
    private float sinkingTime;
    private const float TimeToSinkShrink = 6f;
    private Vector3 naturalScale;

    public AudioClip SplashClip;
    public AudioSource AudioSource;
    public int Points;
    public ParticleSystem SmokePS;
    public GameObject ExplosionPrefab;

    public string[] validBoatNames = { "Rowboat", "Warship" };
    public string boatName;

    private void Awake()
    {
        naturalScale = transform.localScale;
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
            AudioUtils.PlayAudioClip(this.SplashClip, this.AudioSource, 0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSinking)
        {
            float t = sinkingTime / TimeToSinkShrink;
            if (t < 1)
            {
                gameObject.transform.localScale = (1 - t) * naturalScale;
                sinkingTime += Time.deltaTime;
            }
        }

        if (IsSinking || IsExploding) { return; }
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
        if (IsSinking)
        {
            return;
        }

        float randomTorqueX = UnityEngine.Random.Range(-50, 50);
        float randomTorqueY = UnityEngine.Random.Range(-50, 50);
        float randomTorqueZ = UnityEngine.Random.Range(-10, 10);
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddTorque(new Vector3(randomTorqueX, randomTorqueY, randomTorqueZ));

        IsSinking = true;

        if (IsExploding == false)
        {
            GetComponent<Rigidbody>().drag = 6f;
        }
        else
        {
            GetComponent<Rigidbody>().drag = 0;
        }

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

        int randomChance = UnityEngine.Random.Range(0, 5);
        if (randomChance == 0) {
            SoundManager soundManager = FindObjectOfType<SoundManager>();
			if (soundManager != null) soundManager.PlayRandomClip();
        }
    }

    public void Explode(Vector3 explosionPosition)
    {
        float randomForceX = UnityEngine.Random.Range(-100, -50);
        float randomForceY = UnityEngine.Random.Range(100, 300);

        float randomTorqueX = UnityEngine.Random.Range(-150, 150);
        float randomTorqueY = UnityEngine.Random.Range(-150, 150);
        float randomTorqueZ = UnityEngine.Random.Range(-500, 500);

        IsExploding = true;
        Vector3 force = new Vector3(randomForceX, randomForceY, 0);
        if (boatName == validBoatNames[0])
        {
            force = force * 0.5f;
        }
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        GetComponent<Rigidbody>().AddForce(force);
        GetComponent<Rigidbody>().AddTorque(new Vector3(randomTorqueX, randomTorqueY, randomTorqueZ));
    }
}
