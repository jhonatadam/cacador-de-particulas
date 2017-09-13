using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyBehavior : MonoBehaviour {

	// Orientação atual do inimigo.
	[HideInInspector]
	public bool isFacingRight;

	// true se o inimigo está vendo o
	// player, false caso contrário.
	[HideInInspector]
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

	// Use this for initialization.
	public void Start () {
		try {
			// Buscando referência do Player.
			player = GameObject.Find ("Player").GetComponent<Player> ();
		} catch {
			Debug.Log ("Patrulheiro: não encontrou o objeto Player.");
			player = null;
		}

		rb2d = GetComponent<Rigidbody2D> ();
		rend = GetComponent<Renderer> ();

		isFacingRight = true;
	}


	// Ação do inimigo
	public void Act () {
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

	public abstract void Patrol ();
	public abstract void Attack ();
	public abstract bool Look ();
}
