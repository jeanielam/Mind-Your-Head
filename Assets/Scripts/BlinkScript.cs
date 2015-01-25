using UnityEngine;
using System.Collections;

public class BlinkScript : MonoBehaviour 
{
	public Sprite eyesOpen;
	public Sprite eyesClosed;

	private SpriteRenderer mySR;
	private float timeCount;
	private bool isOpen;
	
	void Start () 
	{
		mySR = GetComponent<SpriteRenderer> ();
		mySR.sprite = eyesOpen;
		timeCount = Time.time;
		isOpen = true;
	}

	void Update () 
	{
		if (isOpen && Time.time - timeCount > 2)
		{
			isOpen = false;
			mySR.sprite = eyesClosed;
			timeCount = Time.time;
		}
		else if (!isOpen && Time.time - timeCount > 0.2f)
		{
			isOpen = true;
			mySR.sprite = eyesOpen;
			timeCount = Time.time;
		}
	}
}
