using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiBehavior : EnemyBehavior {
	// Coordenada (em x) máxima (right) e 
	// mínima (left) que o patrulheiro caminha.
	// durante sua patrulha.
	public float rightLimit;
	public float leftLimit;
	public float turningDelay = 1;
	private float lastTurning = 0;
	// Arma corpo a corpo
	private SamuraiMeleeWeapon weapon;

	// Referência do animator
	private Animator animator;
	private bool sawPlayer;

	void Start () {
		base.Start ();
		weapon = GetComponent <SamuraiMeleeWeapon> ();
		animator = GetComponentInChildren <Animator> ();
		print (animator);
	}

	void Update () {
		// se o samurai estiver atacando, o seu 
		// comportamento não é atualizado
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("WeakAttack") || 
			animator.GetCurrentAnimatorStateInfo (0).IsName ("StrongAttack")) {
			return;
		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Walk")) {
			// Andar
			Move ();
		} else {
			Stop ();
		}
		Act ();
	}
	void LateUpdate () {
		animator.SetBool ("isSeeingThePlayer", isSeeingThePlayer);
		animator.SetBool ("sawPlayer", sawPlayer);
	}

	public override void Patrol () {
		// Atualizando orientação
		if (Time.time - lastTurning > turningDelay) {
			UpdateGuidancePatrol ();
			lastTurning = Time.time;
		}
			

	}
	public void UpdateGuidancePatrol () {
		
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
	public override void Attack ()
	{
		if (Time.time - lastTurning > turningDelay) {
			UpdateGuidanceFollowPlayer ();
			lastTurning = Time.time;
		}
		float playerDistance = Vector3.Distance (transform.position, player.transform.position);

		if (weapon.range < playerDistance) {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("WalkAngry")) {
				Move ();
			}
		} else {
			Stop ();
		}

		if (!EnemyInCamera () && player.isActiveAndEnabled)
			sawPlayer = false;
	}

	public void UpdateGuidanceFollowPlayer () {
		if (isFacingRight) {
			if (transform.position.x > player.transform.position.x) {
				isFacingRight = false;
				transform.rotation = new Quaternion (0, 180, 0, 0);
			}
		} else {
			if (transform.position.x < player.transform.position.x) {
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
	public override bool Look ()
	{
		bool ret = (EnemyInCamera () && IsFacingThePlayer () && player.isActiveAndEnabled);
		sawPlayer = ret ? ret : sawPlayer;
		return sawPlayer;
	}
}
