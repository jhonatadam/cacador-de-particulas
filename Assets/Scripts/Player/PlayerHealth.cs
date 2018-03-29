using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;

	public float damageCoolDown = 10f;
	private float damageTime = 0f;

	private Player player;

	// Use this for initialization
	void Start () {
		//Inicializa o HP do player
		health = maxHealth;
		player = GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		UpdateDamageTime ();
	}

	/* Função que gera dano no player.
	 * 
	 * USAR SEMPRE ANTES DO KNOCBACK!
	 * 
	 **/
	[Obsolete ("USAR SEMPRE ANTES DO KNOCBACK!")]
	public void DamagePlayer(float damage) {
		if (damageTime < damageCoolDown || !player.GetUpdateOn())
			return;

		if (health - damage < 0) {
			health = 0;
			damageTime = 0;
			return;
		} 
		health -= damage;
		damageTime = 0;

	}

	/* Função que particula usa para gerar dano no player.
	 * 
	 * 
	 * 
	 **/
	public void ParticleDamagePlayer(float damage) {
		if (damageTime < damageCoolDown || !player.GetUpdateOn())
			return;

		GameObject temp = gameObject.transform.GetChild (3).gameObject;
		PlayerEnergy temp2 = gameObject.GetComponent<PlayerEnergy> ();


		if (temp.activeInHierarchy && temp2.level != EnergyLevel.Vermelho)
			damage = 0;

		if (health - damage < 0) {
			health = 0;
			damageTime = 0;
			return;
		} 
		health -= damage;
		damageTime = 0;

	}

	/* Função que cura o HP do player
	 * 
	 * 
	 * 
	 * */
	public void CurePlayer(float cure) {
		if (health + cure > maxHealth) {
			health = maxHealth;
			return;
		}

		health += cure;
	}

	void UpdateDamageTime() {
		damageTime++;
	}
}
