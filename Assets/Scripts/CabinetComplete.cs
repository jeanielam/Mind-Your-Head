using UnityEngine;
using System.Collections;

public class CabinetComplete : MonoBehaviour 
{
	public bool currentLock; 
	public int currentColour, currentShape; 
	public string contentName;
	public int[] contentVals;

	// Colour order: Red, Green, Blue, Gold, Silver
	// Shape order: Square, Round
	public Sprite[] lockedSquare;
	public Sprite[] lockedRound;
	public Sprite unlocked;
	public Sprite open;

	private Sprite[,] locked = new Sprite[2,5];

	private bool isLocked, isOpen, playerIn, stuffAvailable;
	private float timeCount;
	private AudioSource[] sounds;
	private SpriteRenderer mySR, childSR;
	private ToolbarItem menu;
	
	void Start () 
	{
		mySR = GetComponent<SpriteRenderer> ();
		childSR = transform.GetChild(0).GetComponent<SpriteRenderer>();
		menu = GameObject.Find ("menu").GetComponent<ToolbarItem> ();
		sounds = GetComponents<AudioSource> ();

		isOpen = false; isLocked = currentLock; playerIn = false; stuffAvailable = true;
		timeCount = Time.time;
		childSR.enabled = false;

		for (int i = 0; i < lockedSquare.Length; i++)
		{
			locked[0,i] = lockedSquare[i];
			locked[1,i] = lockedRound[i];
		}

		if (!currentLock) 
			mySR.sprite = unlocked;
		else
			mySR.sprite = locked[currentShape,currentColour];
	}

	void Update ()
	{
		if (Input.GetButtonDown("Jump") && (Time.time - timeCount > 0.3f) && playerIn)
		{
			timeCount = Time.time;
			int inHand = menu.mainBag.inHand;
			if (isLocked && inHand >= 0 && inHand < menu.mainBag.inBag.Count)
			{
				if (menu.mainBag.inBag[inHand].colour == currentColour &&
				    menu.mainBag.inBag[inHand].shape == currentShape)
				{
					isLocked = false;
					mySR.sprite = unlocked;
					menu.mainBag.removeCollectable(menu.mainBag.inBag[inHand]);
					menu.mainBag.inHand = -1;
					sounds[0].Play();
				}
			}
			else if (!isLocked)
			{
				if (!isOpen)
				{
					isOpen = true;
					mySR.sprite = open;
					sounds[1].Play();
					if (stuffAvailable)
						childSR.enabled = true;
				}
				else if (stuffAvailable)
				{
					childSR.enabled = false;
					stuffAvailable = false;
					if (contentVals.Length == 1)
						menu.mainBag.addCollectable(new Collectable (contentName, contentVals[0]));
					else if (contentVals.Length == 2)
					{
						menu.mainBag.addCollectable(new Collectable (contentName, contentVals[0], contentVals[1]));
						sounds[2].Play();
					}
				}
				else
				{
					isOpen = false;
					mySR.sprite = unlocked;
					sounds[1].Play();
				}
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
