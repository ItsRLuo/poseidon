using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadMainMenu : MonoBehaviour {

	// Use this for initialization
	public void LoadMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
