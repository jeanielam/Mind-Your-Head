using UnityEngine;
using System.Collections;

public class Cabinet : MonoBehaviour {

	int direction;
	int rotationVelocity;
	bool hasCollided;
	bool hasHitGround;
	// SOUND
	AudioSource hitGround;
	// Use this for initialization
	void Start () {
		if (GameObject.FindGameObjectWithTag ("GameManager") != null) {
			GetComponent<Rigidbody2D> ().mass *= 5*GameObject.FindGameObjectWithTag ("GameManager").GetComponentInChildren<GameManager> ().difficulty;
		}
		if (Random.value < 0.5)
			direction = -1;
		else
			direction = 1;
		rotationVelocity = Random.Range (5, 10);
		// SOUND
		hasCollided = false;
		hitGround = GetComponent<AudioSource> ();
		hasHitGround = false;
	}
	
	// Update is called once per frame
	void Update () {
		if (!hasCollided)
			this.transform.Rotate (0,0,direction*10*rotationVelocity*Time.deltaTime);
	}

	void OnCollisionEnter2D(Collision2D coll) {
		// SOUND
		Debug.Log (coll.gameObject.ToString());
		if (!hasHitGround && coll.gameObject.name == "brick" && !hitGround.isPlaying) {
			GameObject.Find ("Amy").GetComponent<AudioSource> ().Pause ();
			if (PlayerPrefs.GetString ("Sound") == "True")
					hitGround.Play ();
			else
				hitGround.mute = true;
		}
		if (coll.gameObject.name == "brick") {
			hasHitGround = true;
		}
		hasCollided = true;
	}
}
