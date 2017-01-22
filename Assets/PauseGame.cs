using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseGame : MonoBehaviour {
	protected bool paused;
	// Use this for initialization
	public bool CheckIfPaused () {
		return paused;
	}

    void OnLevelWasLoaded() {
        OnResumeGame();
    }

	public void OnPauseGame () {
		paused = true;
		Time.timeScale = 0;
	}

	// Update is called once per frame
	public void OnResumeGame () {
		paused = false;
		Time.timeScale = 1;

	}
}
