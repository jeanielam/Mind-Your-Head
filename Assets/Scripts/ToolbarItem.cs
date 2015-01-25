using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Collectable
{
	public string name, longName;
	public string detail;
	public int count, colour, shape, value;

	// Unique items
	public Collectable (string newName)
		: this (newName, -1, -1, -1, newName) {}

	// Food
	public Collectable (string newName, int newValue)
		: this ("Food", -1, -1, newValue, newName) {}

	// Same items with different shapes and colours
	public Collectable (string newName, int newColour, int newShape)
		: this (newName, newColour, newShape, -1, "") {}

	// All
	public Collectable (string newName, int newColour, int newShape, int newValue, string newLongName)
	{
		CollectableData d = new CollectableData ();
		name = newName;
		count = 1; colour = newColour; shape = newShape; value = newValue;
		if (colour >= 0 && 
		    shape >= 0 &&
		    colour < d.colours.Length && 
		    shape < d.shapes.Length) 
			longName = d.shapes[shape] + " " + 
					   d.colours[colour] + " " + 
					   name;
		else longName = newLongName;
		detail = d.getDetail (newName);
	}
}

public class Bag
{
	public List<Collectable> inBag; // List of items in bag
	public int inHand; // The item held in hand by player. An index of inBag
	public bool inSpecialArea; // Is player in a special area? False for viewing or eating item

	public Bag ()
	{
		inBag = new List<Collectable> ();	
		inHand = -1;
		inSpecialArea = false;
	}

	// Add Collectable to bag
	public void addCollectable (Collectable a)
	{
		Collectable found = inBag.Find (x => x.longName == a.longName);
		if (found == null) inBag.Add(a);
		else found.count++;
	}

	// Remove Collectable from bag
	public void removeCollectable (Collectable a)
	{
		Collectable found = inBag.Find (x => x.longName == a.longName);
		if (found != null)
		{
			if (found.count <= 0)
				inBag.RemoveAll (x => x.longName == a.longName);
		}
	}
}

[System.Serializable]
public class CollectableData
{
	public string[] colours = {"Red", "Green", "Blue", "Gold", "Silver"};
	public string[] shapes = {"Square", "Round", "Diamond", "Heart", "Crescent"};
	public string[] longNames;
	public Texture2D[] pictures;

	public string getDetail (string name)
	{
		if (name == "Food") 
			return "Eating food gives you strength. " +
				"Hold it in your hand to eat it or to give it to others.";
		else if (name == "Key") 
			return "This key unlocks a door, a cabinet or a box. " +
				"Its colour and shape are important. " +
				"Hold it in your hand to use.";
		else if (name == "Family Photo")
			return "<i><b>'Me and Mama and Papa and Sam in front of our house.'</b></i>\n" +
				"                         - scribbled on the back";
		else 
			return "xxx";
	}

	public Texture2D getSpriteWithLongName (string lname)
	{
		if (longNames.Length == pictures.Length)
		{
			for (int i = 0; i < pictures.Length; i++)
			{
				if (longNames[i] == lname)
					return pictures[i];
			}
		}
		return null;
	}
}

public class ToolbarItem : MonoBehaviour 
{
	
	public Bag mainBag;

	public CollectableData data;
	public Texture2D Up, Down, Exit, HandOpen, HandClosed, Eye, Mouth;

	public GUIStyle bagButtonStyle;
	public GUIStyle wandButtonStyle;
	public GUIStyle popBoxStyle;
	public GUIStyle itemBoxStyle;
	public GUIStyle itemBigStyle;
	public GUIStyle textOnlyStyle;
	public GUIStyle imageOnlyStyle;
	public GUIStyle vicStyle;

	private bool popBoxOn, downOn, upOn; 
	private int currentPage, pages, selectedItem;
	private int slotsInPage = 8; // Number of slots in a full page
	private int identifier = -1; // Bag index for item using
	
	public bool viewing = false;

