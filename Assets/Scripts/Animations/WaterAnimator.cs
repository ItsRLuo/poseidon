using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterAnimator : MonoBehaviour {

	public float movementSpeed = 1f;
	public float amplitude = 1f;
	public float sinAmplitude = 0.4f;
	public float sinFrequence = 1f;
	public float sinSpeed = 1f;

	MeshFilter _renderer;
	Mesh _mesh;
	List<Vector3> _vertices;
	List<Vector3> _verticesStart;

	float leftBorder = 0;
	float rightBorder = 0;

	// Use this for initialization
	void Start () {
		_renderer = GetComponent<MeshFilter>();
		_mesh = _renderer.mesh;
		_vertices = new List<Vector3>(_mesh.vertices);
		_verticesStart = new List<Vector3>(_vertices);

		foreach (var vertices in _vertices)
		{
			leftBorder = Mathf.Min(vertices.x, leftBorder);
			rightBorder = Mathf.Max(vertices.x, rightBorder);
		}
	}
	
	// Update is called once per frame
	void Update () {
		for (int i = 0, l = _vertices.Count; i < l; ++i)
		{
			var v = _vertices[i];
			var vs = _verticesStart[i];

 			if (v.y > 0 && 
			 	Mathf.Abs(v.x - leftBorder) > 1f && 
				Mathf.Abs(v.x - rightBorder) > 1f) {
					var yChange = Mathf.PerlinNoise(v.x, Time.time * movementSpeed + v.z) - 0.5f;
					yChange = yChange * amplitude + 
						Mathf.Sin(Time.time * sinSpeed + v.x * sinFrequence) * sinAmplitude;

					yChange = Mathf.Clamp(yChange, -1, 1);

				v.y = vs.y + yChange;
			 }
			_vertices[i] = v;
		}

		_mesh.SetVertices(_vertices);
	}
}
