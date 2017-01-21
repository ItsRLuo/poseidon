using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterPixel {

    private Rigidbody rigidbody;

    public void Setup(GameObject newGameObject)
    {
        rigidbody = newGameObject.GetComponent<Rigidbody>();
    }

    public void AddForce(float force)
    {
        rigidbody.AddForce(new Vector3(0, force, 0));
    }

    public float GetVelocityMagnitude()
    {
        return rigidbody.velocity.magnitude;
    }
}
