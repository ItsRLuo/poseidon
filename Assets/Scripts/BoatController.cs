using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Vector3 Velocity;
    public float SinkSpeed = 3.0f;
    private const int SINKING_LAYER = 8;

    private bool IsSinking;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position += Velocity * Time.deltaTime;
	}

    internal void Sink()
    {
        GetComponent<Rigidbody>().drag = 6f;
        gameObject.layer = SINKING_LAYER;
        Debug.Log("Glug glug glug");

        FindObjectOfType<ScoreManager>().ScoreAtPoint(transform.position, 10);
    }
}
