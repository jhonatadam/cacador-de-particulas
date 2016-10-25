using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public GroundCheckController groundCheck;

	private Rigidbody2D rb2d;
	private SpriteRenderer sr;
	private Animator anim;

	private bool isFacingRight = true;
	
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		sr = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {

		if (groundCheck.isGrounded ()) {
			if (Input.GetKeyDown (KeyCode.W)) {
				rb2d.AddForce (new Vector2 (0, jumpForce));
				// saltando

			}
		}


	}

	void FixedUpdate() {
		float horizontalMoviment = Input.GetAxis ("Horizontal");

		UpdatePlayerVelocity (horizontalMoviment);
		UpdateSpriteDirection (horizontalMoviment);
		UpdateAnimator ();
	}

	private void UpdatePlayerVelocity (float horizontalMovement) {
		rb2d.velocity = new Vector2 (horizontalMovement * speed, rb2d.velocity.y);
	}

	private void UpdateSpriteDirection (float horizontalMovement) {
		if (horizontalMovement < 0.0f) {
			sr.flipX = true; 
		} else if (horizontalMovement > 0.0f) {
			sr.flipX = false;
		} 
	}

	private void UpdateAnimator () {
		if (groundCheck.isGrounded ()) {
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") &&
			    !anim.GetCurrentAnimatorStateInfo (0).IsName ("Stoped")) {
				anim.Play ("JumpDownStop");
			} else {
				if (rb2d.velocity.x != 0.0f) {
					anim.Play ("Walking");
				} else {
					anim.Play ("Stoped");
				}
			}

		} else {
			if (rb2d.velocity.y < 0.0f) {
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("JumpDown")) {
					anim.Play ("JumpUpJumpDown");
				}
			} else if (rb2d.velocity.y > 0.0f) {
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("JumpUp")) {
					anim.Play ("StopJumpUp");
				}
			} else {

			}
		}
	}
}
