using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchAnimation : MonoBehaviour {

    Vector3 endPosition;

    public void SetEndPosition(Vector3 newPosition)
    {
        endPosition = newPosition;
    }
	
	void Update () {
        transform.position = Vector3.MoveTowards(transform.position, endPosition, 40 * Time.deltaTime);
        if (Vector3.Distance(transform.position, endPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
	}
}
