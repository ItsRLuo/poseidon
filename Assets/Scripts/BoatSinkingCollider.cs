using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatSinkingCollider : MonoBehaviour
{
    const int WATER_LAYER = 4;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider collider) {
		BoatController boatController = GetComponentInParent<BoatController>();

		if (collider.gameObject.name.Contains("Stone")) {
			boatController.Smoke();
			boatController.Sink();
		}

        //if (collider.GetComponent<WaterPixel>() != null)
        if (collider.gameObject.layer == WATER_LAYER) {
            // we have hit the water in a bad way, time to die
            if (boatController != null)
			boatController.Sink();
        }
    }
}
