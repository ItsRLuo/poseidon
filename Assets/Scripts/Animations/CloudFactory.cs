using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudFactory : MonoBehaviour {

	private class CloudChild {
		public Transform cloud;
		public float speed;
		public float width;
	}

	public Transform[] CloudPrefabs;

	public float width = 20f;

	private List<CloudChild> _clouds;

	public int minSpawnHeight = -2;
	public int maxSpawnHeight = 2;

	public Vector3 minScale = new Vector3(0.7f, 0.7f, 0.7f);
	public Vector3 maxScale = new Vector3(0.7f, 0.7f, 0.7f);

	public float minSpeed = 0.2f;
	public float maxSpeed = 0.5f;

	// Use this for initialization
	void Start () {
		_clouds = new List<CloudChild>(10);

		for (int i = 0, count = 10; i < count; i++) {
			var t = SpawnCloud();

			t.localPosition += Vector3.left * width * i / count;
		}
	}
	
	// Update is called once per frame
	void Update () {
		var minPos = -width;
		var lastWidth = 0f;
		for (var i = 0; i < _clouds.Count; ++i)
		{
			var cloud = _clouds[i];

			cloud.cloud.localPosition += Vector3.left * Time.deltaTime * cloud.speed;

			if (cloud.cloud.localPosition.x < -width) {
				_clouds.RemoveAt(i);
				i--;

				Destroy(cloud.cloud.gameObject);
				continue;
			}

			if (cloud.cloud.localPosition.x + cloud.width / 2 > minPos) {
				minPos = cloud.cloud.localPosition.x;
				lastWidth = cloud.width;
			}
		}
		
		if (0 - minPos > lastWidth) {
			SpawnCloud();
		}
	}

	Transform SpawnCloud() {
		var trnsfrm = Instantiate(CloudPrefabs[Random.Range(0, CloudPrefabs.Length)]);

		var child = new CloudChild {
			cloud = trnsfrm,
			speed = Random.Range(minSpeed, maxSpeed),			
		};

		trnsfrm.SetParent(transform);
		trnsfrm.localPosition = Vector3.up * Random.Range(minSpawnHeight, maxSpawnHeight);
		trnsfrm.localScale = new Vector3(Random.Range(minScale.x, maxScale.x), 
										 Random.Range(minScale.y, maxScale.y), 
										 Random.Range(minScale.z, maxScale.z));

		if (Random.Range(0f, 1f) > 0.5f) {
			child.cloud.localEulerAngles = new Vector3(0, 180, 0);
		}

		var renderer = child.cloud.GetComponent<MeshRenderer>();
		child.width = renderer.bounds.max.x * trnsfrm.localScale.x;

		_clouds.Add(child);

		return trnsfrm;
	}

	/// <summary>
	/// Callback to draw gizmos that are pickable and always drawn.
	/// </summary>
	void OnDrawGizmos()
	{
		var p = transform.position;
		Gizmos.DrawLine(p, p - transform.right * width);
		p += Vector3.up * maxSpawnHeight;
		Gizmos.DrawLine(p, p - transform.right * width);
		p += Vector3.down * (maxSpawnHeight - minSpawnHeight);
		Gizmos.DrawLine(p, p - transform.right * width);
	}
}
