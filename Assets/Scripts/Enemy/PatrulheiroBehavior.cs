using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrulheiroBehavior : MonoBehaviour {

	// Orientação atual do patrulheiro.
	public bool isFacingRight = true;

	// Coordenada (em x) máxima (right) e 
	// mínima (left) que o patrulheiro caminha.
	public float rightLimit;
	public float leftLimit;

	// Velocidade em que o patrulheiro se move
	// em relação ao Player.
	public float moveSpeed = 1.8f;

	// Rigidbody.
	private Rigidbody2D rb2d;

	// Referência para o Player.
	public Player player;

	// Use this for initialization.
	void Start () {
		try {
			// Buscando referência do Player.
			player = GameObject.Find ("Player").GetComponent<Player> ();
		} catch {
			Debug.Log ("Patrulheiro: não encontrou o objeto Player.");
			player = null;
		}

		rb2d = GetComponent<Rigidbody2D> ();
	}

	// Update is called once per frame.
	void Update () {
		// Atualizando orientação.
		UpdateGuidance ();
		// movimentando
		Move ();
	}

	public void UpdateGuidance () {
		if (isFacingRight) {
			if (transform.position.x >= rightLimit) {
				isFacingRight = false;
			}
		} else {
			if (transform.position.x <= leftLimit) {
				isFacingRight = true;
			}
		}
	}

	public void Move () {
		// Determinando velocidade.
		float playerSpeed = (player != null ? player.speed :  1); 
		// Atualizando velocidade.
		rb2d.velocity = new Vector2 (moveSpeed * 1, rb2d.velocity.y);
	}
}
