using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSegment : MonoBehaviour
{
    public WaterSegment Next;
    public WaterSegment Previous;
    public bool IsBeingControlled;
    public Vector3 NaturalPosition;
    public float Gravity = 9.8f;

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        //if (!IsBeingControlled)
        //{
        //    if (transform.position.y - NaturalPosition.y > 0)
        //    {
        //        transform.position = new Vector3(NaturalPosition.x, transform.position.y - Gravity * Time.deltaTime, NaturalPosition.z);
        //    }

        //    if (transform.position.y - NaturalPosition.y < 0)
        //    {
        //        transform.position = new Vector3(NaturalPosition.x, transform.position.y + Gravity * Time.deltaTime, NaturalPosition.z);
        //    }
        //}

        //if (Input.GetMouseButtonUp(0))
        //{
        //    this.IsBeingControlled = false;
        //}
    }

    internal void Initialize()
    {
        NaturalPosition = transform.position;
    }
}
