﻿using UnityEngine;
using System.Collections;

public class PlayerAnimUpdater : MonoBehaviour {

	public GameObject player;
	public GroundCheck groundCheck;

	public void UpdateAnim(GameObject obj) {
		float horizontalMoviment = Input.GetAxis ("Horizontal");

		UpdateSpriteDirection (horizontalMoviment, obj);
		UpdateAnimator (obj);
	}

	private void UpdateSpriteDirection (float horizontalMovement, GameObject obj) {
		SpriteRenderer sr = obj.GetComponent<SpriteRenderer> ();

		if (obj.tag == player.tag) {	
			if (horizontalMovement < 0.0f) {
				sr.flipX = true; 
			} else if (horizontalMovement > 0.0f) {
				sr.flipX = false;
			} 
		} else {	
			sr.flipX = player.GetComponent<SpriteRenderer> ().flipX;
		}
	}

	private void UpdateAnimator (GameObject obj) {
		Animator anim = obj.GetComponent <Animator> ();
		Rigidbody2D playerRb2d = player.GetComponent <Rigidbody2D> ();

		if (groundCheck.isGrounded ()) {
			
			if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("Walking") &&
				!anim.GetCurrentAnimatorStateInfo (0).IsName ("Stoped")) {
				print ("aqui");
				anim.Play ("JumpDownStop");
			} else {
				if (playerRb2d.velocity.x != 0.0f) {
					anim.Play ("Walking");
				} else {

					anim.Play ("Stoped");
				}
			}

		} else {
			if (playerRb2d.velocity.y < 0.0f) {
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("JumpDown")) {
					anim.Play ("JumpUpJumpDown");
				}
			} else if (playerRb2d.velocity.y > 0.0f) {
				if (!anim.GetCurrentAnimatorStateInfo (0).IsName ("JumpUp")) {
					anim.Play ("StopJumpUp");
				}
			} else {

			}
		}
	}
}