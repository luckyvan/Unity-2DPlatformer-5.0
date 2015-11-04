using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {
	public float moveForce = 365f;
	public float maxSpeed = 5f;
	public float jumpForce = 1000f;

	private Transform groundCheck;
	private bool grounded = false;
	private Animator anim;

	public bool facingRight = true;

	public bool jump = false;
	// Use this for initialization
	void Start () {
		groundCheck = transform.Find ("groundCheck");
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Update () {
		grounded = Physics2D.Linecast (transform.position, groundCheck.position,LayerMask.GetMask ("Ground"));

		if (Input.GetButtonDown ("Jump")&&grounded) {
			jump = true;
		}
	}

	void Flip(){
		facingRight = !facingRight;
		Vector3 scale = transform.localScale;
		scale.x *= -1;
		transform.localScale = scale;
	}

	void FixedUpdate(){
		float h = Input.GetAxis ("Horizontal");
		anim.SetFloat ("Speed", Mathf.Abs (h));
		Rigidbody2D rb = GetComponent<Rigidbody2D>();
		// only exceed when force and velocity are same direction
		if (h*rb.velocity.x<maxSpeed) {
			rb.AddForce (Vector2.right * h * moveForce);
		}

		// restrict speed abs
		if (Mathf.Abs (rb.velocity.x)>maxSpeed) {
			rb.velocity = new Vector2(Mathf.Sign (rb.velocity.x)*maxSpeed, rb.velocity.y);
		}

		if ((h>0 && !facingRight) ||
		    (h<0 && facingRight)){
			Flip ();
		}

		if (jump) {
			anim.SetTrigger ("Jump");
			rb.AddForce (new Vector2(0, jumpForce));
			jump = false;
		}
	}
}
