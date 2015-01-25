using UnityEngine;
using System.Collections;

public class CabinetScript : MonoBehaviour 
{
	public Sprite open;
	public Sprite closed;

	private bool openFlag, playerIn, stuffAvailable;
	private float timeCount;
	private AudioSource[] sounds;
	private SpriteRenderer stuff;
	private ToolbarItem menu;

	void Start ()
	{
		GetComponent<SpriteRenderer> ().sprite = closed;
		stuff = transform.GetChild(0).GetComponent<SpriteRenderer>();
		menu = GameObject.Find ("menu").GetComponent<ToolbarItem> ();
		openFlag = false;
		playerIn = false;
		stuffAvailable = true;
		timeCount = Time.time;
		sounds = GetComponents<AudioSource> ();

		stuff.enabled = false;
	}

	void Update ()
	{
		if (Input.GetButtonDown("Jump") && (Time.time - timeCount > 0.3f) && playerIn)
		{
			timeCount = Time.time;
			if (!openFlag)
			{
				openFlag = true;
				GetComponent<SpriteRenderer> ().sprite = open;
				sounds[0].Play();
				if (stuffAvailable)
					stuff.enabled = true;
			}
			else if (stuffAvailable)
			{
				stuff.enabled = false;
				stuffAvailable = false;
				sounds[1].Play();
				menu.mainBag.addCollectable(new Collectable ("Key", 4, 0));
			}
			else
			{
				openFlag = false;
				GetComponent<SpriteRenderer> ().sprite = closed;
				sounds[0].Play();
			}
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerIn = true;
			menu.mainBag.inSpecialArea = true;
		}	
	}

	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerIn = false;
			menu.mainBag.inSpecialArea = false;
		}	
	}
}
