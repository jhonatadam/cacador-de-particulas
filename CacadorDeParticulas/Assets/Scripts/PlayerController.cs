using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float speed;
	public float jumpForce;

	private Vector3 lastPosition;

	public GroundCheckController groundCheck;
	public AnimUpdaterController animUpdater;

	private Rigidbody2D rb2d;

	private bool isFacingRight = true;

	private bool updateOn = true;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		lastPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (updateOn) {
			if (groundCheck.isGrounded ()) {
				if (Input.GetKeyDown (KeyCode.W)) {
					rb2d.AddForce (new Vector2 (0, jumpForce));
				}
			}		
		}
	}

	void FixedUpdate() {
		lastPosition = transform.position;

		if (updateOn) {
			float horizontalMoviment = Input.GetAxis ("Horizontal");
			UpdatePlayerVelocity (horizontalMoviment);
		}

		animUpdater.UpdateAnim (gameObject);
	}

	private void UpdatePlayerVelocity (float horizontalMovement) {
		rb2d.velocity = new Vector2 (horizontalMovement * speed, rb2d.velocity.y);
	}

	public Vector3 GetDelta () {
		return transform.position - lastPosition;
	}
		
	public void SetUpdateOn(bool value) {
		updateOn = value;
		rb2d.velocity = new Vector2 (0, 0);
	}

}
