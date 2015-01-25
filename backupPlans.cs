[System.Serializable]
public class CollectableData
{
	// Names of items
	public string[] names = 
	{
		/* Food x5 */
		"Fruit", "Milk and Cookies", "Sandwich", "Boxed Lunch", "Picnic Basket", 
		/* Books x5 */
		"A Burglar's Guide", "Introduction to Spellcraft", "Physical Magic", "Advanced Burglary", "Black Magic", 
		/* Jars x5 */
		"Cold Daisy", "Wet Seed", "Vines", "Feathers", "Gel Fruit",
		/* Opened Jars x5 */
		"Bullets", "Shield", "Mask", "Gas Bomb", "Wings",
		/* Tools x15 */
		"Gold Key", "Silver Key", "Blue Key", "Green Key", "Black Key",
		"Red Key", "Purple Key", "Transit Ticket", "Star Ticket", "Black Cat Ticket", 
		"Water-Marked Ticket", "Skull Ticket", "Family Photo", "Map", "Prophecy"
	}; 

	// Descriptions of items
	public string[] details = 
	{
		/* Fruit */ "Adds 5% of Strength.",
		/* Milk and Cookies */ "Add 10% of Strength.",
		/* Sandwich */ "Adds 15% of Strength.",
		/* Boxed Lunch */ "Adds 20% of Strength.",
		/* Picnic Basket */ "Adds 25% of Strength.",
		/* A Burglar's Guide */ "xxx",
		/* Introduction to Spellcraft */ "xxx",
		/* Physical Magic */ "xxx",
		/* Advanced Burglary */ "xxx",
		/* Black Magic */ "xxx",
		/* Cold Daisy */ "xxx",
		/* Wet Seed */ "xxx",
		/* Vines */ "xxx",
		/* Feathers */ "xxx",
		/* Gel Fruit */ "xxx",
		/* Bullets */ "xxx",
		/* Shield */ "xxx",
		/* Mask */ "xxx",
		/* Gas Bomb */ "xxx",
		/* Wings */ "xxx",
		/* Gold Key */ "xxx",
		/* Silver Key */ "xxx",
		/* Blue Key */ "xxx",
		/* Green Key */ "xxx",
		/* Black Key */ "xxx",
		/* Red Key */ "xxx",
		/* Purple Key */ "xxx",
		/* Transit Ticket */ "xxx",
		/* Star Ticket */ "xxx",
		/* Black Cat Ticket */ "xxx",
		/* Water-Marked Ticket */ "xxx",
		/* Skull Ticket */ "xxx",
		/* Family Photo */ "xxx",
		/* Map */ "xxx",
		/* Prophecy */ "xxx"
	};

	public Sprite[] pictures;

	public string getDetail (string name)
	{
		if (names.Length != details.Length)
		{
			Debug.LogError("Names don't match details");
			return "error";
		}

		for (int i = 0; i < names.Length; i++)
		{
			if (names[i] == name)
				return details[i];
		}

		Debug.LogError("Name not found");
		return "error";
	}
}