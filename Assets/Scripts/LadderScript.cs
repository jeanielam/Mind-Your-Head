using UnityEngine;
using System.Collections;

public class LadderScript : MonoBehaviour 
{
	public GameObject player;
	public float height = 8.4f;
	private bool playerIn = false;
	private float y;

	void FixedUpdate ()
	{
		float move = Input.GetAxis ("Vertical");
		if (move > 0 && playerIn && player.transform.position.y < this.transform.position.y)
		{
			y = player.transform.position.y + height;
			player.transform.position = new Vector2(player.transform.position.x, y);
		}
		else if (move < 0 && playerIn && player.transform.position.y > this.transform.position.y)
		{
			y = player.transform.position.y - height;
			player.transform.position = new Vector2(player.transform.position.x, y);
		}
	}

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerIn = true;
		}	
	}
	
	void OnTriggerExit2D (Collider2D other)
	{
		if (other.tag == "Player")
		{
			playerIn = false;
		}	
	}
}
