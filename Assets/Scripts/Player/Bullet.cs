﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	private float damage;
	public RingController ring;
	private ParticleSystem ps;
	private SpriteRenderer sr;
	private bool dead = false;
	private CircleCollider2D cl;
	// Use this for initialization
	void Start () {
		sr = gameObject.GetComponent<SpriteRenderer> ();
		ps = gameObject.GetComponent<ParticleSystem> ();
		ring.Emit (30);
		cl = gameObject.GetComponent<CircleCollider2D> ();
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
		if (other.gameObject.layer == 12 || other.gameObject.tag == "Player") {
			return;
		}
		if (!dead) {
			string tag = other.gameObject.tag;
			print (tag);
			if (tag == "Enemy") {
				other.gameObject.GetComponent<EnemyHealth> ().DamageEnemy (damage);
			}
			ring.Emit (3);
			ring.Stop ();
			ps.Stop ();
			sr.color = new Color (0, 0, 0, 0);
			cl.enabled = false;
			dead = true;
		}
	}
	private void OnTriggerEnter2D(Collider2D other) {
		if (!dead && other.tag == "Enemy") {
			string tag = other.gameObject.tag;
			other.gameObject.GetComponent<EnemyHealth> ().DamageEnemy (damage);
			ring.Emit (3);
			ring.Stop ();
			ps.Stop ();
			GetComponent<TrailRenderer> ().enabled = false;
			sr.color = new Color (0, 0, 0, 0);
			cl.enabled = false;
			dead = true;
		}
	}
}
