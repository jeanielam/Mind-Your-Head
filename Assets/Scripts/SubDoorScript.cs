using UnityEngine;
using System.Collections;

public class SubDoorScript : MonoBehaviour 
{
	private bool playerIn;
	private float timeCount;
	private doorScript myParent;

	void Start ()
	{
		playerIn = false;
		timeCount = Time.time;
		myParent = transform.parent.GetComponent<doorScript> ();
	}

	void Update ()
	{
		if (Input.GetButtonDown("Jump") && (Time.time - timeCount > 0.3f) && playerIn)
		{
			timeCount = Time.time;
			myParent.isNudged = true;
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerIn = true;
			//Debug.Log("player in");
		}	
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerIn = false;
			//Debug.Log("player out");
		}	
	}
}
