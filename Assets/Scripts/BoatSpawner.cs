using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSpawner : MonoBehaviour
{
    public float BoatsPerSecond = 0.5f;
    public BoatController BoatPrefab;

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
            var boat = GameObject.Instantiate<BoatController>(BoatPrefab);
            boat.transform.position = transform.position;
        }
	}
}
