using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public GroundCheckController groundCheck;
	public AnimUpdaterController animUpdater;

	private Rigidbody2D rb2d;

	private bool isFacingRight = true;
	
	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (groundCheck.isGrounded ()) {
			if (Input.GetKeyDown (KeyCode.W)) {
				rb2d.AddForce (new Vector2 (0, jumpForce));
			}
		}
	}

	void FixedUpdate() {
		float horizontalMoviment = Input.GetAxis ("Horizontal");
		UpdatePlayerVelocity (horizontalMoviment);
		animUpdater.UpdateAnim (gameObject);
	}

	private void UpdatePlayerVelocity (float horizontalMovement) {
		rb2d.velocity = new Vector2 (horizontalMovement * speed, rb2d.velocity.y);
	}
		
}
