using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour {

	int score = 0;
	Text display;

	void Start () {
		display = GetComponent<Text>();
		display.text = "Score: " + score.ToString ();
	}

	void Awake () 
	{
		score = 0;
	}

	public void AddScore()
	{
		score += 10;
		display.text = "Score: " + score.ToString ();
	}

	public void ResetScore()
	{
		score = 0;
		display.text = "Score: " + score.ToString ();
	}

	void Update(){}
}
