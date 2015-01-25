using UnityEngine;
using System.Collections;

public class GGJRestart : MonoBehaviour {

	// SOUND
	// Update is called once per frame
	void Update () {
		GameObject.FindGameObjectWithTag("Score").GetComponentInChildren<ScoreUI>().ResetScore();
		if(Input.GetKeyDown(KeyCode.R)){
			Application.LoadLevel("Main");
		} else if(Input.GetKeyDown(KeyCode.Home)){
			Application.LoadLevel("Start");
		}
	}
}
