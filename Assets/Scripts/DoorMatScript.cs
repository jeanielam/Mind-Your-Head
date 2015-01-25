using UnityEngine;
using System.Collections;

public class DoorMatScript : MonoBehaviour 
{
	public string longname;

	private bool playerIn;
	private ToolbarItem menu;
	private AudioSource unlock;
	private float exitTime = -1f;

	void Start ()
	{
		menu = GameObject.Find ("menu").GetComponent<ToolbarItem> ();
		unlock = GetComponent<AudioSource> ();
	}

	void Update ()
	{
		if (Input.GetButtonDown("Jump") && playerIn && 
		    menu.mainBag.inHand >= 0 && menu.mainBag.inHand < menu.mainBag.inBag.Count)
		{
			if (menu.mainBag.inBag[menu.mainBag.inHand].longName == longname)
			{
				menu.mainBag.removeCollectable(menu.mainBag.inBag[menu.mainBag.inHand]);
				menu.mainBag.inHand = -1;
				unlock.Play();
				exitTime = Time.time;
			}
		}

		if (exitTime > 0 && Time.time - exitTime > 0.5)
		{
			menu.mainBag.inSpecialArea = false;
			Application.LoadLevel("level1");
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
