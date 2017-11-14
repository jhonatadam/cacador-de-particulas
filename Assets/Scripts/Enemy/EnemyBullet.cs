﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour {

	private float damage;
	private SpriteRenderer sr;

	public bool dead = false;

	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer> ();
	}

	// Update is called once per frame
	void Update () {
		if (dead) {
			Destroy (this.gameObject, 1.0f);
		}
	}

	public void setDamage(float damage) {
		this.damage = damage;
	}

	private void OnCollisionEnter2D(Collision2D other) {
		string tag = other.gameObject.tag;
		print ("colidiu com " + tag);
		if (tag == "Player") {
			other.gameObject.GetComponent<PlayerHealth> ().DamagePlayer(damage);
			if (other.gameObject.transform.position.x < this.gameObject.transform.position.x) {
				other.gameObject.GetComponent<Player> ().Knockback (-1, 0.5f, 0.1f, 2);
			} else {
				other.gameObject.GetComponent<Player> ().Knockback (1, 0.5f, 0.1f, 2);
			}
		}
		sr.color = new Color (0, 0, 0, 0);
		dead = true;
	}
}
