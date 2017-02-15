using UnityEngine;
using System;
using System.IO;
using System.Collections;



public class Player : MonoBehaviour {

	public float speed;
	public float jumpForce;
	public float dashTime;
	public float dashSpeed;

	private bool dashing;
	private float dashEnlapsedTime = 0.0f;

	private Animator animator;
	private SpriteRenderer sr;
	private Rigidbody2D rb2d;

	public Vector3 previousPosition;

	public GroundCheck groundCheck;

	private bool updateOn = true;

	void Start () {
		animator = GetComponent <Animator> ();
		sr = GetComponent <SpriteRenderer> ();
		rb2d = GetComponent<Rigidbody2D> ();

		previousPosition = transform.position;
	}

	void Update () {
		
		if (dashing) {
			animator.SetFloat ("playerXVelocity", Mathf.Abs(rb2d.velocity.x));
			dashEnlapsedTime += Time.deltaTime;

			if (dashEnlapsedTime >= dashTime) {
				dashing = false;
				updateOn = true;
				dashEnlapsedTime = 0.0f;
			}
		}

	}

	void LateUpdate() {
		animator.SetFloat ("horizontalMovement", Mathf.Abs(rb2d.velocity.x));
		animator.SetFloat ("verticalMovement", rb2d.velocity.y);

		previousPosition = transform.position;
	}

	public void MoveHorizontally (float horizontalMovement) {
		if (updateOn) {
			// atualizando velocidade
			rb2d.velocity = new Vector2 (horizontalMovement * speed, rb2d.velocity.y);

			// atualizando animator
			UpdateSpriteDirection (horizontalMovement);
		}	
	} 

	public void Jump () {
		if (updateOn && groundCheck.isGrounded ()) {
			// atualizando velocidade
			rb2d.AddForce (new Vector2 (0, jumpForce));

			// atualizando animator
			animator.SetBool("jump", true);
		}
	}

	public void Dash () {
		if (!dashing) {
			dashing = true;
			updateOn = false;
			rb2d.AddForce (new Vector2 ((sr.flipX ? -dashSpeed : dashSpeed) , 0.0f));
			//rb2d.velocity = new Vector2 ((sr.flipX ? -dashSpeed : dashSpeed) , 0.0f);
		}
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

	private void UpdateSpriteDirection (float horizontalMovement) {
		if (horizontalMovement < 0.0f) {
			sr.flipX = true; 
		} else if (horizontalMovement > 0.0f) {
			sr.flipX = false;
		} 
	}
}
