using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReparadorMeleeWeapon : MonoBehaviour {

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
	private Collider2D attack;

	// Use this for initialization
	void Start () {
		animator = GetComponentInChildren<Animator> ();
		attack = GetComponentInChildren<Collider2D> ();
		behavior = GetComponent<EnemyBehavior> ();
		isLoaded = true;
	}

	void Update () {
		Hit ();
	}

	public void Hit () {
		float strongAttack = Random.Range (0.0f, 1.0f);
		if (isLoaded) {
			if (Vector3.Distance(behavior.player.transform.position, transform.position) < range) {
				// bater

				if (strongAttack > 0.5f) {
					animator.SetTrigger ("Attack1");
				} else {
					animator.SetTrigger ("Attack2");
				}

				isLoaded = false;
				timeCounter = reloadTime;
			}
		} else {
			Reload ();	
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "Player") {
			float direction = other.transform.position.x - transform.position.x;
			direction = direction / Mathf.Abs (direction);
			other.gameObject.GetComponent<Player> ().Knockback (direction, 0.3f, 0.1f, 2);

			other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer (damage);
		}
	}

	public void Reload () {
		if (timeCounter > 0.0f) {
			timeCounter -= Time.deltaTime;
		} else {
			isLoaded = true;
		}
	}
}
