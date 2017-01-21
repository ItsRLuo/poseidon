using UnityEngine;
using System.Collections;

public class  boat: MonoBehaviour {
	public Collider coll;

	void Start() {
		coll = GetComponent<Collider>();
	}

	void OnTriggerEnter(Collider other) {
		if (other.attachedRigidbody) {
			other.attachedRigidbody.useGravity = false;
		}

	}

	void OnCollisionEnter(Collision collisionInfo) {
		//Debug.Log("Detected collision between " + gameObject.name + " and " + collisionInfo.collider.name);
		//Debug.Log("There are " + collisionInfo.contacts.Length + " point(s) of contacts");
		//Debug.Log("Their relative velocity is " + collisionInfo.relativeVelocity);
	}

	void OnCollisionStay(Collision collisionInfo) {
		//Debug.Log(gameObject.name + " and " + collisionInfo.collider.name + " are still colliding");
	}

	void OnCollisionExit(Collision collisionInfo) {
		//Debug.Log(gameObject.name + " and " + collisionInfo.collider.name + " are no longer colliding");
	}
}
