using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparadorBehavior : EnemyBehavior {
	// Coordenada (em x) máxima (right) e 
	// mínima (left) que o patrulheiro caminha.
	// durante sua patrulha.
	public float rightLimit;
	public float leftLimit;
	public float turningDelay = 1;
	private float lastTurning = 0;
	private GameObject Atratores;
	// Arma corpo a corpo
	private ReparadorMeleeWeapon weapon;

	// Referência do animator
	private Animator animator;
	private bool sawPlayer;
	private GameObject reparando;
	private bool lookingForAtrator = true;
	void Start () {
		Atratores = GameObject.Find ("Atratores");
		base.Start ();
		weapon = GetComponent <ReparadorMeleeWeapon> ();
		animator = GetComponentInChildren <Animator> ();
		print (animator);
	}

	void Update () {
		// se o samurai estiver atacando, o seu 
		// comportamento não é atualizado
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack1") || 
			animator.GetCurrentAnimatorStateInfo (0).IsName ("Attack2")) {
			return;
		}
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Voando")) {
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
		
	public void StopLookingForAtrator(GameObject atrator){
		lookingForAtrator = false;
		reparando = atrator;
	}
	public void StartLookingForAtrator(){
		lookingForAtrator = true;
	}

	public void UpdateGuidancePatrol () {
		if (lookingForAtrator) { //se está procurando por atrator
			int followAtrator = 0;
			bool foundAtrator = false;
			int nearest = 0;
			for (int i = 0; i <	Atratores.transform.childCount; i++) {
				GameObject atrator = Atratores.transform.GetChild (i).gameObject;
				if (distanceTo (atrator) < distanceTo (Atratores.transform.GetChild (nearest).gameObject )) {
					if ((!atrator.GetComponent<Atrator> ().IsFull ()) && (!atrator.GetComponent<Atrator> ().IsDone ())) {
						foundAtrator = true;
						i = nearest;
					}
				}
			}
			if (foundAtrator) { //se achou um atrator
				GameObject atrator = Atratores.transform.GetChild (nearest).gameObject;
				float deltaX = transform.position.x - atrator.transform.position.x;
				float deltaY = transform.position.y - atrator.transform.position.y;
				float angle = Mathf.Atan2 (deltaY, deltaX);
				Rigidbody2D rgd = GetComponent<Rigidbody2D> ();
				rgd.velocity = new Vector2 (moveSpeed * Mathf.Cos (angle), moveSpeed * Mathf.Sin (angle));
			} else { //se não existe nenhum atrator disponível
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
		} else { //else if(!lookingForAtrator) //se não está procurando por atrator
			if (!reparando.GetComponent<Atrator> ().IsDone()) {
				StartLookingForAtrator ();
			}
		}
	}

	private float distanceTo(GameObject go){
		return Vector2.Distance (transform.position, go.transform.position);
	}
	public override void Attack ()
	{
		if (Time.time - lastTurning > turningDelay) {
			UpdateGuidanceFollowPlayer ();
			lastTurning = Time.time;
		}
		float playerDistance = Vector3.Distance (transform.position, player.transform.position);

		if (weapon.range < playerDistance) {
			if (animator.GetCurrentAnimatorStateInfo (0).IsName ("AttackMode")) {
				Move ();
			}
		} else {
			Stop ();
		}
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
		return ret;
	}
}
