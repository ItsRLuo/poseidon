using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (other.name== "Sphere")
        {
            other.transform.position = new Vector3(9.8f, 20, 25);
        }
    }
}
