using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CargueiroBehavior : EnemyBehavior {

	// Coordenada (em x) máxima (right) e 
	// mínima (left) que o patrulheiro caminha.
	// durante sua patrulha.
	public float rightLimit;
	public float leftLimit;

	//Variável para indicar se o cargueiro já viu o player.
	private bool sawPlayer = false;

	// Arma corpo a corpo
	private EnemyMeleeWeapon weapon;

	// Referência do animator
	public Animator animator;

	void Start () {
		base.Start ();
		weapon = GetComponent <EnemyMeleeWeapon> ();
		animator = GetComponentInChildren <Animator> ();
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
		animator.SetBool ("sawPlayer", sawPlayer);
	}

	public override void Attack ()
	{
		UpdateGuidanceFollowPlayer ();
		float playerDistance = Vector3.Distance (transform.position, player.transform.position);

		if (weapon.range < playerDistance) {
			Move ();
		} else {
			Stop ();
		}
	}

	public override void Patrol ()
	{
		
		// Atualizando orientação
		//Se o player não foi visto, permanece na patrulha, se foi, persegue-o até sair do andar.
		if (!sawPlayer)
			UpdateGuidancePatrol ();
		else {
			UpdateGuidanceFollowPlayer ();
			trackPlayer ();
		}

		// so anda se estiver em modo patrulha na animação
//		if (animator.GetCurrentAnimatorStateInfo (0).IsName ("Patrol") || ) {
			// Andar
			Move ();		
//		}
	}

	public override bool Look ()
	{
		bool ret = (EnemyInCamera () && IsFacingThePlayer () && player.isActiveAndEnabled);
		sawPlayer = ret ? ret : sawPlayer;
		return ret;
	}


	public void Move () {
			// Determinando velocidade.
			float playerSpeed = (player != null ? player.speed : 1); 
			float speed = (isFacingRight ? moveSpeed * playerSpeed : -moveSpeed * playerSpeed);

			// Atualizando velocidade.
			rb2d.velocity = new Vector2 (speed, rb2d.velocity.y);
	}

	//Função cria um raycast em direção ao player e checa se há uma parade/chão/teto/?? entre eles dois
	//assim supõe se o player está em outro andar.
	private void trackPlayer() {
		Debug.DrawLine (transform.position, player.transform.position);

		//Raycast retorna a lista de objetos que o raycast cruzou.
		RaycastHit2D [] trackedObject = Physics2D.LinecastAll (new Vector2 (transform.position.x, transform.position.y),
			new Vector2 (player.transform.position.x, player.transform.position.y), LayerMask.NameToLayer("Interative"));

		if (trackedObject != null) {

			foreach (RaycastHit2D hit in trackedObject) {
				if (hit.collider.tag != "Enemy") {
					if (hit.collider.tag == "Ground" || hit.collider.tag == "Elevator") {
						sawPlayer = false;
					}
				}
			}
		}
	}

	//Retorna o limite em funcao da posicao do player
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

	//Retorna o limite em funcao dos limites estabelecidos.
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

	public bool IsFacingThePlayer () {
		if (isFacingRight) {
			return transform.position.x < player.transform.position.x;
		} else {
			return transform.position.x > player.transform.position.x;
		}
	}

}
