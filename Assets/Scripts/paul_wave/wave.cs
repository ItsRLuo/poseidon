using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wave : MonoBehaviour {
	public MeshFilter meshf;
	public MeshCollider meshC;

	// amplitude
	public const float A = 2f;
	// angular freq
	public const float W = 1.5f;

	// const
	public const float B = 1;
	public const float C = 1;

	public const float CLIP_INTERVAL = 25;

	public float currentA = A;
	public float? lastClickTime = null;
	public Vector3? lastMouseClick = null;

	// Use this for initialization
	void Start () {
		this.meshf = this.GetComponent<MeshFilter>();
		this.meshC = this.GetComponent<MeshCollider>();
	}

	// (A * sin(w*x) + B)/ln(x + C)
	Vector3 waveFunction(Vector3 vert, float timeDelta) {
		float x = vert.x;
		float y;
        float mult = 1;

        if (lastMouseClick.HasValue) {
            if (x < lastMouseClick.Value.x) {
                mult = -1;
            }
        }

		y = currentA * Mathf.Sin(x * timeDelta * W) / (Mathf.Exp(x * mult));
        y = float.IsNaN(y) ? 0 : y;

		return new Vector3(x, y, vert.z);
	}

	void generateWave() {
		Mesh mesh = this.meshf.mesh;
		Vector3[] vertices = mesh.vertices;

		for (int index = 0; index < mesh.vertices.Length; index++) {
			Vector3 vertex = vertices[index];

			// no click or mouse click expired
			if (!lastClickTime.HasValue) {
				vertices[index] = waveFunction(vertex, 0);
			} else {
				vertices[index] = waveFunction(vertex, Time.time - lastClickTime.Value);
			}
		}

		mesh.vertices = vertices;
		mesh.RecalculateBounds();
		this.meshC.sharedMesh = mesh;
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0)) {
			lastClickTime = Time.time;
			lastMouseClick = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, Camera.main.nearClipPlane));
			currentA = Random.Range(-2 * A, 2 * A);
			Debug.Log("I clicked " + lastMouseClick.Value.x.ToString() + ", " + lastMouseClick.Value.y.ToString());
		}

		if (lastClickTime.HasValue && Time.time - lastClickTime.Value > CLIP_INTERVAL) {
			lastClickTime = null;
			lastMouseClick = null;
		}

		generateWave();
	}
}
