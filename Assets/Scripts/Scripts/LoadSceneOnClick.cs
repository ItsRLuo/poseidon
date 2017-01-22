using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LoadSceneOnClick : MonoBehaviour {

	// Use this for initialization
	void LoadByName()
	{
		SceneManager.LoadScene("Water_Boat");
}
}
