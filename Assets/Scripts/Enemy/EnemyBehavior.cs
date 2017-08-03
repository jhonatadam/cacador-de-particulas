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

	// Ação do inimigo
	public void Act () {
		isSeeingThePlayer = Look ();

		// Se o inimigo vê o player:
		if (isSeeingThePlayer) {
			Attack ();
		} else {
			Patrol ();
		}
	}

	public abstract void Patrol ();
	public abstract void Attack ();
	public abstract bool Look ();
}
