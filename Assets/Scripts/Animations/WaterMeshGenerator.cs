using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterMeshGenerator : MonoBehaviour
{

    public Material waterMaterial;

	public float movementSpeed = 0.75f;
	public float amplitude = 0.5f;
	public float sinAmplitude = 0;
	public float sinFrequence = 0.6f;
	public float sinSpeed = 1.5f;

    MeshFilter _meshFilter;
    Mesh _mesh;

    List<Vector3> _vertices;
	List<Vector3> _startVertices;
    List<int> _indices;

    bool firstTime = false;

	private Ocean _ocean;

    // Use this for initialization
    void Start()
    {
		_ocean = GetComponent<Ocean>();

        _vertices = new List<Vector3>();
        _indices = new List<int>();

        firstTime = true;

        var go = new GameObject("Ocean Mesh");
        _meshFilter = go.AddComponent<MeshFilter>();
        _meshFilter.mesh = _mesh = new Mesh();
		_mesh.name = "Ocean Mesh";

		var mr = go.AddComponent<MeshRenderer>();
		mr.material = waterMaterial;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (firstTime)
        {
            firstTime = false;

			_vertices.Clear();
			_indices.Clear();

			generateMesh();

            _mesh.SetVertices(_vertices);
            _mesh.SetIndices(_indices.ToArray(), MeshTopology.Triangles, 0);

            _mesh.RecalculateNormals();
        } else {
			updateMesh();
			_mesh.SetVertices(_vertices);
		}
    }

	void generateMesh() {
		var _waveWidth = 0.1f;
		var waveHeight = _ocean.GetScreenHeight / 2;
		var waveDepth = 20f;

		var step = 5;

		for(var x = step; x < _ocean.GetScreenWidth - step - 1; x += step) {
			var waveWidth = step * _waveWidth;

			var waterPixel = _ocean.GetWaterArray[x].transform.localPosition;
			var nextPixel = _ocean.GetWaterArray[x + step].transform.localPosition;
			var offset = new Vector3(0, 0, 20);

			var v1 = new Vector3(waterPixel.x, waveHeight + waterPixel.y, -waveDepth / 2) + offset;
			var v2 = new Vector3(nextPixel.x, waveHeight + nextPixel.y, -waveDepth / 2) + offset;
			var v3 = new Vector3(nextPixel.x, waveHeight + nextPixel.y, waveDepth / 2) + offset;
			var v4 = new Vector3(waterPixel.x, waveHeight + waterPixel.y, waveDepth / 2) + offset;

			var v5 = new Vector3(waterPixel.x, -waveHeight, -waveDepth / 2) + offset;
			var v6 = new Vector3(nextPixel.x, -waveHeight, -waveDepth / 2) + offset;

			AddStrip(v1, v2, v3, v4);

			AddQuad(v5, v6, NoiseVector(v2), NoiseVector(v1));
		}

		_startVertices = new List<Vector3>(_vertices);
	}

	void updateMesh() {
		var waveHeight = _ocean.GetScreenHeight / 2;
		for (int i = 0, l = _vertices.Count; i < l; ++i)
		{
			var v = _vertices[i];
			var vs = NoiseVector(_startVertices[i]) - _startVertices[i];

			if (v.y < 0) {
				continue;
			}

			var x = (int)(v.x * 10);

			var yChange = Mathf.PerlinNoise(v.x, Time.time * movementSpeed + v.z) - 0.5f;
					yChange = yChange * amplitude + 
						Mathf.Sin(Time.time * sinSpeed + v.x * sinFrequence) * sinAmplitude;

			v.y = _ocean.GetWaterArray[x].transform.localPosition.y + waveHeight + vs.y + yChange;

			_vertices[i] = Vector3.Lerp(_vertices[i], v, Time.deltaTime * 20f);
		}
	}

	Vector3 NoiseVector(Vector3 v) {
		v.y += (Mathf.PerlinNoise(v.x, v.z) - 0.5f);
		return v;
	}

	void AddStrip(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
		var subdivisions = 4f;

		var _1 = NoiseVector(v1);
		var _2 = NoiseVector(v2);

		for (var i = 1; i < subdivisions; i++) {
			var _3 = NoiseVector(Vector3.Lerp(v2, v3, i / subdivisions));
			var _4 = NoiseVector(Vector3.Lerp(v1, v4, i / subdivisions));

			AddQuad(_1, _2, _3, _4);

			_1 = _4;
			_2 = _3;
		}

		AddQuad(_1, _2, NoiseVector(v3), NoiseVector(v4));
	}

	void AddQuad(Vector3 v1, Vector3 v2, Vector3 v3, Vector3 v4) {
		AddTriangle(v1, v3, v2);
		AddTriangle(v1, v4, v3);
	}

	void AddTriangle(Vector3 v1, Vector3 v2, Vector3 v3) {
		var i = _vertices.Count;

		_vertices.Add(v1);
		_vertices.Add(v2);
		_vertices.Add(v3);

		_indices.Add(i++);
		_indices.Add(i++);
		_indices.Add(i++);
	}
}
