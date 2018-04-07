using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SamuraiMeleeWeapon : MonoBehaviour {

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
		if (GetComponent<EnemyBehavior> ().dead) {
			return;
		}
		Hit ();
	}

	public void Hit () {
		float strongAttack = Random.Range (0.0f, 1.0f);
		if (isLoaded) {
			if (Vector3.Distance(behavior.player.transform.position, transform.position) < range) {
				// bater
				behavior.rb2d.velocity = new Vector2(0, behavior.rb2d.velocity.y);
				if (strongAttack > 0.5f) {
					animator.SetTrigger ("StrongAttack");
				} else {
					animator.SetTrigger ("WeakAttack");
				}

				isLoaded = false;
				timeCounter = reloadTime;
			}
		} else {
			Reload ();	
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (GetComponent<EnemyBehavior> ().dead) {
			return;
		}
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer (damage);

			float direction = other.transform.position.x - transform.position.x;
			direction = direction / Mathf.Abs (direction);
			other.gameObject.GetComponent<Player> ().Knockback (direction, 0.5f, 0.1f, 3);
		}
	}

	public void Reload () {
		if (timeCounter > 0.0f) {
			timeCounter -= Time.deltaTime;
		} else {
			isLoaded = true;
		}
	}
	void OnDestroy(){
		behavior = null;
		animator = null;
		attack = null;
	}
}
