using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour {

	// Orientação atual do inimigo.
	[HideInInspector]
	public bool isFacingRight;

	// true se o inimigo está vendo o
	// player, false caso contrário.
	public bool isSeeingThePlayer;

	// Velocidade em que o inimigo
	// se move em relação ao Player.
	public float moveSpeed;

	// Referência para o Player.
	[HideInInspector]
	public Player player;

	// Rigidbody.
	[HideInInspector]
	public Rigidbody2D rb2d;

	// Renderer
	[HideInInspector]
	public Renderer rend;

	// Renderer
	[HideInInspector]
	public Camera mainCamera;

	public bool dead = false;

	// Use this for initialization.
	public void Start () {
		try {
			// Buscando referência do Player.
			player = GameObject.Find ("Player").GetComponent<Player> ();
			mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
		} catch {
			Debug.Log ("EnemyBehaviour: não encontrou o objeto Player.");
			player = null;
		}

		rb2d = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();

		isFacingRight = true;
	}


	// Ação do inimigo
	public void Act () {
		if (dead) {
			return;
		}
		// Olhando
		isSeeingThePlayer = Look ();

		// Se o inimigo vê o player:
		if (isSeeingThePlayer) {
			Attack ();
		} else {
			Patrol ();
		}
	}

	public void Stop () {
		rb2d.velocity = new Vector2 (0, rb2d.velocity.y);
	}

	public bool EnemyInCamera() {
		if (mainCamera != null) {
			Vector3 screenPoint = mainCamera.WorldToViewportPoint (transform.position);
			bool onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
			return onScreen;
		} else {
			print ("EnemyBehaviour: Camera não encontrada!");
			return false;
		}
	}
	//funcao para virar para o outro lado
	public void TurnAround(){
		if (dead) {
			return;
		}
		if (isFacingRight) {
			isFacingRight = false;
			transform.rotation = new Quaternion (0, 180, 0, 0);
		} else {
			isFacingRight = true;
			transform.rotation = new Quaternion (0, 0, 0, 0);
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "IALimite" || other.tag == "Enemy") {
			TurnAround ();
		}
	}

	public abstract void Patrol ();
	public abstract void Attack ();
	public abstract bool Look ();
}
