using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boat : MonoBehaviour
{
    public Vector3 Velocity = new Vector3(10f, 0f, 0f);

	// Use this for initialization
	void Start ()
    {
        		
	}
	
	// Update is called once per frame
	void Update ()
    {
        gameObject.transform.position += Time.deltaTime * Velocity;
	}
}
