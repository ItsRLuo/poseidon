using UnityEngine;
using System.Collections;

public class QuitGame : MonoBehaviour {
	public void Update() {
		if (Input.GetKey ("escape"))
			Quit ();

	}

	public void Quit()
	{
		Application.Quit();
	}
}
