using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Menu : MonoBehaviour {

	// SOUND
	// Use this for initialization
	void Start () {
		PlayerPrefs.SetString ("Sound", "True");
		PlayerPrefs.SetString ("Subtitles", "True");
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyUp ("m")) {
			Debug.Log ("m");
			if (PlayerPrefs.GetString ("Sound") == "False") {
				PlayerPrefs.SetString ("Sound", "True");
				foreach (Transform child in GameObject.Find("GOCanvas").transform) {
					if (child.gameObject.name == "Sound") {
						Debug.Log ("found it");
						child.gameObject.GetComponent<Text> ().text = "Press m to turn sound OFF";
						child.gameObject.GetComponent<Text> ().color = Color.red;
					}
				}
			} else {
				PlayerPrefs.SetString ("Sound", "False");
				foreach (Transform child in GameObject.Find("GOCanvas").transform) {
					if (child.gameObject.name == "Sound") {
						Debug.Log ("found it");
						child.gameObject.GetComponent<Text> ().text = "Press m to turn sound ON";
						child.gameObject.GetComponent<Text> ().color = Color.green;
					}
				}
			}
		}
		if (Input.GetKeyUp ("s")) {
			if (PlayerPrefs.GetString ("Subtitles") == "False") {
				PlayerPrefs.SetString ("Subtitles", "True");
				foreach (Transform child in GameObject.Find("GOCanvas").transform) {
					if (child.gameObject.name == "Subtitles") {
						Debug.Log ("found it");
						child.gameObject.GetComponent<Text> ().text = "Press s to turn subtitles OFF";
						child.gameObject.GetComponent<Text> ().color = Color.red;
					}
				}
			} else {
				PlayerPrefs.SetString ("Subtitles", "False");
				foreach (Transform child in GameObject.Find("GOCanvas").transform) {
					if (child.gameObject.name == "Subtitles") {
						Debug.Log ("found it");
						child.gameObject.GetComponent<Text> ().text = "Press s to turn subtitles ON";
						child.gameObject.GetComponent<Text> ().color = Color.green;
					}
				}
			}
		}
		if (Input.GetKeyUp (KeyCode.Return)) {
			Application.LoadLevel ("Tutorial");
		}
	}
}
