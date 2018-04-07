using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeWeapon : MonoBehaviour {

	// Referência para o script
	// de comportamento do inimigo.
	[HideInInspector]
	public EnemyBehavior behavior;

	// true se arma está pronta para
	// atirar, false caso contrário.
	[HideInInspector]
	public bool isLoaded;

	// Tempo que demorar parar a arma 
	// do inimigo ser recarregada.
	public float reloadTime;

	// Dano da bala.
	public float damage;

	// Contagem regressiva para 
	// recarregar a pistola
	[HideInInspector]
	public float timeCounter;

	// Distância a qual o inimigo
	// começa a atacar o player.
	public float range;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren <Animator> ();
		behavior = GetComponent<EnemyBehavior> ();
		isLoaded = true;
	}

	void Update () {
		Hit ();
	}

	public void Hit () {
		if (isLoaded) {
			if (Vector3.Distance(behavior.player.transform.position, transform.position) < range) {
				// bater
				animator.SetTrigger ("Attack");

				isLoaded = false;
				timeCounter = reloadTime;
			}
		} else {
			Reload ();	
		}
	}

	public void Reload () {
		if (timeCounter > 0.0f) {
			timeCounter -= Time.deltaTime;
		} else {
			isLoaded = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (GetComponent<EnemyBehavior> ().dead) {
			return;
		}
		string tag = other.gameObject.tag;

		if (tag == "Player") {
			//Calcula a direcao que o player esta em relacao ao inimigo para aplicar o knockback.
			float direction = other.transform.position.x - transform.position.x;
			direction = direction / Mathf.Abs (direction);
			other.gameObject.GetComponent<Player> ().Knockback (direction, 0.5f, 0.1f, 2);


			other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer (damage);
		}
	}
}
