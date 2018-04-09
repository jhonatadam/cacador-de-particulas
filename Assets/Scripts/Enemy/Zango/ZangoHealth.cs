using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZangoHealth : EnemyHealth {

	private ZangoMeleeWeapon weapon;
	private GameObject battleStarter;
	// Use this for initialization
	void Start () {
		health = maxHealth;
		weapon = gameObject.GetComponent<ZangoMeleeWeapon> ();
		battleStarter = GameObject.Find ("BattleStarter");
	}

	// Update is called once per frame
	void Update () {
		GetComponentInChildren<SpriteRenderer>().color = new Color(1, health/maxHealth, health/maxHealth);
		if (health <= 0) {
			KillEnemy ();
		}
	}

	public override void DamageEnemy(float damage) {

		if (health - damage < 0) {
			health = 0;
			return;
		} 
		health -= damage;

		weapon.energyBallCounter += damage;
		weapon.cureCounter += damage;
		weapon.rotationCounter += damage;

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
		battleStarter.GetComponent<ZangoBattleStarter> ().camera2.SetActive (false);
		battleStarter.GetComponent<ZangoBattleStarter> ().camera1.SetActive (true);

		//TODO essa é apenas uma morte provisória, é preciso fazer corretamente. Colocar animações e etc.
		Destroy (this.gameObject);
	}
}
