using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Meter : MonoBehaviour {
	public int energy = 100;
	public int refillAmt = 1;
	public int drainStepAmt = 3;

	public MeshFilter container;
	public MeshFilter energyBar;

	const int MAX_ENERGY = 100;

	int drainAmt = 0;

	public int Energy {
		get { return energy; }
	}

	void DrainEnergy(int dropAmount) {
		this.energy = (int) Mathf.Min(this.energy - dropAmount, 0);
	}

	void FillEnergy() {
		this.energy += this.refillAmt;
		this.energy = (int) Mathf.Min(this.energy, MAX_ENERGY);
	}

	// Use this for initialization
	void Start () {
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			this.drainAmt = drainStepAmt;
		}
		if (Input.GetMouseButtonUp(0)) {
			this.drainAmt = 0;
		}

		this.energy -= this.drainAmt;
		this.energy = (int) Mathf.Max(this.energy, 0);

		FillEnergy();
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

		this.energyBar.transform.localPosition = newPos;
		this.energyBar.transform.localScale = newScale;
	}
}
