using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnScreenPositioner : MonoBehaviour {

	public Vector2 screen;

	private Renderer _renderer;

	// Use this for initialization
	void Start () {
		_renderer = GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update () {
		var w = Camera.main.ViewportToWorldPoint(screen);

		var position = transform.position;
		position.x = w.x + _renderer.bounds.extents.x;
		position.y = w.y + _renderer.bounds.extents.y;

		transform.position = position;
	}
}
