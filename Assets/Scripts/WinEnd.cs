using UnityEngine;
using System.Collections;

public class WinEnd : MonoBehaviour 
{
	bool nextlevel = false;
	// SOUND
	AudioSource doorCreak;

	void Start() {
		// SOUND
		doorCreak = GetComponent<AudioSource> ();
		if (PlayerPrefs.GetString ("Sound") == "True") {
						doorCreak.Play ();
			StartCoroutine(waitforsound());
				}
		else
			doorCreak.mute = true;
	}

	IEnumerator waitforsound() {
		yield return new WaitForSeconds(2);
		}
	void OnCollisionEnter2D(Collision2D coll) {
		if (coll.gameObject.tag == "Player") {
			Debug.Log ("Next Level");
			if (Application.loadedLevelName != "Tutorial")
					GameObject.FindGameObjectWithTag ("Score").GetComponentInChildren<ScoreUI> ().AddScore ();

			if (PlayerPrefs.GetString ("Sound") == "True") 
					doorCreak.Play ();
		} else
			doorCreak.mute = true;
		Application.LoadLevel ("Intermediate");
	}
}