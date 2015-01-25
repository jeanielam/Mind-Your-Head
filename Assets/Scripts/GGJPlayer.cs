using UnityEngine;
using System.Collections;

public class GGJPlayer : MonoBehaviour
{
	[HideInInspector]
	public bool facingRight = true;			// For determining which way the player is currently facing.
	[HideInInspector]
	public bool jump = false;				// Condition for whether the player should jump.
	
	public bool nextLevel = false;
	public float moveForce = 365f;			// Amount of force added to move the player left and right.
	public float maxSpeed = 5f;				// The fastest the player can travel in the x axis.
	public AudioClip[] jumpClips;			// Array of clips for when the player jumps.
	public float jumpForce = 10f;			// Amount of force added when the player jumps.
	private Transform groundCheck;			// A position marking where to check if the player is grounded.
	private Transform headCheck;
	public bool grounded = false;			// Whether or not the player is grounded.
	private Animator anim;					// Reference to the player's animator component.
	private bool headed = false;
	
	public AudioClip Footstep3;
	public AudioClip toad05;
	public AudioSource[] sounds;

	void OnCollisionEnter2D(Collision2D coll) {
		Debug.Log ("Amy hits: " + coll.gameObject.ToString ());
	}

	void Awake()
	{
		// Setting up references.
		groundCheck = transform.Find("groundCheck");
		headCheck = transform.Find("headCheck");
		anim = GetComponent<Animator>();
		// SOUNDs

		sounds = GetComponents<AudioSource>();
//
//		footsteps = GetComponent<AudioSource> (); //sounds [0];
//		owSound = GetComponent<AudioSource> ();
	}
	
	
	void Update()
	{
		// The player is grounded if a linecast to the groundcheck position hits anything on the ground layer.
	    //grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")); 
		headed = Physics2D.Linecast(transform.position, headCheck.position, 1 << LayerMask.NameToLayer("Ground")); 

		// If the jump button is pressed and the player is grounded then the player should jump.
		if((Input.GetButtonDown("Jump")) && grounded)
			jump = true;

		if (headed) {
			Debug.Log (Time.time.ToString());
			StartCoroutine(waitforsound());
			Debug.Log (Time.time.ToString());
			Debug.Log ("Dead!");
		}

	}

	IEnumerator waitforsound() {
		if (this.transform.rotation.z == 0) {
						this.transform.Rotate (0, 0, 90);
						audio.clip = toad05;
						audio.Play ();
						print ("toad play");
						Debug.Log ("w" + Time.time.ToString ());
						yield return new WaitForSeconds (2f);
						Debug.Log ("wait" + Time.time.ToString ());
						Application.LoadLevel ("Gameover");
				}
	}
	
	void FixedUpdate ()
	{
		// Cache the horizontal input.
		float h = Input.GetAxis("Horizontal");
		Debug.Log (h.ToString ());
		// The Speed animator parameter is set to the absolute value of the horizontal input.
		anim.SetFloat("Speed", Mathf.Abs(h));
		
		// If the player is changing direction (h has a different sign to velocity.x) or hasn't reached maxSpeed yet...
		if(h * rigidbody2D.velocity.x < maxSpeed)
			// ... add a force to the player.
			rigidbody2D.AddForce(Vector2.right * h * moveForce);
		
		// If the player's horizontal velocity is greater than the maxSpeed...
		if(Mathf.Abs(rigidbody2D.velocity.x) > maxSpeed)
			// ... set the player's velocity to the maxSpeed in the x axis.
			rigidbody2D.velocity = new Vector2(Mathf.Sign(rigidbody2D.velocity.x) * maxSpeed, rigidbody2D.velocity.y);
		// SOUND
		// !footsteps.isPlaying
		if (rigidbody2D.velocity != new Vector2 (0, 0) && !audio.isPlaying && PlayerPrefs.GetString ("Sound") == "True") {
			audio.clip = Footstep3;
			audio.Play ();
			Debug.Log ("footsteps playing");
		} else if (PlayerPrefs.GetString ("Sound") == "False")
		    audio.mute = true;

		if (rigidbody2D.velocity.x < 0.1 && rigidbody2D.velocity.x > -0.1)
						//footsteps.Stop ();

		// If the input is moving the player right and the player is facing left...
		if(h > 0 && !facingRight)
			// ... flip the player.
			Flip();
		// Otherwise if the input is moving the player left and the player is facing right...
		else if(h < 0 && facingRight)
			// ... flip the player.
			Flip();
		
		// If the player should jump...
		if(jump)
		{			
			// Add a vertical force to the player.
			rigidbody2D.AddForce(new Vector2(0f, jumpForce));
			
			// Make sure the player can't jump again until the jump conditions from Update are satisfied.
			jump = false;
			grounded = false;
		}
	}
	
	
	void Flip ()
	{
		// Switch the way the player is labelled as facing.
		facingRight = !facingRight;
		
		// Multiply the player's x local scale by -1.
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}
}