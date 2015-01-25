using UnityEngine;
using System.Collections;

public class PlayerControllerScript : MonoBehaviour 
{
	public float maxSpeed = 10f;
	private bool facingRight = true;
	private bool handOut = false;

	private Animator ani;
	private AudioSource footsteps;
	private float timeCount;
	private ToolbarItem menu;

	void Start () 
	{
		ani = GetComponent<Animator> ();
		footsteps = GetComponent<AudioSource> ();
		timeCount = 0;
		menu = GameObject.Find ("menu").GetComponent<ToolbarItem> ();
	}

	void Update ()
	{
		if(Input.GetButtonDown("Jump") && Time.time - timeCount > 0.3f)
		{
			handOut = true;
			timeCount = Time.time;

			// View or eat item
			Bag b = menu.mainBag;
			int h = b.inHand;
			if (!b.inSpecialArea && !menu.viewing)
			{
				if (h >= 0 && h < b.inBag.Count)
				{
					if (b.inBag[h].name == "Food")
					{
						// add stuff later
						b.removeCollectable(b.inBag[h]);
						b.inHand = -1;
					}
					else
					{
						menu.viewing = true;
					}
				}
			}
			else if (menu.viewing)
			{
				menu.viewing = false;
			}
		}
	}

	void FixedUpdate () 
	{
		float move = Input.GetAxis ("Horizontal");

		ani.SetFloat("Speed", Mathf.Abs (move));

		if (Mathf.Abs (move) > 0.01f && !(footsteps.isPlaying))
		{
			footsteps.Play();
		}
		else if (Mathf.Abs (move) < 0.01f && footsteps.isPlaying)
		{
			footsteps.Stop();
		}


		if(handOut)
		{
			ani.SetTrigger("HandOut");
			handOut = false;
		}

		if (Time.time - timeCount < 0.5f)
			rigidbody2D.velocity = new Vector2 (0, rigidbody2D.velocity.y);
		else
			rigidbody2D.velocity = new Vector2 (move * maxSpeed, rigidbody2D.velocity.y);

		if (move > 0 && !facingRight) 
			Flip ();
		else if (move < 0 && facingRight)
			Flip ();
	}

	void Flip ()
	{
		facingRight = !facingRight;
		Vector3 tempScale = transform.localScale;
		tempScale.x *= -1;
		transform.localScale = tempScale;
	}
}
