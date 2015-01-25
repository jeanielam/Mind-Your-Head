using UnityEngine;
using System.Collections;

public class Score : MonoBehaviour {

	public static Score myScore;
	static int scoreVal = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<TextMesh> ().text = "Score: " + scoreVal.ToString();
	}

	public void addScore() {
		scoreVal += 10;

	}
}