	void Awake()
	{
		mainBag = new Bag();
		mainBag.addCollectable(new Collectable ("Family Photo"));

		popBoxOn = false; currentPage = 1; selectedItem = -1;

		DontDestroyOnLoad(this.gameObject);
	}

	void OnGUI() 
	{
		// Bag Button
		if (GUI.Button (new Rect (10, 515, 100, 100), "", bagButtonStyle) && !popBoxOn) 
		{
			popBoxOn = true;
		}

		// Stuff in hand
		if (mainBag.inHand >= 0)
		{
			GUI.Label (new Rect (800, 515, 160, 80), 
			           data.getSpriteWithLongName(mainBag.inBag[identifier].longName), 
			           imageOnlyStyle);
		}

		// Viewing Item
		if (viewing && mainBag.inHand >= 0 && mainBag.inHand < mainBag.inBag.Count)
		{
			Texture2D vp = data.getSpriteWithLongName(mainBag.inBag[mainBag.inHand].longName);
			GUI.Label (new Rect (160, 50, 640, 480), vp, imageOnlyStyle);
		}

		// For Pop Box to Open
		if (popBoxOn) 
		{
			int num = mainBag.inBag.Count;
			pages = num/slotsInPage + 1;

			if (currentPage > 1) upOn = true;
			else upOn = false;

			if (currentPage < pages) downOn = true;
			else downOn = false;

			// Main Box
			GUI.Label (new Rect (160, 50, 640, 480), "", popBoxStyle);

			string itemName;
			string content = ""; Texture2D contentPic = new Texture2D (10, 10);
			bool somethingSelected = false;
			for (int i = 0; i < slotsInPage; i++)
			{
				int index = i + (currentPage-1)*slotsInPage;
				if (index >= num) break;

				GUIStyle temp;
				if (selectedItem == index) 
				{
					temp = itemBigStyle;
					content = mainBag.inBag[index].detail;
					contentPic = data.getSpriteWithLongName(mainBag.inBag[index].longName);
					somethingSelected = true;
					if (mainBag.inHand < 0) identifier = index;
				}
				else 
				{
					temp = itemBoxStyle;
				}
				//GUI.Label (new Rect (456, 60, 334, 460), itemName, itemBigStyle); // Big Box

				itemName = mainBag.inBag[index].longName;
				if (GUI.Button (new Rect (170, 60 + i*50, 276, 45), itemName, temp))
				{
					selectedItem = index;
				}
			}

			if (somethingSelected)
			{
				// Big Box
				GUI.Label (new Rect (456, 60, 334, 460), "", itemBigStyle);
				GUI.Label (new Rect (456, 405, 334, 100), content, textOnlyStyle); // Description
				GUI.Label (new Rect (456, 60, 334, 264), contentPic, imageOnlyStyle); // Picture

				if (mainBag.inHand < 0)
				{
					if (GUI.Button (new Rect (548, 333, 87, 59), HandOpen, "label"))
					{
						mainBag.inHand = identifier;
						mainBag.inBag[identifier].count--;
					}
				}
				else
				{
					if (GUI.Button (new Rect (548, 333, 87, 59), HandClosed, "label"))
					{
						mainBag.inHand = -1;
						mainBag.inBag[identifier].count++;
					}
				}
				GUI.Label (new Rect (635, 338, 50, 50), "x" + mainBag.inBag[selectedItem].count, itemBigStyle);
			}
			else GUI.Label (new Rect (473, 65, 300, 450), "", vicStyle); // Vic Box

			if (upOn) 
			{
				if (GUI.Button (new Rect (360, 465, 60, 60), Up, "label"))
					currentPage--;
			}

			if (downOn)
			{
				if (GUI.Button (new Rect (190, 465, 60, 60), Down, "label"))
					currentPage++;
			}

			if (GUI.Button (new Rect (260, 470, 100, 47), Exit, "label"))
				popBoxOn = false;
		}
	}
}
