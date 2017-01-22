using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {
	public int energy = 100;
	public int refillAmt = 25;
	public int drainStepAmt = 3;

	public MeshFilter container;
	public MeshFilter energyBar;

	const int MAX_ENERGY = 100;

	public int Energy {
		get { return energy; }
	}

	void DrainEnergy(int dropAmount) {
		this.energy = (int) Mathf.Max(this.energy - dropAmount, 0);
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		this.energy = (int) Mathf.Max(this.energy, 0);
		draw();
	}

	void draw() {
		Vector3 newScale = new Vector3(
			this.energy / (float)MAX_ENERGY,
			this.energyBar.transform.localScale.y ,
			this.energyBar.transform.localScale.z
		);
		Vector3 newPos = new Vector3(
			(1f - this.energy/(float)MAX_ENERGY) / -2f ,
			this.energyBar.transform.localPosition.y ,
			this.energyBar.transform.localPosition.z
		);

		if (this.energy >= 20) {
			this.energyBar.transform.localPosition = Vector3.Lerp(this.energyBar.transform.localPosition, newPos, 0.8f);
		} else {
			this.energyBar.transform.localPosition = newPos;
		}
		this.energyBar.transform.localScale = newScale;
	}

	public bool Full() {
		return this.energy == MAX_ENERGY;
	}

	public void Refill() {
		this.energy += this.refillAmt;
		this.energy = (int) Mathf.Min(this.energy, MAX_ENERGY);
	}

	public void Deplete() {
		this.energy = 0;
	}
}
