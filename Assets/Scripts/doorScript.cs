using UnityEngine;
using System.Collections;

public class doorScript : MonoBehaviour 
{
	public GameObject player;

	public Sprite[] doorStates; // 2 locks, left lock, right lock, unlocked
	// Colour order: Red, Green, Blue, Gold, Silver
	// Shape order: Square, Round
	public Sprite[] squareBells, roundBells;
	public bool isLockedL, isLockedR;
	public int bellColourL, bellShapeL, bellColourR, bellShapeR;

	public bool isNudged;

	private bool playerIn, isOpen; 
	private SpriteRenderer mySR, lBellSR, rBellSR;
	private GameObject lDoorGO, rDoorGO;
	private ToolbarItem menu;
	private float timeCount;
	private bool playerLeft; // Is player left of door
	private AudioSource[] sounds;

	void Start ()
	{
		isOpen = false; playerIn = false; isNudged = false;
		mySR = GetComponent<SpriteRenderer> ();
		lBellSR = transform.Find ("leftBell").GetComponent<SpriteRenderer> ();
		rBellSR = transform.Find ("rightBell").GetComponent<SpriteRenderer> ();
		lDoorGO = transform.Find ("doorOpenLeft").gameObject;
		rDoorGO = transform.Find ("doorOpenRight").gameObject;
		sounds = GetComponents<AudioSource> ();
		menu = GameObject.Find ("menu").GetComponent<ToolbarItem> ();
		timeCount = Time.time;
		CheckDoorState (true);
	}

	void Update ()
	{
		if (Input.GetButtonDown("Jump") && (Time.time - timeCount > 0.3f) && playerIn)
		{
			timeCount = Time.time;
			if (player.transform.position.x < this.transform.position.x) 
				playerLeft = true;
			else playerLeft = false;
			int inHand = menu.mainBag.inHand;

			// Something in hand
			if (inHand >= 0 && inHand < menu.mainBag.inBag.Count)
			{
				// Left lock
				if (isLockedL && playerLeft)
				{
					if (menu.mainBag.inBag[inHand].colour == bellColourL &&
					    menu.mainBag.inBag[inHand].shape == bellShapeL)
					{
						isLockedL = false;
						CheckDoorState (false);
						menu.mainBag.removeCollectable(menu.mainBag.inBag[inHand]);
						menu.mainBag.inHand = -1;
						sounds[0].Play ();
						return;
					}
				}

				// Right Lock
				if (isLockedR && !playerLeft)
				{
					if (menu.mainBag.inBag[inHand].colour == bellColourR &&
					    menu.mainBag.inBag[inHand].shape == bellShapeR)
					{
						isLockedR = false;
						CheckDoorState (false);
						menu.mainBag.removeCollectable(menu.mainBag.inBag[inHand]);
						menu.mainBag.inHand = -1;
						sounds[0].Play ();
						return;
					}
				}
			}

			// Door Not Locked
			if (!isLockedL && !isLockedR && !isOpen)
			{
				mySR.enabled = false;
				GetComponent<Collider2D>().enabled = false;
				isOpen = true;
				sounds[1].Play ();
				playerIn = false;
				if (playerLeft)
					rDoorGO.SetActive(true);
				else
					lDoorGO.SetActive(true);
				return;
			}

			// Trying to open door with no key
			if (!isOpen) sounds[2].Play ();
		}

		// Door Open
		if (isOpen && isNudged)
		{
			isOpen = false;
			isNudged = false;
			mySR.enabled = true;
			mySR.sprite = doorStates [3];
			GetComponent<Collider2D>().enabled = true;
			lDoorGO.SetActive(false);
			rDoorGO.SetActive(false);
			sounds[1].Play ();
			playerIn = false;
		}
	}

	void OnCollisionEnter2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerIn = true;
			menu.mainBag.inSpecialArea = true;
		}	
	}
	
	void OnCollisionExit2D (Collision2D other)
	{
		if (other.gameObject.tag == "Player")
		{
			playerIn = false;
			menu.mainBag.inSpecialArea = false;
		}	
	}
	
	void CheckDoorState (bool ini)
	{
		if (ini) // Door not open
		{
			lDoorGO.SetActive(false);
			rDoorGO.SetActive(false);
			lBellSR.enabled = true;
			rBellSR.enabled = true;
		}

		if (isLockedL && isLockedR)
		{
			mySR.sprite = doorStates [0];
			if (ini)
			{
				CheckBellLeft();
				CheckBellRight();
			}
		}
		else if (isLockedL)
		{
			mySR.sprite = doorStates [1];
			rBellSR.enabled = false;
			if (ini) CheckBellLeft();
		}
		else if (isLockedR)
		{
			mySR.sprite = doorStates [2];
			lBellSR.enabled = false;
			if (ini) CheckBellRight();
		}
		else if (!isOpen)
		{
			rBellSR.enabled = false;
			lBellSR.enabled = false;
			mySR.sprite = doorStates [3];
		}
	}
	
	void CheckBellLeft ()
	{
		if (bellShapeL == 0) // Square
		{
			lBellSR.sprite = squareBells[bellColourL];
		}
		if (bellShapeL == 1)// Round
		{
			lBellSR.sprite = roundBells[bellColourL];
		}
	}
	
	void CheckBellRight ()
	{
		if (bellShapeR == 0) // Square
		{
			rBellSR.sprite = squareBells[bellColourR];
		}
		if (bellShapeR == 1)// Round
		{
			rBellSR.sprite = roundBells[bellColourR];
		}
	}
}
