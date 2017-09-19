using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyGun : MonoBehaviour {

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

	// Direção do tiro.
	public Vector2 shootingDirection;

	// Munição.
	public GameObject bullet;

	// Velocidade da bala.
	public float bulletSpeed;

	// Dano da bala.
	public float bulletDamage;

	// Contagem regressiva para 
	// recarregar a pistola
	[HideInInspector]
	public float timeCounter;

	public Transform bulletExitPosition;

	private Animator animator;

	// Use this for initialization
	void Start () {
		animator = GetComponent<Animator> ();
		behavior = GetComponent<PatrulheiroBehavior> ();
		isLoaded = true;
	}
	
	public void Shoot () {
		// Precisa estar em modo de ataque (na animação) para atacar
		if (!animator.GetCurrentAnimatorStateInfo (0).IsName ("AttackMode"))
			return;

		if (isLoaded) {
			// atirar

			//GameObject bulletTemp = Instantiate (bullet, 
			//	transform.position + 0.1f * (behavior.isFacingRight ? Vector3.right : Vector3.left), transform.rotation);

			GameObject bulletTemp = Instantiate (bullet, bulletExitPosition.position, transform.rotation);

			bulletTemp.GetComponent<Bullet> ().setDamage (bulletDamage);
			bulletTemp.GetComponent<Rigidbody2D> ().velocity = shootingDirection * bulletSpeed;

			// Ativando animação de tiro
			animator.SetTrigger ("Attack");

			isLoaded = false;
			timeCounter = reloadTime;

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

	public abstract void Aim ();

}
