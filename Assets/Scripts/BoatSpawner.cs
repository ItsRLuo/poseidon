using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BoatSpawnProbability
{
    public BoatController BoatPrefab;
    public float Probability;
}

public class BoatSpawner : MonoBehaviour
{
    public float BoatsPerSecond = 0.5f;
    public AudioSource AudioSource;
    public AudioClip SplashClip;
    public BoatSpawnProbability[] Probabilities;

	// Use this for initialization
	void Start ()
    {

	}

	// Update is called once per frame
	void Update ()
    {
        float spawnProb = Time.deltaTime * BoatsPerSecond;
        if (spawnProb > Random.value)
        {
            SpawnABoat();
        }
	}

    private void SpawnABoat()
    {
        float random = Random.value;
        float cumProb = 0;
        BoatController prefab = null;
        
        foreach (var boatProbability in Probabilities)
        {
            cumProb += boatProbability.Probability;
            if (random < cumProb)
            {
                prefab = boatProbability.BoatPrefab;
                break;
            }
        }

        var boat = GameObject.Instantiate<BoatController>(prefab);
        boat.AudioSource = AudioSource;
        boat.SplashClip = SplashClip;
        boat.transform.position = transform.position;
    }
}
