using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulheiroBehavior : EnemyBehavior {

	// Coordenada (em x) máxima (right) e 
	// mínima (left) que o patrulheiro caminha.
	// durante sua patrulha.
	public float rightLimit;
	public float leftLimit;

	// Referência do animator
	public Animator animator;

	void Start () {
		base.Start ();
		animator = GetComponent <Animator> ();
	}

	// Update is called once per frame.
	void Update () {
		Act ();
	}

	void LateUpdate () {
		animator.SetBool ("IsSeeingThePlayer", isSeeingThePlayer);
	}

	public override void Patrol () {
		// Atualizando orientação
		UpdateGuidance ();

		// so anda se estiver em modo patrulha na animação
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Patrol")) {
			// Andar
			Move ();		
		}

	}

	public override void Attack () {
		Stop (); 
	}

	public override bool Look ()
	{
		//return (rend.isVisible && IsFacingThePlayer () && player.isActiveAndEnabled);
		return (EnemyInCamera() && IsFacingThePlayer () && player.isActiveAndEnabled);
	}

	public void UpdateGuidance () {
		if (isFacingRight) {
			if (transform.position.x >= rightLimit) {
				isFacingRight = false;
				transform.rotation = new Quaternion (0, 180, 0, 0);
			}
		} else {
			if (transform.position.x <= leftLimit) {
				isFacingRight = true;
				transform.rotation = new Quaternion (0, 0, 0, 0);
			}
		}
	}

	public void Move () {
		// Determinando velocidade.
		float playerSpeed = (player != null ? player.speed :  1); 
		float speed = (isFacingRight ? moveSpeed * playerSpeed : -moveSpeed * playerSpeed);

		// Atualizando velocidade.
		rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
	}

	public bool IsFacingThePlayer () {
		if (isFacingRight) {
			return transform.position.x < player.transform.position.x;
		} else {
			return transform.position.x > player.transform.position.x;
		}
	}


}
