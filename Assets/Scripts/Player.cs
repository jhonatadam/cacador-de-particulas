using UnityEngine;
using System;
using System.IO;
using System.Collections;



public class Player : MonoBehaviour {

	public float speed;
	public float jumpForce;

	public Vector3 previousPosition;

	public GroundCheck groundCheck;
	public PlayerAnimUpdater animUpdater;

	private Rigidbody2D rb2d;

	private bool updateOn = true;

	void Start () {
		rb2d = GetComponent<Rigidbody2D> ();
		previousPosition = transform.position;
	}

	void LateUpdate() {
		previousPosition = transform.position;
	}

	public void MoveHorizontally (float horizontalMovement) {
		if (updateOn) {
			rb2d.velocity = new Vector2 (horizontalMovement * speed, rb2d.velocity.y);
			animUpdater.UpdateAnim (gameObject);
		}	
	} 

	public void Jump () {
		if (updateOn && groundCheck.isGrounded ()) {
			rb2d.AddForce (new Vector2 (0, jumpForce));
		}
	}

	public void Dash () {
		
	}

	public void Fire () {
	
	}

	public Vector3 GetPreviousPositionDifference () {
		return transform.position - previousPosition;
	}
		
	public void SetUpdateOn(bool value) {
		updateOn = value;
		rb2d.velocity = new Vector2 (0, 0);
	}

	public bool GetUpdateOn() {
		return updateOn;
	}

}
