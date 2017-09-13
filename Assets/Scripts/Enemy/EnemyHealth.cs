using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public float maxHealth;
	public float health;

	// Use this for initialization
	void Start () {
		health = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0) {
			KillEnemy ();
		}
	}

	public void DamageEnemy(float damage) {

		if (health - damage < 0) {
			health = 0;
			return;
		} 
		health -= damage;

	}

	/* Função que cura o HP do player
	 * 
	 * 
	 * 
	 * */
	public void CureEnemy(float cure) {
		if (health + cure > maxHealth) {
			health = maxHealth;
			return;
		}

		health += cure;
	}

	public void KillEnemy() {
		//TODO essa é apenas uma morte provisória, é preciso fazer corretamente. Colocar animações e etc.

		Destroy (this.gameObject);
	}
}
