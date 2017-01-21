using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatController : MonoBehaviour
{
    public Vector3 Velocity;
    public float SinkSpeed = 3.0f;

    private bool IsSinking;

	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (IsSinking)
        //{
        //    //gameObject.transform.position += sinkVelocity;
        //}
        //else
        //{
            gameObject.transform.position += Velocity * Time.deltaTime;
        //}
	}

    internal void Sink()
    {
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
        //GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().drag = 6f;
        GetComponent<CapsuleCollider>().enabled = false;
        Debug.Log("Glug glug glug");
    }
}
