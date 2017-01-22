using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Spiral : MonoBehaviour {
	public float m_Drift = 0.01f;
	public float LIFE_TIME = 0.01f;
	public int maxParticles = 2;

	ParticleSystem ps;
	ParticleSystem.Particle[] m_Particles;

	// Use this for initialization
	void Start () {
		ps = this.GetComponent<ParticleSystem>();
	}

	void animateSpirlaParticle() {
		// GetParticles is allocation free because we reuse the m_Particles buffer between updates
		int numParticlesAlive = ps.GetParticles(m_Particles);
		var circleSpeed = 1f;
		var forwardSpeed = -1f; // Assuming negative Z is towards the camera
		var circleSize = 0.1f;
		var circleGrowSpeed = 0.1f;

		// Change only the particles that are alive
		for (int i = 0; i < numParticlesAlive; i++) {
			float remainingLifeTime = m_Particles[i].remainingLifetime;

			if (remainingLifeTime > 2.0f) {
				// circleSize += timeDelta * circleGrowSpeed;
				Vector3 newPos = new Vector3(
					m_Particles[i].position.x,
					remainingLifeTime * Mathf.Cos(Time.time * remainingLifeTime),
					remainingLifeTime * Mathf.Sin(Time.time * remainingLifeTime)
				);

				m_Particles[i].position = Vector3.Lerp(m_Particles[i].position, newPos, 0.85f * Time.deltaTime);
			}
		}

		// Apply the particle changes to the particle system
		ps.SetParticles(m_Particles, numParticlesAlive);
	}

	void initializeIfNeeded() {
		if (ps == null) ps = GetComponent<ParticleSystem>();

		if (m_Particles == null || m_Particles.Length < ps.maxParticles)
			m_Particles = new ParticleSystem.Particle[ps.maxParticles]; 
	}

	void Update() {
		ParticleSystem.EmissionModule em = ps.emission;
 	}

	// Update is called once per frame
	void LateUpdate () {
		initializeIfNeeded();
		animateSpirlaParticle();
	}
}
