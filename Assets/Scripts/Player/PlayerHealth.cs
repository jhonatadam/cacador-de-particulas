﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerHealth : MonoBehaviour {

	public float maxHealth = 100f;
	public float health;

	public float damageCoolDown = 10f;
	private float damageTime = 0f;

	public bool dead = false;

	private Player player;
	private AudioManager audioManager;

	// Use this for initialization
	void Start () {
		//Inicializa o HP do player
		health = maxHealth;
		player = GetComponent<Player>();
		audioManager = AudioManager.instance;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void FixedUpdate() {
		UpdateDamageTime ();
	}

	///<summary>
	///Função que gera dano no player.
	///!!!USAR SEMPRE ANTES DO KNOCBACK!!!
	///</summary>
	/// <param name="damage">Dano que o player recebe</param>
	public void DamagePlayer(float damage) {
		if (damageTime < damageCoolDown || player.GetInDialogue() || dead)
			return;
        GameObject.Find("HUD").GetComponent<HealthBar>().DamageBlink(damage);
		if (health - damage <= 0) {
			health = 0;
			damageTime = 0;
			dead = true;
			player.Death ();
			player.SetUpdateFalse ();
			return;
		}
		audioManager.PlaySound ("Pain");
		health -= damage;
		damageTime = 0;
        
	}

	/* Função que particula usa para gerar dano no player.
	 * 
	 * 
	 * 
	 **/
	public void ParticleDamagePlayer(float damage) {
        GameObject temp = gameObject.transform.GetChild(3).gameObject;
        PlayerEnergy temp2 = gameObject.GetComponent<PlayerEnergy>();

        if (damageTime < damageCoolDown || player.GetInDialogue() || dead || (temp.activeInHierarchy && temp2.level != EnergyLevel.Vermelho))
			return;

        if (temp.activeInHierarchy && temp2.level != EnergyLevel.Vermelho) {
            damage = 0;
        } 
        GameObject.Find("HUD").GetComponent<HealthBar>().DamageBlink(damage);

        
        if (health - damage <= 0) {
			health = 0;
			damageTime = 0;
			dead = true;
			player.Death ();
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
	void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "DropHP") {
			if (coll.gameObject.GetComponent<DropHP> ().active) {
				audioManager.PlaySound("DropHP");
				CurePlayer (coll.gameObject.GetComponent<DropHP> ().amount);
				coll.gameObject.GetComponent<EffectDestroy> ().End ();
				coll.gameObject.GetComponent<DropHP> ().active = false;
			}
		}

	}
}
