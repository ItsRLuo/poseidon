using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreLabel : MonoBehaviour {
	public string Boatname;

	void Start () {
		int score = PlayerPrefs.GetInt (Boatname, 0);
		gameObject.GetComponent<Text> ().text = score.ToString();

	}

}
