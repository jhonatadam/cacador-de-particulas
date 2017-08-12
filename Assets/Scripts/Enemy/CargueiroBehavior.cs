using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargueiroBehavior : EnemyBehavior {

	// Arma corpo a corpo
	private EnemyMeleeWeapon weapon;

	// Referência do animator
	public Animator animator;

	void Start () {
		base.Start ();
		weapon = GetComponent <EnemyMeleeWeapon> ();
		animator = GetComponent <Animator> ();
	}

	// Update is called once per frame
	void Update () {
		// se o cargueiro estiver dando o soco, o seu 
		// comportamento não é atualizado
		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Punch")) {
			return;
		}

		Act ();
	}

	void LateUpdate () {
		animator.SetBool ("isSeeingThePlayer", isSeeingThePlayer);
	}

	public override void Attack ()
	{
		UpdateGuidance ();
		float playerDistance = Vector3.Distance (transform.position, player.transform.position);

		if (weapon.range < playerDistance) {
			Move ();
		} else {
			Stop ();
		}
	}

	public override void Patrol ()
	{
		Stop ();
	}

	public override bool Look ()
	{
		return (rend.isVisible && player.isActiveAndEnabled);
	}


	public void Move () {
			// Determinando velocidade.
			float playerSpeed = (player != null ? player.speed : 1); 
			float speed = (isFacingRight ? moveSpeed * playerSpeed : -moveSpeed * playerSpeed);

			// Atualizando velocidade.
			rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
	}

	public void UpdateGuidance () {
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

}
